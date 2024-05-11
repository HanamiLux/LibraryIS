using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LibraryWebApi.Models
{
    public class DbHelper <T>(T context) where T : DbContext
    {
        private readonly T context = context ?? throw new ArgumentNullException(nameof(context), "Context cannot be null");

        public IQueryable<TEntity> GetAll<TEntity>(params string[] fkeys) where TEntity : class 
        {
            IQueryable<TEntity> query = context.Set<TEntity>().AsQueryable();
            foreach (string key in fkeys)
            {
                var propertyInfo = typeof(TEntity).GetProperty(key) ??
                    throw new ArgumentException($"Property '{key}' does not exist on type '{typeof(TEntity).Name}'.");
                var navigationPropertyType = propertyInfo.PropertyType;

                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, key);
                var lambda = Expression.Lambda<Func<TEntity, object>>(property, parameter);

                query = query.Include(lambda);
            }


            return query;

        }

        public async Task<List<TEntity>> GetByPageAsync<TEntity>(int page, int pageSize) where TEntity : class
        {
            return await GetAll<TEntity>().Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(int id) where TEntity : class
        {
            try
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
            catch (NullReferenceException)
            {
                return null;
            }
            
        }
 
        public async Task<User?> GetUserAuthAsync(string name, string password)
        {
            try
            {
                return await context.Set<User>().Include(u => u.Role).SingleOrDefaultAsync(obj => obj.Login.ToLower() == name.ToLower() && obj.Password == password);
            }
            catch (NullReferenceException)
            {
                return null;
            }

        }

        public string GetUserPassword(string name)
        {
            try
            {
                return context.Set<User>().SingleOrDefaultAsync(obj => obj.Login.ToLower() == name.ToLower()).Result.Password;
            }
            catch (NullReferenceException)
            {
                return null;
            }

        }


        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync<TEntity>(int id) where TEntity : class
        {
            var entity = await GetByIdAsync<TEntity>(id);
            if (entity == null) return;
            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}

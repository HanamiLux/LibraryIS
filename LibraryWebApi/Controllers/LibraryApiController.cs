using LibraryWebApi.Data;
using LibraryWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace LibraryWebApi.Controllers
{
    [ApiController]
    public class LibraryApiController : ControllerBase
    {
        private readonly DbHelper<LibraryContext> _db;

        public LibraryApiController(LibraryContext dbContext)
        {
            _db = new DbHelper<LibraryContext>(dbContext);
        }

        #region Roles CRUD
        [HttpGet]
        [Route("api/[controller]/Roles/GetAll/")]
        public IActionResult GetAllRoles()
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Role>(), _db.GetAll<Role>());
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Roles/Get/{id}/")]
        public async Task<IActionResult> GetRole(int id)
        {
            var entity = await _db.GetByIdAsync<Role>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Role>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Roles/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetRolesByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Role>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Role>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Roles/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Role>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Roles/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateRole([FromBody] Role role)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(role), role);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Roles/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateRole([FromBody] Role role)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(role), role);
            return GetResponse(response);
        }
        #endregion

        #region Users CRUD
        [HttpGet]
        [Route("api/[controller]/Users/GetAll/")]
        public IActionResult GetAllUsers()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<User>("Role"), _db.GetAll<User>("Role"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Users/Get/{id}/")]
        public async Task<IActionResult> GetUser(int id)
        {
            var entity = await _db.GetByIdAsync<User>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<User>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Users/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetUsersByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<User>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<User>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Users/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<User>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Users/Create/")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (_db.GetUserPassword(user.Login) != null) 
                return BadRequest("Пользователь уже существует!");
            user.Roleid = 7;
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(user), user);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Users/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(user), user);
            return GetResponse(response);
        }
        #endregion

        #region Bestbooks CRUD
        [HttpGet]
        [Route("api/[controller]/Bestbooks/GetAll/")]
        public IActionResult GetAllBestbooks()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Bestbook>("Book"), _db.GetAll<Bestbook>("Book"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Bestbooks/Get/{id}/")]
        public async Task<IActionResult> GetBestbook(int id)
        {
            var entity = await _db.GetByIdAsync<Bestbook>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Bestbook>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Bestbooks/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetBestbooksByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Bestbook>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Bestbook>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Bestbooks/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteBestbook(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Bestbook>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Bestbooks/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateBestbook([FromBody] Bestbook bestbook)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(bestbook), bestbook);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Bestbooks/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateBestbook([FromBody] Bestbook bestbook)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(bestbook), bestbook);
            return GetResponse(response);
        }
        #endregion

        #region Books CRUD
        [HttpGet]
        [Route("api/[controller]/Books/GetAll/")]
        public IActionResult GetAllBooks()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Book>("Author", "Genre", "Publisher"), _db.GetAll<Book>("Author", "Genre", "Publisher"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Books/Get/{id}/")]
        public async Task<IActionResult> GetBook(int id)
        {
            var entity = await _db.GetByIdAsync<Book>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Book>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Books/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetBooksByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Book>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Book>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Books/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Book>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Books/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateBook([FromBody] Book book)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(book), book);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Books/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateBook([FromBody] Book book)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(book), book);
            return GetResponse(response);
        }
        #endregion

        #region Bookinstances CRUD
        [HttpGet]
        [Route("api/[controller]/Bookinstances/GetAll/")]
        public IActionResult GetAllBookinstances()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Bookinstance>("Book", "Condition"), _db.GetAll<Bookinstance>("Book", "Condition"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Bookinstances/Get/{id}/")]
        public async Task<IActionResult> GetBookinstance(int id)
        {
            var entity = await _db.GetByIdAsync<Bookinstance>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Bookinstance>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Bookinstances/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetBookinstancesByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Bookinstance>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Bookinstance>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Bookinstances/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteBookinstance(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Bookinstance>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Bookinstances/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateBookinstance([FromBody] Bookinstance bookinstance)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(bookinstance), bookinstance);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Bookinstances/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateBookinstance([FromBody] Bookinstance bookinstance)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(bookinstance), bookinstance);
            return GetResponse(response);
        }
        #endregion

        #region Bookrentals CRUD
        [HttpGet]
        [Route("api/[controller]/Bookrentals/GetAll/")]
        public IActionResult GetAllBookrentals()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Bookrental>("Instance", "User"), _db.GetAll<Bookrental>("Instance", "User"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Bookrentals/Get/{id}/")]
        public async Task<IActionResult> GetBookrental(int id)
        {
            var entity = await _db.GetByIdAsync<Bookrental>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Bookrental>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Bookrentals/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetBookrentalsByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Bookrental>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Bookrental>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Bookrentals/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteBookrental(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Bookrental>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Bookrentals/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateBookrental([FromBody] Bookrental bookrental)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(bookrental), bookrental);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Bookrentals/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateBookrental([FromBody] Bookrental bookrental)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(bookrental), bookrental);
            return GetResponse(response);
        }
        #endregion

        #region Bookreviews CRUD
        [HttpGet]
        [Route("api/[controller]/Bookreviews/GetAll/")]
        public IActionResult GetAllBookreviews()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Bookreview>("Book", "User"), _db.GetAll<Bookreview>("Book", "User"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Bookreviews/Get/{id}/")]
        public async Task<IActionResult> GetBookreview(int id)
        {
            var entity = await _db.GetByIdAsync<Bookreview>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Bookreview>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Bookreviews/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetBookreviewsByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Bookreview>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Bookreview>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Bookreviews/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteBookreview(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Bookreview>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Bookreviews/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateBookreview([FromBody] Bookreview bookreview)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(bookreview), bookreview);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Bookreviews/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateBookreview([FromBody] Bookreview bookreview)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(bookreview), bookreview);
            return GetResponse(response);
        }
        #endregion

        #region Conditions CRUD
        [HttpGet]
        [Route("api/[controller]/Conditions/GetAll/")]
        public IActionResult GetAllConditions()
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Condition>(), _db.GetAll<Condition>());
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Conditions/Get/{id}/")]
        public async Task<IActionResult> GetCondition(int id)
        {
            var entity = await _db.GetByIdAsync<Condition>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Condition>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Conditions/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetConditionsByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Condition>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Condition>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Conditions/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteCondition(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Condition>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Conditions/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateCondition([FromBody] Condition condition)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(condition), condition);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Conditions/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateCondition([FromBody] Condition condition)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(condition), condition);
            return GetResponse(response);
        }
        #endregion

        #region Genres CRUD
        [HttpGet]
        [Route("api/[controller]/Genres/GetAll/")]
        public IActionResult GetAllGenres()
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Genre>(), _db.GetAll<Genre>());
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Genres/Get/{id}/")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var entity = await _db.GetByIdAsync<Genre>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Genre>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Genres/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetGenresByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Genre>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Genre>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Genres/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Genre>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Genres/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateGenre([FromBody] Genre genre)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(genre), genre);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Genres/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateGenre([FromBody] Genre genre)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(genre), genre);
            return GetResponse(response);
        }
        #endregion

        #region Logs CRUD
        [HttpGet]
        [Route("api/[controller]/Logs/GetAll/")]
        public IActionResult GetAllLogs()
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Log>("User"), _db.GetAll<Log>("User"));
            return new JsonResult(response, options);
        }

        [HttpGet]
        [Route("api/[controller]/Logs/Get/{id}/")]
        public async Task<IActionResult> GetLog(int id)
        {
            var entity = await _db.GetByIdAsync<Log>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Log>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Logs/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetLogsByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Log>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Log>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Logs/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Log>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Logs/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateLog([FromBody] Log log)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(log), log);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Logs/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateLog([FromBody] Log log)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(log), log);
            return GetResponse(response);
        }
        #endregion

        #region Authors CRUD
        [HttpGet]
        [Route("api/[controller]/Authors/GetAll/")]
        public IActionResult GetAllAuthors()
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Author>(), _db.GetAll<Author>());
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Authors/Get/{id}/")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var entity = await _db.GetByIdAsync<Author>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Author>(id), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Authors/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetAuthorsByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Author>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Author>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Authors/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Author>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Authors/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateAuthor([FromBody] Author author)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(author), author);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Authors/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateAuthor([FromBody] Author author)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(author), author);
            return GetResponse(response);
        }
        #endregion

        #region Publishers CRUD
        [HttpGet]
        [Route("api/[controller]/Publishers/GetAll/")]
        public IActionResult GetAllPublishers()
        {
            var response = ResponseHandler.HandleDbOperations(() => _db.GetAll<Publisher>(), _db.GetAll<Publisher>());
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Publishers/GetByPage/{page?}/{pageSize?}")]
        public async Task<IActionResult> GetPublishersByPage(int page = 1, int pageSize = 10)
        {
            var entity = await _db.GetByPageAsync<Publisher>(page, pageSize);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByPageAsync<Publisher>(page, pageSize), entity);
            return GetResponse(response);
        }

        [HttpGet]
        [Route("api/[controller]/Publishers/Get/{id}/")]
        public async Task<IActionResult> GetPublisher(int id)
        {
            var entity = await _db.GetByIdAsync<Publisher>(id);
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.GetByIdAsync<Publisher>(id), entity);
            return GetResponse(response);
        }

        [HttpDelete]
        [Route("api/[controller]/Publishers/Delete/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.DeleteAsync<Publisher>(id), "Successfully deleted!");
            return GetResponse(response);
        }

        [HttpPost]
        [Route("api/[controller]/Publishers/Create/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreatePublisher([FromBody] Publisher publisher)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.AddAsync(publisher), publisher);
            return GetResponse(response);
        }

        [HttpPut]
        [Route("api/[controller]/Publishers/Update/")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdatePublisher([FromBody] Publisher publisher)
        {
            var response = await ResponseHandler.HandleDbOperationsAsync(() => _db.UpdateAsync(publisher), publisher);
            return GetResponse(response);
        }
        #endregion

        #region additional
        private IActionResult GetResponse(ApiResponse response)
        {
            return (response.Code == "1" || response == null) ? BadRequest(response) : Ok(response);
        }
        #endregion
    }
}
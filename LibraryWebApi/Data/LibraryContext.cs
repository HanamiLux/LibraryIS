using System;
using System.Collections.Generic;
using LibraryWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApi.Data;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Bestbook> Bestbooks { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Bookinstance> Bookinstances { get; set; }

    public virtual DbSet<Bookrental> Bookrentals { get; set; }

    public virtual DbSet<Bookreview> Bookreviews { get; set; }

    public virtual DbSet<Condition> Conditions { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authors_pkey");
        });

        modelBuilder.Entity<Bestbook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bestbooks_pkey");

            entity.HasOne(d => d.Book).WithMany(p => p.Bestbooks).HasConstraintName("bestbooks_bookid_fkey");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("books_pkey");

            entity.Property(e => e.Isavailable).HasDefaultValue(true);

            entity.HasOne(d => d.Author).WithMany(p => p.Books).HasConstraintName("books_authorid_fkey");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books).HasConstraintName("books_genreid_fkey");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasConstraintName("books_publisherid_fkey");
        });

        modelBuilder.Entity<Bookinstance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bookinstances_pkey");

            entity.Property(e => e.Isavailable).HasDefaultValue(true);

            entity.HasOne(d => d.Book).WithMany(p => p.Bookinstances).HasConstraintName("bookinstances_bookid_fkey");

            entity.HasOne(d => d.Condition).WithMany(p => p.Bookinstances).HasConstraintName("bookinstances_conditionid_fkey");
        });

        modelBuilder.Entity<Bookrental>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bookrentals_pkey");

            entity.HasOne(d => d.Instance).WithMany(p => p.Bookrentals).HasConstraintName("bookrentals_instanceid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookrentals).HasConstraintName("bookrentals_userid_fkey");
        });

        modelBuilder.Entity<Bookreview>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("bookreviews_pkey");

            entity.Property(e => e.Reviewdate).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Book).WithMany(p => p.Bookreviews).HasConstraintName("bookreviews_bookid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Bookreviews).HasConstraintName("bookreviews_userid_fkey");
        });

        modelBuilder.Entity<Condition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("conditions_pkey");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("genres_pkey");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("logs_pkey");

            entity.Property(e => e.Logdatetime).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.User).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("logs_userid_fkey");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("publishers_pkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.Property(e => e.Isavailable).HasDefaultValue(true);

            entity.HasOne(d => d.Role).WithMany(p => p.Users).HasConstraintName("users_roleid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

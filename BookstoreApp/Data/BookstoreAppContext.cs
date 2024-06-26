﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BookstoreApp.Areas.Identity.Data;
using BookstoreApp.Models;

namespace BookstoreApp.Models
{
    public class BookstoreAppContext : IdentityDbContext<BookstoreAppUser>
    {
        public BookstoreAppContext (DbContextOptions<BookstoreAppContext> options)
            : base(options)
        {
        }

        public DbSet<BookstoreApp.Models.Author> Author { get; set; } = default!;
        public DbSet<BookstoreApp.Models.Book> Book { get; set; } = default!;
        public DbSet<BookstoreApp.Models.Review> Review { get; set; } = default!;
        public DbSet<BookstoreApp.Models.Genre> Genre { get; set; } = default!;
        public DbSet<BookstoreApp.Models.UserBooks> UserBooks { get; set; } = default!;
        public DbSet<BookstoreApp.Models.BookGenre> BookGenre { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}

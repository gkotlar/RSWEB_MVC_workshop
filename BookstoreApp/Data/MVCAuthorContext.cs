using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models
{
    public class MVCAuthorContext : DbContext
    {
        public MVCAuthorContext (DbContextOptions<MVCAuthorContext> options)
            : base(options)
        {
        }

        public DbSet<BookstoreApp.Models.Author> Author { get; set; } = default!;
    }
}

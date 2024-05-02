using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models
{
    public class GendreContext : DbContext
    {
        public GendreContext (DbContextOptions<GendreContext> options)
            : base(options)
        {
        }

        public DbSet<BookstoreApp.Models.Genre> Genre { get; set; } = default!;
    }
}

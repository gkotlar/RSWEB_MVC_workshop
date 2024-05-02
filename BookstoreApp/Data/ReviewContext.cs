using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApp.Models
{
    public class ReviewContext : DbContext
    {
        public ReviewContext (DbContextOptions<ReviewContext> options)
            : base(options)
        {
        }

        public DbSet<BookstoreApp.Models.Review> Review { get; set; } = default!;
    }
}

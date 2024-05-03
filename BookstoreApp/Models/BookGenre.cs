using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BookstoreApp.Models
{
    public class BookGenre
    {
        public int Id { get; set; }

        [Display(Name = "Book ID")]
        public int BookId { get; set; }

        [Display(Name = "Gendre ID")]

        public int GendreId { get; set; }

        public Book? Book { get; set; }

        public Genre? Genre { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BookstoreApp.Models
{
    public class UserBooks
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(450)]
        [Display(Name = "Application User")]

        public string AppUser { get; set; }

        [Required]
        [Display(Name = "Book ID")]
        public int BookId { get; set; }
    }
}

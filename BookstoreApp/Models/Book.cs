using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookstoreApp.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Year Published")]
        public int? YearPublished { get; set; }

        [Required]
        [Display(Name = "Number of Pages")]
        public int? NumPages { get; set; }

        [Required]
        [StringLength(50)]
        public string? Publisher { get; set; }


        [Display(Name = "Front Page")]
        public int? FrontPage { get; set; }

        [Required]
        [Display(Name = "Download Url")]
        public int? DownloadUrl { get; set; }

        [Required]
        [Display (Name ="Author Id")]
        public int AuthorId { get; set; }

        public Author? Authors { get; set; }

        public ICollection<Review>? Reviews { get; set; }

        public ICollection<BookGenre>? bookGenres { get; set; }

        public ICollection<UserBooks>? UserBooks { get;}
    }
}


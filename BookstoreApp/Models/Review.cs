using System.ComponentModel.DataAnnotations;

namespace BookstoreApp.Models
{
    public class Review
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        [StringLength(450)]
        public string AppUser { get; set; }

        [Required]
        [StringLength(500)]
        public string Comment { get; set; }
        
        public int? Rating { get; set; }
        public Book? Book { get; set; }

    }
}

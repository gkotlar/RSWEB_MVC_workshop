using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BookstoreApp.Models
{
    public class Genre
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string GendreName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using BookstoreApp.Models;
using System.Collections.Generic;

namespace BookstoreApp.ViewModels
{
    public class BookGenresEditViewModel
    {
        public Book Book { get; set; }

        public IFormFile? CoverPage { get; set; }
        public IFormFile? ElectronicVersion {  get; set; }

        public IEnumerable<int>? SelectedGenres { get; set; }
        public IEnumerable<SelectListItem>? GenreList { get; set; }
        
    }
}

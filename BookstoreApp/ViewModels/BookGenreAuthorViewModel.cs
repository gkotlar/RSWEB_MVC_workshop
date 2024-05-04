using Microsoft.AspNetCore.Mvc.Rendering;
using BookstoreApp.Models;
using System.Collections.Generic;

namespace BookstoreApp.ViewModels
{
    public class BookGenreAuthorViewModel
    {
        public IList<Book> Books { get; set; }
        public SelectList Genres { get; set; }
        public SelectList Authors { get; set; }
        public int? BookGenre { get; set; }
        public int? BookAuthor { get; set; }
        public string? SearchString { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using BookstoreApp.Models;
using System.Collections.Generic;

namespace BookstoreApp.ViewModels
{
    public class AuthorFirstLastNameViewModel
    {
        public IList<Author> Authors { get; set; }

       // public List<Book> Books { get; set; }

        public string? SearchStringFName { get; set; }
        public string? SearchStringLName { get; set; }
    }
}

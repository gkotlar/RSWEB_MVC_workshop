using Microsoft.AspNetCore.Mvc.Rendering;
using BookstoreApp.Models;
using System.Collections.Generic;

namespace BookstoreApp.ViewModels
{
    public class BookReviewViewModel
    {
        public Book Books { get; set; }
        public Review Reviews { get; set; }

    }
}

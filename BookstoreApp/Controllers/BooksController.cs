using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Claims;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using Microsoft.CodeAnalysis.Elfie.Serialization;

using BookstoreApp.Models;
using BookstoreApp.ViewModels;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookstoreAppContext _context;

        private readonly ILogger<HomeController> _logger;

        private readonly IWebHostEnvironment _webHostEnvironment;



        public BooksController(BookstoreAppContext context, ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? bookGenre, int? bookAuthor, string searchString )
        {
            IQueryable<Book> books = _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .Include(b => b.bookGenres)
                .AsQueryable();

            // get genres and auhors for the select list
            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(b => b.GenreName);

            var authors = _context.Author.AsEnumerable();
            authors = authors.OrderBy(b => b.FullName);
            //    IQueryable<string> genreQuery = _context.Book.OrderBy(m=>m.bookGenres).Select(m=>m.bookGenres).Distinct();
             
            // query boos by title/genre/author
            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            if (bookGenre.HasValue)
            {
                books = books.Where(b=>b.bookGenres.Any(bg => bg.GenreId == bookGenre));
            }
            if (bookAuthor.HasValue)
            {
                books = books.Where(s => s.AuthorId.Equals(bookAuthor));
            }                

            var bookGenreAuthorVM = new BookGenreAuthorViewModel
            {
                Genres = new SelectList(genres, "Id", "GenreName"),
                Authors = new SelectList(authors, "Id", "FullName"),
                Books = await books.ToListAsync(),
            };

            return View(bookGenreAuthorVM);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .Include(b => b.bookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.UserBooks)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.userId = userId;

            BookReviewViewModel viewModel = new BookReviewViewModel
            {
                Books = book,
                Reviews = new Review()
            };

            return View(viewModel);
        }

        // GET: Books/Create
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName");

            var genres = await _context.Genre.OrderBy(b => b.GenreName).ToListAsync();

            BookGenresEditViewModel viewModel = new BookGenresEditViewModel
            {
                Book = new Book(),
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = new List<int>(),
            };


            return View(viewModel);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookGenresEditViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string uploadedElectronicBook = UploadedElectronicBook(viewModel);
                string uploadedCoverPage = UploadedCoverPage(viewModel);

                Book book = new Book
                {
                    Title = viewModel.Book.Title,
                    AuthorId = viewModel.Book.AuthorId,
                    YearPublished = viewModel.Book.YearPublished,
                    NumPages = viewModel.Book.NumPages,
                    Description = viewModel.Book.Description,
                    Publisher = viewModel.Book.Publisher,
                    FrontPage = uploadedCoverPage,
                    DownloadUrl = uploadedElectronicBook,
                };
                _context.Add(book);
                await _context.SaveChangesAsync();

                if (viewModel.SelectedGenres != null)
                {
                    foreach (int genreId in viewModel.SelectedGenres)
                    {
                        _context.BookGenre.Add(new BookGenre { GenreId = genreId, BookId = book.Id });
                    }
                    await _context.SaveChangesAsync();
                }

                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", viewModel.Book.AuthorId);
            var genres = await _context.Genre.OrderBy(s => s.GenreName).ToListAsync();
            viewModel.GenreList = new MultiSelectList(genres, "Id", "GenreName");
            return View(viewModel);
        }

        // GET: Books/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _context.Book.Where(b => b.Id == id).Include(b => b.bookGenres).First();

            if (book == null)
            {
                return NotFound();
            }

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(b => b.GenreName);

            BookGenresEditViewModel viewModel = new BookGenresEditViewModel
            {
                Book = book,
                GenreList = new MultiSelectList(genres, "Id", "GenreName"),
                SelectedGenres = book.bookGenres.Select(sg => sg.GenreId),
            };

            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", book.AuthorId);
            return View(viewModel);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookGenresEditViewModel viewModel)
        {
            if (id !=viewModel.Book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (viewModel.CoverPage != null)
                    {
                        string uploadedCoverPage = UploadedCoverPage(viewModel);
                        viewModel.Book.FrontPage = uploadedCoverPage;
                    }

                    if (viewModel.ElectronicVersion != null)
                    {
                        string uploadedElectronicBook = UploadedElectronicBook(viewModel);
                        viewModel.Book.DownloadUrl = uploadedElectronicBook;
                    }

                    _context.Update(viewModel.Book);
                    await _context.SaveChangesAsync();

                    IEnumerable<int> newGenreList = viewModel.SelectedGenres;
                    IEnumerable<int> previousGenreList = _context.BookGenre.Where(s => s.BookId == id).Select(s=>s.GenreId);
                    IQueryable<BookGenre> toBeRemoved = _context.BookGenre.Where(s=>s.BookId == id);  
                    if (newGenreList != null)
                    {
                        toBeRemoved = toBeRemoved.Where(s => !newGenreList.Contains(s.GenreId));
                        foreach (int genreId in newGenreList)
                        {
                            if (!previousGenreList.Any(s => s == genreId))
                            {
                                _context.BookGenre.Add(new BookGenre { GenreId = genreId, BookId = id });
                            }
                        }
                    }

                    _context.BookGenre.RemoveRange(toBeRemoved);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(viewModel.Book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", viewModel.Book.AuthorId);
            return View(viewModel);
        }

        // GET: Books/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Authors)
                .Include(b => b.bookGenres).ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }


        // function to upload he picture to the pictures folder and return the unique file name
        private string UploadedCoverPage(BookGenresEditViewModel model)
        {
            string uniqueCoverPageName = null;

            if (model.CoverPage != null)
            {
                string fileDir1 = Path.Combine(_webHostEnvironment.WebRootPath, @"BookCoverPages");

                if (!System.IO.Directory.Exists(fileDir1))
                    System.IO.Directory.CreateDirectory(fileDir1);

                uniqueCoverPageName = Guid.NewGuid().ToString() + Path.GetExtension(model.CoverPage.FileName);

                string filePath1 = Path.Combine(fileDir1, uniqueCoverPageName);

                using (var fileStream1 = new FileStream(filePath1, FileMode.Create))
                {
                    model.CoverPage.CopyTo(fileStream1);
                }
                string fileRelativeUrl1 = @"/BookCoverPages/" + uniqueCoverPageName;
                uniqueCoverPageName = fileRelativeUrl1;

            }
            return uniqueCoverPageName;
        }

        // function to upload the Book PDF to the Files folder and return the unique file name
        private string UploadedElectronicBook(BookGenresEditViewModel model)
        {
            string uniqueEBookName = null;

            if (model.ElectronicVersion != null)
            {
                string fileDir2 = Path.Combine(_webHostEnvironment.WebRootPath, @"BookDownloads");

                if (!System.IO.Directory.Exists(fileDir2))
                    System.IO.Directory.CreateDirectory(fileDir2);

                uniqueEBookName = Guid.NewGuid().ToString() + Path.GetExtension(model.ElectronicVersion.FileName);

                string filePath2 = Path.Combine(fileDir2, uniqueEBookName);

                using (var fileStream2 = new FileStream(filePath2, FileMode.Create))
                {
                    model.ElectronicVersion.CopyTo(fileStream2);
                }
                string fileRelativeUrl2 = @"/BookDownloads/" + uniqueEBookName;
                uniqueEBookName = fileRelativeUrl2;
            }
            return uniqueEBookName;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

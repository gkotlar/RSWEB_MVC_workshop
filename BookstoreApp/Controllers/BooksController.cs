using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookstoreApp.Models;
using Microsoft.AspNetCore.Hosting;

using BookstoreApp.ViewModels;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookstoreAppContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;


        public BooksController(BookstoreAppContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _webHostEnvironment = hostEnvironment;
        }

        // GET: Books
        public async Task<IActionResult> Index(int? bookGenre, int? bookAuthor, string searchString )
        {
            IQueryable<Book> books = _context.Book
                .Include(b => b.Authors)
                .Include(b => b.Reviews)
                .Include(b => b.bookGenres)
                .AsQueryable();

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(b => b.GenreName);


            var authors = _context.Author.AsEnumerable();
            authors = authors.OrderBy(b => b.FullName);
            //    IQueryable<string> genreQuery = _context.Book.OrderBy(m=>m.bookGenres).Select(m=>m.bookGenres).Distinct();

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
                .Include(b => b.bookGenres).ThenInclude(b => b.Genre)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
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
        private string UploadedCoverPage(BookGenresEditViewModel model)
        {
            string uniqueCoverPageName = null;

            if (model.CoverPage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "BookCoverPages");

                if (!System.IO.Directory.Exists(uploadsFolder))
                    System.IO.Directory.CreateDirectory(uploadsFolder);

                uniqueCoverPageName = Guid.NewGuid().ToString() + Path.GetExtension(model.CoverPage.FileName);

                string filePath = Path.Combine(uploadsFolder, uniqueCoverPageName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverPage.CopyTo(fileStream);
                }
            }
            return uniqueCoverPageName;
        }
        private string UploadedElectronicBook(BookGenresEditViewModel model)
        {
            string uniqueEBookName = null;

            if (model.ElectronicVersion != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "BookDownloads");

                if (!System.IO.Directory.Exists(uploadsFolder))
                    System.IO.Directory.CreateDirectory(uploadsFolder);

                uniqueEBookName = Guid.NewGuid().ToString() + Path.GetExtension(model.ElectronicVersion.FileName);

                string filePath = Path.Combine(uploadsFolder, uniqueEBookName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CoverPage.CopyTo(fileStream);
                }
            }
            return uniqueEBookName;
        }

        // GET: Books/Edit/5
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
    }
}

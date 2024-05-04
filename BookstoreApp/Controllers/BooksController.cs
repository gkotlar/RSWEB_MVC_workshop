using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookstoreApp.Models;
using BookstoreApp.ViewModels;

namespace BookstoreApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookstoreAppContext _context;

        public BooksController(BookstoreAppContext context)
        {
            _context = context;
        }

        // GET: Books
        public async Task<IActionResult> Index(int bookGenre, int bookAuthor, string searchString )
        {
            IQueryable<Book> books = _context.Book.AsQueryable();
            IQueryable<BookGenre> bookGenres = _context.BookGenre.AsQueryable();

            var genres = _context.Genre.AsEnumerable();
            genres = genres.OrderBy(b => b.GenreName);


            var authors = _context.Author.AsEnumerable();
            authors = authors.OrderBy(b => b.FullName);
            //    IQueryable<string> genreQuery = _context.Book.OrderBy(m=>m.bookGenres).Select(m=>m.bookGenres).Distinct();

            if (!string.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString));
            }
            if (bookGenre != 0)
            {
                //need to fix it
                bookGenres = bookGenres.Where(s => s.GenreId == bookGenre);
                books = books.Where(s => s.Id.Equals(bookGenres.First().BookId));
            }
            if (bookAuthor != 0)
            {
                books = books.Where(s => s.AuthorId.Equals(bookAuthor));
            }

            books = books.Include(b => b.Authors).Include(b=>b.Reviews).Include(b => b.bookGenres).ThenInclude( b => b.Genre);

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
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,YearPublished,NumPages,Description,Publisher,FrontPage,DownloadUrl,AuthorId")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorId"] = new SelectList(_context.Author, "Id", "FullName", book.AuthorId);
            return View(book);
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

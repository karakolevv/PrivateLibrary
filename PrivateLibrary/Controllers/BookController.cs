using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Models.Pagination;
using System.ComponentModel.Design;
using System.Drawing.Printing;

namespace PrivateLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? search, string? sortOrder, int pageNumber = 1, int pageSize = 5)
        {
            var books = _context.Books
                .Where(b => b.IsTaken == false)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                books = books.Where(book => book.Title.Contains(search) || book.Author.Contains(search) || book.ISBN.Equals(search));
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "Заглавие (А-Я)":
                        books = books.OrderBy(b => b.Title);
                        break;
                    case "Заглавие (Я-А)":
                        books = books.OrderByDescending(b => b.Title);
                        break;
                    case "Автор (А-Я)":
                        books = books.OrderBy(b => b.Author);
                        break;
                    case "Автор (Я-А)":
                        books = books.OrderByDescending(b => b.Author);
                        break;
                    default:
                        break;
                }
            }

            var filtered = await books
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedBooks = new PaginatedList<Book>(filtered, filtered.Count, pageNumber, pageSize);

            return View(paginatedBooks);
        }

        [HttpGet]
        public async Task<IActionResult> AutoComplete(string search)
        {
            var books = await _context.Books
                .Where(book => book.Title.Contains(search) || book.Author.Contains(search))
                .Select(book => new { book.Title, book.Author })
                .ToListAsync()
                ?? throw new ArgumentNullException(nameof(search));

            return Json(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new ArgumentNullException(nameof(id));

            return View(book);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(Book book)
        {
            if (!ModelState.IsValid)
                throw new ArgumentNullException(nameof(book));

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new ArgumentNullException(nameof(id));

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
                throw new ArgumentNullException(nameof(book));

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new ArgumentNullException(nameof(id));

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Take(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id)
                ?? throw new ArgumentNullException(nameof(id));

            if (_context.Books.Where(b => b.IsTaken == false).Count() < 3)
            {
                ModelState.AddModelError("", "Книгата не може да бъде взета.");
                return View("Details", book);
            }

            book.IsTaken = true;

            int numberOfDays = 14;
            var currentUser = await _userManager.GetUserAsync(User)
                ?? throw new ArgumentNullException(nameof(id));
            var reader = await _context.Readers.FirstOrDefaultAsync(r => r.UserId == currentUser.Id);

            var takenBook = new TakenBook()
            {
                BookId = book.Id,
                Title = book.Title,
                Author = book.Author,
                DateOfTaking = DateTime.Now,
                DateOfReturn = DateTime.Now.AddDays(numberOfDays),
                Price = book.CostPerDay * numberOfDays,
                ReaderId = reader.Id
            };

            _context.Add(takenBook);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), "TakenBook", new { id = takenBook.Id });
        }
    }
}

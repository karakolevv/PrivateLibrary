using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Models.Pagination;
using System.Drawing.Printing;
using static System.Reflection.Metadata.BlobBuilder;

namespace PrivateLibrary.Controllers
{
    public class TakenBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TakenBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? search, int pageNumber = 1, int pageSize = 10)
        {
            var takenBooks = _context.TakenBooks.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                takenBooks = takenBooks.Where(tb => tb.Title.Contains(search) || tb.Author.Contains(search));
            }

            var filtered = await takenBooks
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

            var paginatedTakenBooks = new PaginatedList<TakenBook>(filtered, takenBooks.Count(), pageNumber, pageSize);

            return View(paginatedTakenBooks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var takenBook = await _context.TakenBooks
                .Include(tb => tb.Reader)
                .Include(tb => tb.Reader.User)
                .Include(tb => tb.Book)
                .FirstOrDefaultAsync(x => x.Id == id);
            return View(takenBook);
        }

        public async Task<IActionResult> Return(int id)
        {
            var takenBook = await _context.TakenBooks.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new ArgumentNullException(nameof(id));
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == takenBook.BookId)
                ?? throw new ArgumentNullException(nameof(id));

            book.IsTaken = false;
            takenBook.DateOfReturn = DateTime.Now;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index), "Book");
        }
    }
}

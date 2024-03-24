using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;

namespace PrivateLibrary.Controllers
{
    public class TakenBookController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TakenBookController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var takenBooks = await _context.TakenBooks
                .ToListAsync();
            return View(takenBooks);
        }

        public async Task<IActionResult> Details(int id)
        {
            var takenBook = await _context.TakenBooks
                .Include(tb => tb.Reader)
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

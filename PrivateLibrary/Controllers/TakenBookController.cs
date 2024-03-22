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
                .FirstOrDefaultAsync(x => x.Id == id);
            return View(takenBook);
        }
    }
}

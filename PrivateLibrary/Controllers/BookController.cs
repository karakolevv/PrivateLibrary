using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using PrivateLibrary.Data.Models;
using System.ComponentModel.Design;

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
        public async Task<IActionResult> Index(string search)
        {
            var books = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                books = books.Where(book => book.Title.Contains(search));
            }

            return View(await books.ToListAsync() ?? throw new ArgumentNullException(nameof(search)));
        }

        [HttpGet]
        public async Task<IActionResult> AutoComplete(string search)
        {
            var books = await _context.Books
                .Where(book => book.Title.Contains(search))
                .Select(book => book.Title).ToListAsync()
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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
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

            book.IsTaken = true;

            int numberOfDays = 14;
            var currentUser = await _userManager.GetUserAsync(User)
                ?? throw new ArgumentNullException(nameof(id));

            var takenBook = new TakenBook()
            {
                Title = book.Title,
                Author = book.Author,
                DateOfTaking = DateTime.Now,
                DateOfReturn = DateTime.Now.AddDays(numberOfDays),
                Price = book.CostPerDay * numberOfDays,
                ReaderId = currentUser.Id
            };

            _context.Add(takenBook);
            await _context.SaveChangesAsync();

            //TODO: da ne te prashta kum tuka
            return RedirectToAction(nameof(Index), "TakenBook");
        }
    }
}

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

        public async Task<IActionResult> Index()
        {
            var books = await _context.Books.ToListAsync();
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
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
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Take(int id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            book.IsTaken = true;
            await _context.SaveChangesAsync();

            //TODO: moje bi da e konstanta¿
            int numberOfDays = 14;
            var currentUser = await _userManager.GetUserAsync(User);

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
            return RedirectToAction("Index", "TakenBook");
        }
    }
}

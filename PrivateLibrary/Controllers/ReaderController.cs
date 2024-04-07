using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using PrivateLibrary.Models.Pagination;
using static System.Reflection.Metadata.BlobBuilder;
using System.Drawing.Printing;
using PrivateLibrary.Models.Reader;

namespace PrivateLibrary.Controllers
{
    public class ReaderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ReaderController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? search, int pageNumber = 1, int pageSize = 5)
        {
            var readers = _context.Readers
                .Include(u => u.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                readers = readers.Where(readers => readers.User.UserName.Contains(search)
                || readers.User.FirstName.Contains(search) || readers.User.LastName.Contains(search)
                || readers.User.Email.Contains(search));
            }

            var filtered = await readers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedReaders = new PaginatedList<Reader>(filtered, readers.Count(), pageNumber, pageSize);

            return View(paginatedReaders);
        }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            ReaderRegisterViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(ReaderRegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Неуспешна регистрация!";
                return View(model);
            }

            var user = new ApplicationUser()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Reader");
                var reader = new Reader()
                {
                    User = user,
                    UserId = user.Id
                };
                await _context.Readers.AddAsync(reader);
                await _context.SaveChangesAsync();
                return RedirectToAction("GetAll", "Reader");
            }

            foreach (var item in result.Errors)
            {
                ModelState.AddModelError("", item.Description);
            }

            return View(model);
        }

        public async Task<IActionResult> Details(string userId)
        {
            var reader = await _context.Readers
                .Include(u => u.User)
                .FirstOrDefaultAsync(e => e.Id == userId)
                ?? throw new ArgumentNullException(nameof(userId));

            return View(reader);
        }

        [Authorize(Roles = "Admin, Reader, Employee")]
        public async Task<IActionResult> TakenBooks(string readerId, string search, int pageNumber = 1, int pageSize = 10)
        {
            var takenBooks = _context.TakenBooks
                .Include(tb => tb.Book)
                .Where(tb => tb.ReaderId == readerId && tb.DateOfReturn > DateTime.Now)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                takenBooks = takenBooks.Where(takenBooks => takenBooks.Title.Contains(search) || takenBooks.Author.Contains(search));
            }

            var filtered = await takenBooks
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedTakenBooks = new PaginatedList<TakenBook>(filtered, takenBooks.Count(), pageNumber, pageSize);

            return View(paginatedTakenBooks);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string userId)
        {
            var reader = await _context.Readers
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.UserId == userId)
                    ?? throw new ArgumentNullException(nameof(userId));

            if (reader is null)
            {
                return NotFound();
            }

            _context.Users.Remove(reader.User);
            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string userId)
        {
            var reader = await _context.Readers
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.UserId == userId)
                    ?? throw new ArgumentNullException(nameof(userId));

            var model = new EditReaderViewModel
            {
                Id = reader.UserId,
                FirstName = reader.User.FirstName,
                LastName = reader.User.LastName,
                UserName = reader.User.UserName,
                PhoneNumber = reader.User.PhoneNumber,
                Email = reader.User.Email
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditReaderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var reader = await _context.Readers
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.UserId == model.Id)
                    ?? throw new ArgumentNullException(nameof(model.Id));

                reader.UserId = model.Id;
                reader.User.FirstName = model.FirstName;
                reader.User.LastName = model.LastName;
                reader.User.UserName = model.UserName;
                reader.User.PhoneNumber = model.PhoneNumber;
                reader.User.Email = model.Email;

                var result = _context.Readers.Update(reader);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { userId = reader.Id });
            }

            return View(model);
        }
    }
}

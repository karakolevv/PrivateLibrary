using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Data;
using Microsoft.AspNetCore.Authorization;
using PrivateLibrary.Models.Pagination;
using PrivateLibrary.Models;

namespace PrivateLibrary.Controllers
{
    public class ReaderController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public ReaderController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                readers = readers.Where(readers => readers.User.UserName.Contains(search) || readers.User.FirstName.Contains(search) || readers.User.LastName.Contains(search) || readers.User.Email.Contains(search));
            }

            var filtered = await readers
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedReaders = new PaginatedList<Reader>(filtered, filtered.Count, pageNumber, pageSize);

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
                TempData["error"] = "Unsuccessful register!";
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
                TempData["success"] = "Successful login!";
                return RedirectToAction("Login", "Account");
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

        [Authorize(Roles = "Admin, Reader")]
        public async Task<IActionResult> TakenBooks(string readerId)
        {
            var takenBooks = await _context.TakenBooks
                .Where(tb => tb.ReaderId == readerId)
                .ToListAsync();

            return View(takenBooks);
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var reader = await _context.Readers
                    .Include(r => r.User)
                    .FirstOrDefaultAsync(r => r.UserId == userId)
                    ?? throw new ArgumentNullException(nameof(userId));

            if (reader is null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            _context.Users.Remove(reader.User);
            _context.Readers.Remove(reader);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));
        }


    }
}

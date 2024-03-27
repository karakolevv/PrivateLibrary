using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrivateLibrary.Data;
using PrivateLibrary.Data.Models;
using PrivateLibrary.Models;
using PrivateLibrary.Models.Pagination;

namespace PrivateLibrary.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public EmployeeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> Details(string userId)
        {
            var employee = await _context.Readers
                 .Include(u => u.User)
                 .FirstOrDefaultAsync(e => e.Id == userId)
                 ?? throw new ArgumentNullException(nameof(userId));

            return View(employee);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            EmployeeRegisterViewModel model = new();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(EmployeeRegisterViewModel model)
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
                UserName = model.UserName,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                var employee = new Employee
                {
                    MiddleName = model.MiddleName,
                    EGN = model.EGN,
                    HireDate = DateTime.Now,
                    IsAccountActive = true,
                    User = user,
                    UserId = user.Id
                };
                await _context.Employees.AddAsync(employee);
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

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll(string? search, int pageNumber = 1, int pageSize = 5)
        {
            var employees = _context.Employees
                .Include(u => u.User)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                ViewBag.Search = search;
                employees = employees.Where(employees => employees.User.UserName.Contains(search) || employees.User.FirstName.Contains(search) || employees.MiddleName.Contains(search) || employees.User.LastName.Contains(search) || employees.User.Email.Contains(search));
            }

            var filtered = await employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var paginatedEmployees = new PaginatedList<Employee>(filtered, filtered.Count, pageNumber, pageSize);

            return View(paginatedEmployees);
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

        public async Task<IActionResult> Delete(string userId)
        {
            var employee = await _context.Employees
                .Include(e => e.User)
                .FirstOrDefaultAsync(e => e.UserId == userId)
                ?? throw new ArgumentNullException(nameof(userId));

            if (employee is null)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

            _context.Users.Remove(employee.User);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GetAll));

        }
    }
}

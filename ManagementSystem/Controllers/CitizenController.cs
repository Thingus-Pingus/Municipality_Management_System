using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Models;
using ManagementSystem.Data;
namespace ManagementSystem.Controllers
{
    public class CitizenController : Controller
    {
        private readonly MunicipalityDbContext _context;

        public CitizenController(MunicipalityDbContext context)
        {
            _context = context;
        }

        // GET: Citizens
        public async Task<IActionResult> Index()
        {
            try
            {
                var citizens = await _context.Citizens.ToListAsync();
                return View(citizens);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching citizens: {ex.Message}");
                return View("Error", new ErrorView { Message = "Unable to retrieve citizen records." });
            }
        }

        // GET: Citizen/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.CitizenId == id);
                if (citizen == null)
                    return NotFound();

                return View(citizen);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching citizen details: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error retrieving citizen details." });
            }
        }

        // GET: Citizen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Citizen/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Citizen citizen)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(citizen);
            }

            try
            {
                citizen.RegistrationDate = DateTime.Now;
                _context.Add(citizen);
                await _context.SaveChangesAsync();

                Console.WriteLine("Citizen successfully added to the database.");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                ModelState.AddModelError("", "A database error occurred while adding the citizen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred.");
            }

            return View(citizen); // Prevents blank screen by returning the view
        }


        // GET: Citizen/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var citizen = await _context.Citizens.FindAsync(id);
                if (citizen == null)
                    return NotFound();

                return View(citizen);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching citizen for edit: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading citizen data for editing." });
            }
        }

        // POST: Citizen/Edit
        private bool CitizenExists(int id)
        {
            return _context.Citizens.Any(c => c.CitizenId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Citizen citizen)
        {
            if (id != citizen.CitizenId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(citizen);

            try
            {
                // Get the existing citizen from the database
                var existingCitizen = await _context.Citizens.FindAsync(id);
                if (existingCitizen == null)
                {
                    return NotFound();
                }

                // Preserve the RegistrationDate
                citizen.RegistrationDate = existingCitizen.RegistrationDate;

                _context.Entry(existingCitizen).CurrentValues.SetValues(citizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(citizen.CitizenId))
                    return NotFound();

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating citizen: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while updating the citizen.");
                return View(citizen);
            }
        }

        // GET: Citizen/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var citizen = await _context.Citizens.FirstOrDefaultAsync(c => c.CitizenId == id);
                if (citizen == null)
                    return NotFound();

                return View(citizen);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading citizen for deletion: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading citizen data for deletion." });
            }
        }

        // POST: Citizen/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var citizen = await _context.Citizens.FindAsync(id);
                if (citizen == null)
                {
                    Console.WriteLine($"Citizen with ID {id} not found.");
                    return NotFound();
                }
                Console.WriteLine($"Deleting Citizen with ID: {id}");
                _context.Citizens.Remove(citizen);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting citizen: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error deleting citizen record." });
            }
        }
    }
}

using ManagementSystem.Data;
using ManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        private readonly MunicipalityDbContext _context;

        public StaffController(MunicipalityDbContext context)
        {
            _context = context;
        }

        // GET: Staff
        public async Task<IActionResult> Index()
        {
            try
            {
                var staffs = await _context.Staffs.ToListAsync();
                return View(staffs);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff: {ex.Message}");
                return View("Error", new ErrorView { Message = "Unable to retrieve staff records." });
            }
        }

        // GET: Staff/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
                if (staff == null)
                    return NotFound();

                return View(staff);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff details: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error retrieving staff details." });
            }
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }
                ModelState.AddModelError("", "Please correct the errors and try again.");
                return View(staff);
            }

            try
            {
                _context.Add(staff);
                await _context.SaveChangesAsync();

                Console.WriteLine("Staff member successfully added to the database.");
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Database Update Error: {dbEx.InnerException?.Message ?? dbEx.Message}");
                ModelState.AddModelError("", "A database error occurred while adding the staff.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Error: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred.");
            }

            return View(staff); // Prevents blank screen by returning the view
        }


        // GET: Staff/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var staff = await _context.Staffs.FindAsync(id);
                if (staff == null)
                    return NotFound();

                return View(staff);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff for edit: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading staff data for editing." });
            }
        }

        // POST: Staff/Edit
        private bool StaffExists(int id)
        {
            return _context.Staffs.Any(s => s.StaffId == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Staff staff)
        {
            if (id != staff.StaffId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(staff);

            try
            {
                // Get the existing citizen from the database
                var existingstaff = await _context.Staffs.FindAsync(id);
                if (existingstaff == null)
                {
                    return NotFound();
                }

                _context.Entry(existingstaff).CurrentValues.SetValues(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StaffExists(staff.StaffId))
                    return NotFound();

                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating staff: {ex.Message}");
                ModelState.AddModelError("", "An error occurred while updating the staff.");
                return View(staff);
            }
        }

        // GET: Staff/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var staff = await _context.Staffs.FirstOrDefaultAsync(s => s.StaffId == id);
                if (staff == null)
                    return NotFound();

                return View(staff);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading staff for deletion: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading staff data for deletion." });
            }
        }

        // POST: Staff/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var staff = await _context.Staffs.FindAsync(id);
                if (staff == null)
                {
                    Console.WriteLine($"Staff with ID {id} not found.");
                    return NotFound();
                }
                Console.WriteLine($"Deleting Staff with ID: {id}");
                _context.Staffs.Remove(staff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting staff: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error deleting staff record." });
            }
        }
    }
}

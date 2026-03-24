using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementSystem.Models;
using ManagementSystem.Data;
using ManagementSystem.Dtos;


namespace ManagementSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly MunicipalityDbContext _context;

        // Constructor to initialize the database context
        public ReportController(MunicipalityDbContext context)
        {
            _context = context;
        }

        // GET: Reports - Fetches all reports along with related citizen data
        public async Task<IActionResult> Index()
        {
            try
            {
                var reports = await _context.Reports.Include(r => r.Citizen).ToListAsync();
                return View(reports);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching reports: {ex.Message}");
                return View("Error", new ErrorView { Message = "Unable to retrieve report records." });
            }
        }

        // GET: Report/Create - Displays the form for creating a new report
        public IActionResult Create()
        {
            ViewBag.Citizens = _context.Citizens.ToList();
            return View();
        }

        // POST: Report/Create - Handles form submission to create a new report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReportDto reportDto)
        {
        if (ModelState.IsValid)
        {
            var report = new Report()
            {
                SubmissionDate = reportDto.SubmissionDate,
                Status = reportDto.Status,
                CitizenId = reportDto.CitizenId,
                ReportType = reportDto.ReportType,
                Details = reportDto.Details,
            };
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(reportDto);
        }

        // GET: Report/Edit/{id} - Displays the form for editing a report
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            var reportDto = new ReportDto()
            {
                ReportId = report.ReportId,
                SubmissionDate = report.SubmissionDate,
                Status = report.Status,
                CitizenId = report.CitizenId,
                ReportType = report.ReportType,
                Details = report.Details
            };

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(reportDto);
        }

        // POST: Report/Edit/{id} - Handles form submission to update a report
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ReportDto reportDto)
        {
            if (id != reportDto.ReportId)
            {
                return BadRequest("Mismatched Report ID.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var report = await _context.Reports.FindAsync(id);
                    if (report == null)
                    {
                        return NotFound();
                    }

                    report.SubmissionDate = reportDto.SubmissionDate;
                    report.Status = reportDto.Status;
                    report.CitizenId = reportDto.CitizenId;
                    report.ReportType = reportDto.ReportType;
                    report.Details = reportDto.Details;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating report: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating the report.");
                }
            }

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(reportDto);
        }


        // GET: Citizen/Details - Details action to view report details
        public IActionResult Details(int id)
        {
            var report = _context.Reports.Include(r => r.Citizen).FirstOrDefault(r => r.ReportId == id);
            if (report == null)
            {
                return NotFound();
            }

            return View(report);
        }

        // GET: Report/Delete - Loads a report to confirm deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var report = await _context.Reports.Include(r => r.Citizen).FirstOrDefaultAsync(r => r.ReportId == id);
                if (report == null)
                    return NotFound();
                return View(report);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading report for deletion: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading report data for deletion." });
            }
        }

        // POST: Report/Delete - Confirms and deletes a report from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var report = await _context.Reports.FindAsync(id);
                if (report == null)
                {
                    Console.WriteLine($"Report with ID {id} not found.");
                    return NotFound();
                }
                Console.WriteLine($"Deleting Report with ID: {id}");
                _context.Reports.Remove(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting report: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error deleting report record." });
            }
        }
    }
}

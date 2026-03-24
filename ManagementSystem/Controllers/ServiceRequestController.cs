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
    public class ServiceRequestController : Controller
    {
        private readonly MunicipalityDbContext _context;

        // Constructor to initialize the database context
        public ServiceRequestController(MunicipalityDbContext context)
        {
            _context = context;
        }

        // GET: ServiceRequests - Fetches all service requests along with related citizen data
        public async Task<IActionResult> Index()
        {
            try
            {
                var serviceRequests = await _context.ServiceRequests.Include(s => s.Citizen).ToListAsync();
                return View(serviceRequests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching service requests: {ex.Message}");
                return View("Error", new ErrorView { Message = "Unable to retrieve service request records." });
            }
        }

        // GET: ServiceRequest/Create - Displays the form for creating a new service request
        public IActionResult Create()
        {
            ViewBag.Citizens = _context.Citizens.ToList();
            return View();
        }

        // POST: ServiceRequest/Create - Handles form submission to create a new service request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceRequestDto serviceRequestDto)
        {
            if (ModelState.IsValid)
            {
                var serviceRequest = new ServiceRequest()
                {
                    RequestDate = serviceRequestDto.RequestDate,
                    Status = serviceRequestDto.Status,
                    CitizenId = serviceRequestDto.CitizenId,
                    ServiceType = serviceRequestDto.ServiceType
                };
                _context.ServiceRequests.Add(serviceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(serviceRequestDto);
        }

        // GET: ServiceRequest/Edit/{id} - Displays the form for editing a service request
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceRequest = await _context.ServiceRequests.FindAsync(id);
            if (serviceRequest == null)
            {
                return NotFound();
            }

            var serviceRequestDto = new ServiceRequestDto()
            {
                RequestId = serviceRequest.RequestId,
                RequestDate = serviceRequest.RequestDate,
                Status = serviceRequest.Status,
                CitizenId = serviceRequest.CitizenId,
                ServiceType = serviceRequest.ServiceType
            };

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(serviceRequestDto);
        }

        // POST: ServiceRequest/Edit/{id} - Handles form submission to update a service request
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServiceRequestDto serviceRequestDto)
        {
            if (id != serviceRequestDto.RequestId)
            {
                return BadRequest("Mismatched Service Request ID.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var serviceRequest = await _context.ServiceRequests.FindAsync(id);
                    if (serviceRequest == null)
                    {
                        return NotFound();
                    }

                    serviceRequest.RequestDate = serviceRequestDto.RequestDate;
                    serviceRequest.Status = serviceRequestDto.Status;
                    serviceRequest.CitizenId = serviceRequestDto.CitizenId;
                    serviceRequest.ServiceType = serviceRequestDto.ServiceType;

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error updating service request: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating the service request.");
                }
            }

            ViewBag.Citizens = _context.Citizens.ToList();
            return View(serviceRequestDto);
        }

        // GET: ServiceRequest/Details - View details of a service request
        public IActionResult Details(int id)
        {
            var serviceRequest = _context.ServiceRequests.Include(s => s.Citizen).FirstOrDefault(s => s.RequestId == id);
            if (serviceRequest == null)
            {
                return NotFound();
            }
            return View(serviceRequest);
        }

        // GET: ServiceRequest/Delete - Loads a service request to confirm deletion
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            try
            {
                var serviceRequest = await _context.ServiceRequests.Include(s => s.Citizen).FirstOrDefaultAsync(s => s.RequestId == id);
                if (serviceRequest == null)
                    return NotFound();
                return View(serviceRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading service request for deletion: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error loading service request data for deletion." });
            }
        }

        // POST: ServiceRequest/Delete - Confirms and deletes a service request from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var serviceRequest = await _context.ServiceRequests.FindAsync(id);
                if (serviceRequest == null)
                {
                    Console.WriteLine($"Service Request with ID {id} not found.");
                    return NotFound();
                }
                Console.WriteLine($"Deleting Service Request with ID: {id}");
                _context.ServiceRequests.Remove(serviceRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting service request: {ex.Message}");
                return View("Error", new ErrorView { Message = "Error deleting service request record." });
            }
        }
    }

}

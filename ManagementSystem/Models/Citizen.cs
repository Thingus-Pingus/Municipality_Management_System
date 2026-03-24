using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models
{
    public class Citizen
    {
        [Key]
        public int CitizenId { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(50)]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public ICollection<Report> Reports { get; set; } = new List<Report>();
    }
}

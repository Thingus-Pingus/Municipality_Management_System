using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models
{
    public class Staff
    {
        [Key]
        public int StaffId { get; set; } 

        [Required]
        [StringLength(50)]
        public string FullName { get; set; } 

        [Required]
        [StringLength(50)]
        public string Position { get; set; } 

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
    }
}

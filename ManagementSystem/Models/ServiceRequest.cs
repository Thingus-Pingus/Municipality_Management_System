using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManagementSystem.Models
{
    public class ServiceRequest
    {
        [Key]
        public int RequestId { get; set; }

        [ForeignKey("Citizen")]
        public int CitizenId { get; set; }

        [Required]
        [StringLength(50)]
        public string ServiceType { get; set; }  

        [Required]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Pending"; 

        public Citizen Citizen { get; set; }

        public ICollection<Citizen> Citizens { get; set; }

    }
}

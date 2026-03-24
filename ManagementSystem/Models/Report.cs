using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagementSystem.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        // Navigation property
        public Citizen Citizen { get; set; }
        [ForeignKey("Citizen")]
        public int CitizenId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportType { get; set; }

        [Required]
        [StringLength(100)]
        public string Details { get; set; }

        [Required]
        public DateTime SubmissionDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(50)]
        public string Status { get; set; } = "Under Review";

    }
}

namespace ManagementSystem.Dtos
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public int CitizenId { get; set; }
        public string ReportType { get; set; }
        public string Details { get; set; }
        public DateTime SubmissionDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Under Review"; 
    }
}

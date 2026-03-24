namespace ManagementSystem.Dtos
{
    public class ServiceRequestDto
    {
        public int RequestId { get; set; }
        public int CitizenId { get; set; }
        public string ServiceType { get; set; }
        public DateTime RequestDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
    }
}

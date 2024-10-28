namespace infoproDigitalTechTask.DTOs
{
    public class QuoteDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime DateCreated { get; set; }
        public string Vrm { get; set; }
        public string VehicleDescription { get; set; }
        public int? Mileage { get; set; }
        public decimal OverallPrice { get; set; }
        public List<JobDTO> Jobs { get; set; }
    }

}

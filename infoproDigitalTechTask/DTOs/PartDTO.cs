namespace infoproDigitalTechTask.DTOs
{
    public class PartDTO
    {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public string PartType { get; set; } // 'Mechanical' or 'Fluid'
        public decimal TotalCost { get; set; }
    }
}

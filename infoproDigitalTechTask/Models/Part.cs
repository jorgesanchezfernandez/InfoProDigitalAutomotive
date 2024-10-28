using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infoproDigitalTechTask.Models
{
    public abstract class Part
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public int JobId { get; set; }
        public string PartNumber { get; set; }
        public string PartDescription { get; set; }
        public int Quantity { get; set; } // Quantity used on the job (different meaning for fluid vs. mechanical)
        public decimal UnitCost { get; set; } // Unit cost per item or per liter
        public Job Job { get; set; }
        public string PartType { get; set; }

        [NotMapped]
        public abstract decimal TotalCost { get; }
    }
}

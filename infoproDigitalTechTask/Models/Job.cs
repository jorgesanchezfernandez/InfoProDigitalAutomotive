using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infoproDigitalTechTask.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public int QuoteId { get; set; }

        [StringLength(10, ErrorMessage = "The job code cannot exceed {1} characters. ")]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public decimal LabourTime { get; set; }
        public decimal LabourRate { get; set; }
        public decimal? PriceOverride { get; set; } 
        public bool IsCustomerAuthorized { get; set; }
        public DateTime DateCreated { get; set; }
        public Quote Quote { get; set; }
        public List<Part> Parts { get; set; }


        [NotMapped]
        public decimal Price
        {
            get
            {
                decimal partsCost = 0;
                foreach (var part in Parts)
                {
                    partsCost += part.TotalCost;
                }

                return (LabourTime * LabourRate) + partsCost;
            }
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace infoproDigitalTechTask.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }

        [StringLength(10, ErrorMessage = "The job code cannot exceed {1} characters. ")]
        public string JobCode { get; set; }
        public string JobDescription { get; set; }
        public decimal LabourTime { get; set; }
        public decimal LabourRate { get; set; }
        public decimal? PriceOverride { get; set; }
        public bool IsCustomerAuthorized { get; set; }
        public decimal Price { get; set; }
        public List<PartDTO> Parts { get; set; }
    }

}

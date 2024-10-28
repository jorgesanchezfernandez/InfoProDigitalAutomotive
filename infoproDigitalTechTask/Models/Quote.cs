using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace infoproDigitalTechTask.Models
{
    public class Quote
    {
        [Key]
        public int Id { get; set; } 
        public string CustomerName { get; set; } 
        public string CustomerEmail { get; set; } 
        public DateTime DateCreated { get; set; } 
        public int CustomerId { get; set; } 
        public string Vrm { get; set; } 
        public string VehicleDescription { get; set; } 
        public int? Mileage { get; set; } 
        public List<Job> Jobs { get; set; }

        [NotMapped]
        public decimal OverallPrice
        {
            get
            {
                decimal jobsCost = 0;
                foreach (var job in Jobs)
                {
                    if (job.IsCustomerAuthorized)
                    {
                        if (job.PriceOverride.HasValue)
                        {
                            jobsCost += job.PriceOverride.Value;
                        }
                        else
                        {
                            jobsCost += job.Price;
                        }
                    }               
                }

                return jobsCost;
            }
        }
    }
}

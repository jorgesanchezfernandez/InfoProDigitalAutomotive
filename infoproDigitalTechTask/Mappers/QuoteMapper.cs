using System.Linq;
using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Mappers
{ 
    public static class QuoteMapper
    {
        public static QuoteDTO ToDTO(Quote quote)
        {
            return new QuoteDTO
            {
                Id = quote.Id,
                CustomerName = quote.CustomerName,
                CustomerEmail = quote.CustomerEmail,
                DateCreated = quote.DateCreated,
                Vrm = quote.Vrm,
                VehicleDescription = quote.VehicleDescription,
                Mileage = quote.Mileage,
                OverallPrice = quote.OverallPrice,
                Jobs = quote.Jobs?.Select(JobMapper.ToDTO).ToList()
            };
        }

        public static Quote ToModel(QuoteDTO quoteDTO)
        {
            return new Quote
            {
                Id = quoteDTO.Id,
                CustomerName = quoteDTO.CustomerName,
                CustomerEmail = quoteDTO.CustomerEmail,
                DateCreated = quoteDTO.DateCreated,
                Vrm = quoteDTO.Vrm,
                VehicleDescription = quoteDTO.VehicleDescription,
                Mileage = quoteDTO.Mileage,
                Jobs = quoteDTO.Jobs?.Select(JobMapper.ToModel).ToList()
            };
        }
    }

}

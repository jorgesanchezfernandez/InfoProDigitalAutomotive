using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Mappers
{
    public static class PartMapper
    {
        public static PartDTO ToDTO(Part part)
        {
            return new PartDTO
            {
                Id = part.Id,
                PartNumber = part.PartNumber,
                PartDescription = part.PartDescription,
                Quantity = part.Quantity,
                UnitCost = part.UnitCost,
                TotalCost = part.TotalCost,
                PartType = part.PartType
            };
        }

        public static Part ToModel(PartDTO partDTO)
        {
            if (partDTO.PartType == "Mechanical")
            {
                return new MechanicalPart
                {
                    Id = partDTO.Id,
                    PartNumber = partDTO.PartNumber,
                    PartDescription = partDTO.PartDescription,
                    Quantity = partDTO.Quantity,
                    UnitCost = partDTO.UnitCost,
                    PartType = "Mechanical"
                };
            }
            else if (partDTO.PartType == "Fluid")
            {
                return new FluidPart
                {
                    Id = partDTO.Id,
                    PartNumber = partDTO.PartNumber,
                    PartDescription = partDTO.PartDescription,
                    Quantity = partDTO.Quantity,
                    UnitCost = partDTO.UnitCost,
                    PartType = "Fluid"
                };
            }

            return null;
        }
    }
}

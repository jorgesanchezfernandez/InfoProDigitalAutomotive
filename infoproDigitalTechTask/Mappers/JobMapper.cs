using System.Linq;
using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Mappers
{
    public static class JobMapper
    {
        public static JobDTO ToDTO(Job job)
        {
            return new JobDTO
            {
                Id = job.Id,
                JobCode = job.JobCode,
                JobDescription = job.JobDescription,
                LabourTime = job.LabourTime,
                LabourRate = job.LabourRate,
                PriceOverride = job.PriceOverride,
                IsCustomerAuthorized = job.IsCustomerAuthorized,
                Price = job.Price,
                Parts = job.Parts?.Select(PartMapper.ToDTO).ToList()
            };
        }

        public static Job ToModel(JobDTO jobDTO)
        {
            return new Job
            {
                Id = jobDTO.Id,
                JobCode = jobDTO.JobCode,
                JobDescription = jobDTO.JobDescription,
                LabourTime = jobDTO.LabourTime,
                LabourRate = jobDTO.LabourRate,
                PriceOverride = jobDTO.PriceOverride,
                IsCustomerAuthorized = jobDTO.IsCustomerAuthorized,
                Parts = jobDTO.Parts?.Select(PartMapper.ToModel).ToList()
            };
        }
    }

}

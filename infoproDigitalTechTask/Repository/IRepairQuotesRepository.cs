using infoproDigitalTechTask.DTOs;

namespace infoproDigitalTechTask.Repository
{
    public interface IRepairQuotesRepository
    {
        Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync();
        Task<QuoteDTO> GetQuoteDetailsAsync(int quoteId);
        Task<IEnumerable<QuoteDTO>> GetQuotesByCustomerEmailAsync(string email);
        Task<JobDTO> AddJobToQuoteAsync(int quoteId, JobDTO jobDto);
        Task<PartDTO> AddPartToJobAsync(int jobId, PartDTO partDto);
    }
}

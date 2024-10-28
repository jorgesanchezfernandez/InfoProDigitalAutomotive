using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Mappers;
using infoproDigitalTechTask.Models.DBContext;
using infoproDigitalTechTask.Models;
using Microsoft.EntityFrameworkCore;

namespace infoproDigitalTechTask.Repository
{
    public class RepairQuotesRepository : IRepairQuotesRepository
    {
        private readonly QuoteManagementDBContext _context;

        public RepairQuotesRepository(QuoteManagementDBContext context)
        {
            _context = context;
        }

        // Obtener todas las cotizaciones
        public async Task<IEnumerable<QuoteDTO>> GetAllQuotesAsync()
        {
            var quotes = await _context.Quote
                .Include(q => q.Jobs)
                    .ThenInclude(j => j.Parts)
                .ToListAsync();

            return quotes.Select(QuoteMapper.ToDTO).ToList();
        }

        // Obtener los detalles de una cotización específica
        public async Task<QuoteDTO> GetQuoteDetailsAsync(int quoteId)
        {
            var quote = await _context.Quote
                .Include(q => q.Jobs)
                    .ThenInclude(j => j.Parts)
                .FirstOrDefaultAsync(q => q.Id == quoteId);

            if (quote == null) return null;

            return QuoteMapper.ToDTO(quote);
        }

        // Obtener todas las cotizaciones de un cliente según el correo electrónico
        public async Task<IEnumerable<QuoteDTO>> GetQuotesByCustomerEmailAsync(string email)
        {
            var quotes = await _context.Quote
                .Where(q => q.CustomerEmail == email)
                .Include(q => q.Jobs)
                    .ThenInclude(j => j.Parts)
                .ToListAsync();

            return quotes.Select(QuoteMapper.ToDTO).ToList();
        }

        // Agregar un nuevo trabajo a una cotización
        public async Task<JobDTO> AddJobToQuoteAsync(int quoteId, JobDTO jobDto)
        {
            var quote = await _context.Quote.FindAsync(quoteId);
            if (quote == null) throw new KeyNotFoundException($"Quote with ID {quoteId} not found.");

            var job = JobMapper.ToModel(jobDto);
            job.QuoteId = quoteId;

            _context.Job.Add(job);
            await _context.SaveChangesAsync();

            return JobMapper.ToDTO(job);
        }

        // Agregar una nueva parte a un trabajo
        public async Task<PartDTO> AddPartToJobAsync(int jobId, PartDTO partDto)
        {
            var job = await _context.Job.FindAsync(jobId);
            if (job == null) throw new KeyNotFoundException($"Job with ID {jobId} not found.");

            Part part;
            if (partDto.PartType == "Mechanical")
            {
                part = new MechanicalPart
                {
                    PartNumber = partDto.PartNumber,
                    PartDescription = partDto.PartDescription,
                    Quantity = partDto.Quantity,
                    UnitCost = partDto.UnitCost,
                    DateCreated = DateTime.Now,
                    JobId = jobId
                };
            }
            else if (partDto.PartType == "Fluid")
            {
                part = new FluidPart
                {
                    PartNumber = partDto.PartNumber,
                    PartDescription = partDto.PartDescription,
                    Quantity = partDto.Quantity,
                    UnitCost = partDto.UnitCost,
                    DateCreated = DateTime.Now,
                    JobId = jobId
                };
            }
            else
            {
                throw new ArgumentException("Invalid part type specified.");
            }

            _context.Part.Add(part);
            await _context.SaveChangesAsync();

            return PartMapper.ToDTO(part);
        }
    }
}

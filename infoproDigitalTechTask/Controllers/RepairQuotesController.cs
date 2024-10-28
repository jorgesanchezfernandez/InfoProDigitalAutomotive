using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Models;
using infoproDigitalTechTask.Repository;
using Microsoft.AspNetCore.Mvc;

namespace infoproDigitalTechTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairQuotesController : ControllerBase
    {
        private readonly IRepairQuotesRepository _repairQuotesRepository;

        public RepairQuotesController(IRepairQuotesRepository repairQuoteRepository)
        {
            _repairQuotesRepository = repairQuoteRepository;
        }

        [HttpGet("Quote/GetAll")]
        public async Task<ActionResult<IEnumerable<QuoteDTO>>> GetAllQuotes()
        {
            var quotes = await _repairQuotesRepository.GetAllQuotesAsync();

            return Ok(quotes);
        }

        [HttpGet("Quote/{id}/Details")]
        public async Task<ActionResult<QuoteDTO>> GetQuoteDetails(int id)
        {
            var quote = await _repairQuotesRepository.GetQuoteDetailsAsync(id);

            if (quote == null) return NotFound();

            return Ok(quote);
        }

        [HttpGet("Quote/Customer/Email/{email}")]
        public async Task<ActionResult<IEnumerable<QuoteDTO>>> GetQuotesByCustomerEmail(string email)
        {
            var quotes = await _repairQuotesRepository.GetQuotesByCustomerEmailAsync(email);

            return Ok(quotes);
        }

        [HttpPost("Quote/{quoteId}/AddJob")]
        public async Task<ActionResult<JobDTO>> AddJobToQuote(int quoteId, [FromBody] JobDTO jobDto)
        {
            try
            {
                var job = await _repairQuotesRepository.AddJobToQuoteAsync(quoteId, jobDto);
                return CreatedAtAction(nameof(GetQuoteDetails), new { id = quoteId }, job);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Quote/Jobs/{jobId}/AddPart")]
        public async Task<ActionResult<PartDTO>> AddPartToJob(int jobId, [FromBody] PartDTO partDto)
        {
            try
            {
                var part = await _repairQuotesRepository.AddPartToJobAsync(jobId, partDto);
                return CreatedAtAction(nameof(GetQuoteDetails), new { id = jobId }, part);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

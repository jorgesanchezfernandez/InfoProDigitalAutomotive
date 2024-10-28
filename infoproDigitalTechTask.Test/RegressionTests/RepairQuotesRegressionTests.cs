using infoproDigitalTechTask.Controllers;
using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace infoproDigitalTechTask.Test.RegressionTests
{
    public class RepairQuotesControllerRegressionTests
    {
        private readonly Mock<IRepairQuotesRepository> _mockRepo;
        private readonly RepairQuotesController _controller;

        public RepairQuotesControllerRegressionTests()
        {
            _mockRepo = new Mock<IRepairQuotesRepository>();
            _controller = new RepairQuotesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllQuotes_ShouldReturnAllQuotes_WhenQuotesExist()
        {
            // Arrange
            var quotes = new List<QuoteDTO>
            {
                new QuoteDTO { Id = 1, CustomerName = "Rafael Nadal", CustomerEmail = "rafael.nadal@rafaelnadal.com" },
                new QuoteDTO { Id = 2, CustomerName = "Novak Djokovic", CustomerEmail = "novak.djokovic@novakdjokovic.com" }
            };
            _mockRepo.Setup(repo => repo.GetAllQuotesAsync()).ReturnsAsync(quotes);

            // Act
            var result = await _controller.GetAllQuotes();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedQuotes = Assert.IsType<List<QuoteDTO>>(okResult.Value);
            Assert.Equal(2, returnedQuotes.Count);
        }

        [Fact]
        public async Task GetQuoteDetails_ShouldReturnQuoteWithJobsAndParts_WhenQuoteExists()
        {
            // Arrange
            var quote = new QuoteDTO
            {
                Id = 1,
                CustomerName = "Rafael Nadal",
                CustomerEmail = "rafael.nadal@rafaelnadal.com",
                Jobs = new List<JobDTO>
                {
                    new JobDTO
                    {
                        Id = 1, JobCode = "J001", JobDescription = "Engine Repair",
                        Parts = new List<PartDTO> { new PartDTO { PartNumber = "P001", PartDescription = "Bolt", Quantity = 5 } }
                    }
                }
            };
            _mockRepo.Setup(repo => repo.GetQuoteDetailsAsync(1)).ReturnsAsync(quote);

            // Act
            var result = await _controller.GetQuoteDetails(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedQuote = Assert.IsType<QuoteDTO>(okResult.Value);
            Assert.Single(returnedQuote.Jobs);
            Assert.Single(returnedQuote.Jobs[0].Parts);
        }

        [Fact]
        public async Task GetQuotesByCustomerEmail_ShouldReturnQuotesForSpecificCustomer()
        {
            // Arrange
            var email = "rafael.nadal@rafaelnadal.com";
            var quotes = new List<QuoteDTO>
            {
                new QuoteDTO { Id = 1, CustomerName = "Rafael Nadal", CustomerEmail = email },
                new QuoteDTO { Id = 2, CustomerName = "Rafael Nadal", CustomerEmail = email }
            };
            _mockRepo.Setup(repo => repo.GetQuotesByCustomerEmailAsync(email)).ReturnsAsync(quotes);

            // Act
            var result = await _controller.GetQuotesByCustomerEmail(email);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedQuotes = Assert.IsType<List<QuoteDTO>>(okResult.Value);
            Assert.Equal(2, returnedQuotes.Count);
            Assert.All(returnedQuotes, q => Assert.Equal(email, q.CustomerEmail));
        }

        [Fact]
        public async Task AddJobToQuote_ShouldReturnNewJob_WhenJobIsAddedSuccessfully()
        {
            // Arrange
            var quoteId = 1;
            var jobDto = new JobDTO { JobCode = "J002", JobDescription = "Transmission repair" };
            _mockRepo.Setup(repo => repo.AddJobToQuoteAsync(quoteId, jobDto)).ReturnsAsync(new JobDTO { Id = 1, JobCode = "J002" });

            // Act
            var result = await _controller.AddJobToQuote(quoteId, jobDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedJob = Assert.IsType<JobDTO>(createdResult.Value);
            Assert.Equal("J002", returnedJob.JobCode);
        }

        [Fact]
        public async Task AddJobToQuote_ShouldReturnNotFound_WhenQuoteDoesNotExist()
        {
            // Arrange
            var quoteId = 1;
            var jobDto = new JobDTO { JobCode = "J002", JobDescription = "Transmission repair" };
            _mockRepo.Setup(repo => repo.AddJobToQuoteAsync(quoteId, jobDto)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.AddJobToQuote(quoteId, jobDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddPartToJob_ShouldReturnNewPart_WhenPartIsAddedSuccessfully()
        {
            // Arrange
            var jobId = 1;
            var partDto = new PartDTO { PartNumber = "P002", PartDescription = "Engine Oil" };
            _mockRepo.Setup(repo => repo.AddPartToJobAsync(jobId, partDto)).ReturnsAsync(new PartDTO { PartNumber = "P002" });

            // Act
            var result = await _controller.AddPartToJob(jobId, partDto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedPart = Assert.IsType<PartDTO>(createdResult.Value);
            Assert.Equal("P002", returnedPart.PartNumber);
        }

        [Fact]
        public async Task AddPartToJob_ShouldReturnNotFound_WhenJobDoesNotExist()
        {
            // Arrange
            var jobId = 1;
            var partDto = new PartDTO { PartNumber = "P002", PartDescription = "Engine Oil" };
            _mockRepo.Setup(repo => repo.AddPartToJobAsync(jobId, partDto)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.AddPartToJob(jobId, partDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}

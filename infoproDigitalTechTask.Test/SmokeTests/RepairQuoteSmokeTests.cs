using infoproDigitalTechTask.Controllers;
using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace infoproDigitalTechTask.Test.SmokeTests
{
    public class RepairQuotesControllerSmokeTests
    {
        private readonly Mock<IRepairQuotesRepository> _mockRepo;
        private readonly RepairQuotesController _controller;

        public RepairQuotesControllerSmokeTests()
        {
            _mockRepo = new Mock<IRepairQuotesRepository>();
            _controller = new RepairQuotesController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllQuotes_ShouldReturnOkResult()
        {
            // Arrange
            _mockRepo.Setup(repo => repo.GetAllQuotesAsync()).ReturnsAsync(new List<QuoteDTO>());

            // Act
            var result = await _controller.GetAllQuotes();

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetQuoteDetails_ShouldReturnOkResult_WhenQuoteExists()
        {
            // Arrange
            int quoteId = 1;
            _mockRepo.Setup(repo => repo.GetQuoteDetailsAsync(quoteId)).ReturnsAsync(new QuoteDTO());

            // Act
            var result = await _controller.GetQuoteDetails(quoteId);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetQuoteDetails_ShouldReturnNotFoundResult_WhenQuoteDoesNotExist()
        {
            // Arrange
            int quoteId = 1;
            _mockRepo.Setup(repo => repo.GetQuoteDetailsAsync(quoteId)).ReturnsAsync((QuoteDTO)null);

            // Act
            var result = await _controller.GetQuoteDetails(quoteId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task GetQuotesByCustomerEmail_ShouldReturnOkResult()
        {
            // Arrange
            string email = "customer@customer.com";
            _mockRepo.Setup(repo => repo.GetQuotesByCustomerEmailAsync(email)).ReturnsAsync(new List<QuoteDTO>());

            // Act
            var result = await _controller.GetQuotesByCustomerEmail(email);

            // Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddJobToQuote_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            int quoteId = 1;
            var jobDto = new JobDTO { JobCode = "J001", JobDescription = "Test job" };
            _mockRepo.Setup(repo => repo.AddJobToQuoteAsync(quoteId, jobDto)).ReturnsAsync(new JobDTO());

            // Act
            var result = await _controller.AddJobToQuote(quoteId, jobDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task AddJobToQuote_ShouldReturnNotFoundResult_WhenQuoteDoesNotExist()
        {
            // Arrange
            int quoteId = 1;
            var jobDto = new JobDTO { JobCode = "J001", JobDescription = "Test job" };
            _mockRepo.Setup(repo => repo.AddJobToQuoteAsync(quoteId, jobDto)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.AddJobToQuote(quoteId, jobDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }

        [Fact]
        public async Task AddPartToJob_ShouldReturnCreatedAtActionResult()
        {
            // Arrange
            int jobId = 1;
            var partDto = new PartDTO { PartNumber = "P001", PartDescription = "Test Part" };
            _mockRepo.Setup(repo => repo.AddPartToJobAsync(jobId, partDto)).ReturnsAsync(new PartDTO());

            // Act
            var result = await _controller.AddPartToJob(jobId, partDto);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task AddPartToJob_ShouldReturnNotFoundResult_WhenJobDoesNotExist()
        {
            // Arrange
            int jobId = 1;
            var partDto = new PartDTO { PartNumber = "P001", PartDescription = "Test Part" };
            _mockRepo.Setup(repo => repo.AddPartToJobAsync(jobId, partDto)).Throws<KeyNotFoundException>();

            // Act
            var result = await _controller.AddPartToJob(jobId, partDto);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }
}

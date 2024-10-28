using infoproDigitalTechTask.Models;
using infoproDigitalTechTask.DTOs;
using infoproDigitalTechTask.Models.DBContext;
using infoproDigitalTechTask.Repository;
using Microsoft.EntityFrameworkCore;

namespace infoproDigitalTechTask.Test.IntegrationTests
{
    public class RepairQuotesRepositoryTests : IDisposable
    {
        private readonly QuoteManagementDBContext _context;
        private readonly RepairQuotesRepository _repository;
        private readonly List<Quote> _quotes;

        public RepairQuotesRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<QuoteManagementDBContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;
            _context = new QuoteManagementDBContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _repository = new RepairQuotesRepository(_context);
            _quotes = new List<Quote>
            {
                new Quote
                {
                    CustomerName = "Rafael Nadal", CustomerEmail = "rafael.nadal@rafanadal.com", DateCreated = DateTime.Now,
                    CustomerId = 1, Vrm = "XYZ1234", VehicleDescription = "Toyota Corolla 2020", Mileage = 25000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "ENG001", JobDescription = "Engine Repair", LabourTime = 5.0m, LabourRate = 80.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH001", PartDescription = "Engine Part A",
                                    Quantity = 2, UnitCost = 100.0m, DateCreated = DateTime.Now, PartType = "Mechanical" },
                                new FluidPart { PartNumber = "FLUID001", PartDescription = "Engine Oil",
                                    Quantity = 5000, UnitCost = 20.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        },
                        new Job
                        {
                            JobCode = "BRK001", JobDescription = "Brake Replacement", LabourTime = 3.0m, LabourRate = 70.0m,
                            IsCustomerAuthorized = true, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                        }
                    }
                },
                new Quote
                {
                    CustomerName = "Novak Djokovic", CustomerEmail = "novak.djokovic@novakdjokovic.com", DateCreated = DateTime.Now,
                    CustomerId = 2, Vrm = "ABC5678", VehicleDescription = "Honda Civic 2018", Mileage = 45000,
                    Jobs = new List<Job>
                    {
                        new Job
                        {
                            JobCode = "SERV001", JobDescription = "Full Service", LabourTime = 4.0m, LabourRate = 85.0m,
                            IsCustomerAuthorized = false, DateCreated = DateTime.Now,
                            Parts = new List<Part>
                            {
                                new FluidPart { PartNumber = "FLUID002", PartDescription = "Coolant Fluid",
                                    Quantity = 2000, UnitCost = 15.0m, DateCreated = DateTime.Now, PartType = "Fluid" }
                            }
                        }
                    }
                }
            };
        }

        public void Dispose()
        {
            _context.Database.CloseConnection();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllQuotesAsync_ShouldReturnAllQuotes()
        {
            // Arrange
            _context.Quote.AddRange(_quotes);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllQuotesAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetQuoteDetailsAsync_ShouldReturnQuoteWithJobsAndParts()
        {
            // Arrange
            _context.Quote.Add(_quotes[1]);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetQuoteDetailsAsync(_quotes[1].Id);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result.Jobs);
            Assert.Single(result.Jobs.First().Parts);
        }


        [Fact]
        public async Task GetQuotesByCustomerEmailAsync_ShouldReturnQuotesForCustomer()
        {
            // Arrange
            _context.Quote.AddRange(_quotes);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetQuotesByCustomerEmailAsync("rafael.nadal@rafanadal.com");

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task AddJobToQuoteAsync_ShouldAddJobToSpecifiedQuote()
        {
            // Arrange
            _context.Quote.Add(_quotes[0]);
            await _context.SaveChangesAsync();

            var jobDto = new JobDTO
            {
                JobCode = "ELC002",
                JobDescription = "Battery Replacement",
                LabourTime = 2.0m,
                LabourRate = 75.0m,
                IsCustomerAuthorized = true,
                Parts = new List<PartDTO>
                {
                    new PartDTO { PartNumber = "MECH003", PartDescription = "Battery",
                        Quantity = 1, UnitCost = 120.0m, PartType = "Mechanical" }
                }
            };

            // Act
            var result = await _repository.AddJobToQuoteAsync(_quotes[0].Id, jobDto);
            var updatedQuote = await _context.Quote.Include(q => q.Jobs).FirstOrDefaultAsync(q => q.Id == _quotes[0].Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, updatedQuote.Jobs.Count);
            Assert.NotNull(updatedQuote.Jobs.FirstOrDefault(x => x.JobCode == "ELC002").JobCode);
        }

        [Fact]
        public async Task AddPartToJobAsync_ShouldAddPartToSpecifiedJob()
        {
            // Arrange
            _context.Quote.Add(_quotes[0]);
            await _context.SaveChangesAsync();

            var partDto = new PartDTO
            {
                
                PartNumber = "MECH003",
                PartDescription = "Battery",
                Quantity = 1,
                UnitCost = 120.0m,
                PartType = "Mechanical"
            };

            // Act
            var result = await _repository.AddPartToJobAsync(_quotes[0].Jobs[0].Id, partDto);
            var updatedJob = await _context.Job.Include(j => j.Parts).FirstOrDefaultAsync(j => j.Id == _quotes[0].Jobs[0].Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, updatedJob.Parts.Count);
            Assert.NotNull( updatedJob.Parts.FirstOrDefault(x => x.PartNumber == "MECH003").PartNumber);
        }
    }
}

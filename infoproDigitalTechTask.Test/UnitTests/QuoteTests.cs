using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Test.UnitTests
{
    public class QuoteTests
    {
        [Fact]
        public void OverallPrice_ShouldCalculateSumOfJobsPrices()
        {
            // Arrange
            var quote = new Quote
            {
                Jobs = new List<Job>
                {
                    new Job { IsCustomerAuthorized = true, Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                    },
                    new Job { IsCustomerAuthorized = false, Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                    },
                    new Job { PriceOverride = 150, IsCustomerAuthorized = false }
                }
            };

            // Act
            var overallPrice = quote.OverallPrice;

            // Assert
            Assert.Equal(200, overallPrice);
        }

        [Fact]
        public void OverallPriceOverrided_ShouldCalculateSumOfJobsPrices()
        {
            // Arrange
            var quote = new Quote
            {
                Jobs = new List<Job>
                {
                    new Job { PriceOverride = 100, IsCustomerAuthorized = true, Parts = new List<Part>
                            {
                                new MechanicalPart { PartNumber = "MECH002", PartDescription = "Brake Pads",
                                    Quantity = 4, UnitCost = 50.0m, DateCreated = DateTime.Now, PartType = "Mechanical" }
                            }
                    },
                    new Job { PriceOverride = 200, IsCustomerAuthorized = true },
                    new Job { PriceOverride = 150, IsCustomerAuthorized = false }
                }
            };

            // Act
            var overallPrice = quote.OverallPrice;

            // Assert
            Assert.Equal(300, overallPrice);
        }

        [Fact]
        public void OverallPrice_ShouldBeZero_WhenNoJobs()
        {
            // Arrange
            var quote = new Quote
            {
                Jobs = new List<Job>()
            };

            // Act
            var overallPrice = quote.OverallPrice;

            // Assert
            Assert.Equal(0, overallPrice);
        }
    }
}
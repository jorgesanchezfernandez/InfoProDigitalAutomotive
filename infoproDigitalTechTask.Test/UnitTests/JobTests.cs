using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Test.UnitTests
{
    public class JobTests
    {
        [Fact]
        public void Price_ShouldBePriceCalculated_WhenOverrideIsSet()
        {
            // Arrange
            var job = new Job
            {
                LabourTime = 5,
                LabourRate = 50,
                PriceOverride = 300,
                IsCustomerAuthorized = true,
                Parts = new List<Part>()
            };

            // Act
            var price = job.Price;

            // Assert
            Assert.Equal(250, price);
        }

        [Fact]
        public void Price_ShouldBeCalculated_WhenNoPriceOverride()
        {
            // Arrange
            var job = new Job
            {
                LabourTime = 2,
                LabourRate = 50,
                IsCustomerAuthorized = true,
                Parts = new List<Part>
                {
                    new MechanicalPart { Quantity = 2, UnitCost = 20 },
                    new MechanicalPart { Quantity = 1, UnitCost = 10 }
                }
            };

            // Act
            var price = job.Price;

            // Assert
            Assert.Equal(150, price);
        }

        [Fact]
        public void Price_ShouldBeCalculated_WhenNotAuthorized()
        {
            // Arrange
            var job = new Job
            {
                LabourTime = 2,
                LabourRate = 50,
                PriceOverride = 200,
                IsCustomerAuthorized = false,
                Parts = new List<Part>()
            };

            // Act
            var price = job.Price;

            // Assert
            Assert.Equal(100, price);
        }
    }
}

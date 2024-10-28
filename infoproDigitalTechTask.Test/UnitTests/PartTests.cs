using infoproDigitalTechTask.Models;

namespace infoproDigitalTechTask.Test.UnitTests
{
    public class PartTests
    {
        [Fact]
        public void MechanicalPart_TotalCost_ShouldCalculateCorrectly()
        {
            // Arrange
            var part = new MechanicalPart
            {
                Quantity = 5,
                UnitCost = 10
            };

            // Act
            var totalCost = part.TotalCost;

            // Assert
            Assert.Equal(50, totalCost);
        }

        [Fact]
        public void FluidPart_TotalCost_ShouldCalculateCorrectly()
        {
            // Arrange
            var part = new FluidPart
            {
                Quantity = 1500,
                UnitCost = 20
            };

            // Act
            var totalCost = part.TotalCost;

            // Assert
            Assert.Equal(30, totalCost);
        }

        [Fact]
        public void MechanicalPart_TotalCost_ShouldBeZero_WhenQuantityIsZero()
        {
            // Arrange
            var part = new MechanicalPart
            {
                Quantity = 0,
                UnitCost = 10
            };

            // Act
            var totalCost = part.TotalCost;

            // Assert
            Assert.Equal(0, totalCost);
        }

        [Fact]
        public void FluidPart_TotalCost_ShouldBeZero_WhenQuantityIsZero()
        {
            // Arrange
            var part = new FluidPart
            {
                Quantity = 0,
                UnitCost = 20
            };

            // Act
            var totalCost = part.TotalCost;

            // Assert
            Assert.Equal(0, totalCost);
        }
    }
}

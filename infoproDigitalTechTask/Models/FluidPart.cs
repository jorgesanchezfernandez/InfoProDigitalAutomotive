namespace infoproDigitalTechTask.Models
{
    public class FluidPart : Part
    {
        public override decimal TotalCost => (Quantity / 1000m) * UnitCost; // Price calculated by converting ml to liters
    }
}

namespace infoproDigitalTechTask.Models
{
    public class MechanicalPart : Part
    {
        public override decimal TotalCost => Quantity * UnitCost;
    }
}

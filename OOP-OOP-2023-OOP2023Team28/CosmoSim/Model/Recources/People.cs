namespace CosmoSim.Model.Recources;

public class People: Resource
{
    private double FoodConsumption { get; set; }
    private double WaterConsumption { get; set; }
    private double OxygenConsumption { get; set; }

    
    public People(double size)
    {
        FoodConsumption = size;
        WaterConsumption = size;
        OxygenConsumption = size;
        this.Size = size;
        this.MaxAmountOnShip = 100;
        this.MaxAmountOnPlanet = 100;
    }
    
    public override void SetSize(double size)
    {
        this.Size = size;
    }
}
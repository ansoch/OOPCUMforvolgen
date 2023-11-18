namespace CosmoSim.Model.Recources;

public class Oxygen: Resource
{
    public Oxygen(double size)
    {
        this.MaxAmountOnPlanet = 100;
        this.MaxAmountOnShip = 100;
        this.Size = size;
    }
    
    public override void SetSize(double size)
    {
        this.Size = size;
    }
}
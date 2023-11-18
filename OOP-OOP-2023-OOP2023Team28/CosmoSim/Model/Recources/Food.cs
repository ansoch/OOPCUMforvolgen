namespace CosmoSim.Model.Recources;

public class Food: Resource
{
    public Food(double size)
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
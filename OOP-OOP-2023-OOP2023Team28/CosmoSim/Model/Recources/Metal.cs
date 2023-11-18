namespace CosmoSim.Model.Recources;

public class Metal: Resource
{
    public Metal(double size)
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
namespace CosmoSim.Model.Recources;

public abstract class Resource
{

    public double Size { get; protected set; }
    public double MaxAmountOnShip { get; protected set; }
    public double MaxAmountOnPlanet { get; protected set; }

    public abstract void SetSize(double size);
}
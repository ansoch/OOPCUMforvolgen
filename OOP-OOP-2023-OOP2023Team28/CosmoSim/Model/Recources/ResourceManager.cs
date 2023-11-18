using CosmoSim.Model.Recources;

namespace CosmoSim.Model; 

public class ResourceManager {
    private int _level;
    private readonly double _initialResourceMultiplier;

    public ResourceManager(int level, double initialResourceMultiplier, double baseResourceValue)
    {
        this._level = level;
        this._initialResourceMultiplier = initialResourceMultiplier;
        this.BaseResourceValue = baseResourceValue;
    }

    public Recources.Resources GetRequiredBuildResources()
    {
        var value = this.BaseResourceValue * Math.Pow(_initialResourceMultiplier, _level);
        return CreateResources(value);
    }

    public double BaseResourceValue { get; set; }

    public Recources.Resources GetRequiredUpgradeResources()
    {
        var value = (this.BaseResourceValue + 4) * Math.Pow(_initialResourceMultiplier, _level);
        return CreateResources(value);
    }

    public Recources.Resources GetReceivedUseResources()
    {
        var value = (this.BaseResourceValue - 3) * Math.Pow(_initialResourceMultiplier, _level);
        return CreateResources(value);
    }

    public Recources.Resources GetReceivedDestroyResources()
    {
        var value = (this.BaseResourceValue + 1) * Math.Pow(_initialResourceMultiplier, _level);
        return CreateResources(value);
    }

    public Recources.Resources CreateResources(double value)
    {
        return new Recources.Resources(value, value, value, value, value, value, value, value);
    }
}
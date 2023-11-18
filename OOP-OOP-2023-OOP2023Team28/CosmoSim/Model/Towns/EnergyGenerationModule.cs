using CosmoSim.Model.Towns.Entities;

namespace CosmoSim.Model.Towns;

public abstract class EnergyGenerationModule: Town
{
    public double PowerGeneration { get; protected set;  }
    public double MaxPowerGeneration { get; protected set; }
    public double BaseGenerationValue { get; protected set; }


    public abstract void ChangePowerGeneration(double powerGeneration, bool isIncrease);
}
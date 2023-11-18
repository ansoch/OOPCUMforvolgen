using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class RecoveryRecourcesFactory : Town {
    public double RecourceGeneration { get; set; }
    public double BaseGenerationValue { get; private set; }

    public double MaxPowerGeneration { get; private set; }

    public RecoveryRecourcesFactory(
        Planet planet,
        int level
    )
    {
        Planet = planet;
        Level = level;
        
        IsWorking = true;
        
        TownType = TownType.RecoveryRecourcesFactory;

        InitialResourceMultiplier = 1.2;
        ResourceMultiplier = 1.0;
        BaseResourceValue = ResourceCalculator.CalculateBaseValue(level);
        
        MaxPowerGeneration = GetBaseMaxPowerGeneration();
        BaseGenerationValue = GetBasePowerGeneration();
        
        Manager = new ResourceManager(level, InitialResourceMultiplier, BaseResourceValue);


        RequiredBuildResources = Manager.GetRequiredBuildResources();
        RequiredUpgradeResources = Manager.GetRequiredUpgradeResources();
        ReceivedDestroyResources = Manager.GetReceivedDestroyResources();
        ReceivedUseResources = Manager.GetReceivedUseResources();
    }



    public override void UpgradeTown()
    {
        Level++;
        ResourceMultiplier *= 1.1;
    }

    public override void StartWork()
    {
        IsWorking = true;
    }

    public override void StopWork()
    {
        IsWorking = false;
    }


    public void IncreaseRecoveryRecourceGeneration(double value)
    {
        double increase;

        if (RecourceGeneration + value <= MaxPowerGeneration) {
            increase = value + RecourceGeneration - BaseGenerationValue;
            RecourceGeneration += increase;
        }
        else {
            increase = MaxPowerGeneration - RecourceGeneration;
            RecourceGeneration = MaxPowerGeneration;
        }
        double increasePercentage = increase / BaseGenerationValue;
        ReceivedUseResources = Manager.CreateResources(BaseResourceValue * (increasePercentage + 1));
    }

    public void ReduceRecoveryRecourceGeneration(double value)
    {
        double increase;

        if (RecourceGeneration - value > BaseGenerationValue) {
            increase = RecourceGeneration - value - BaseGenerationValue;
            RecourceGeneration -= value;
        }
        else {
            increase = 0;
            RecourceGeneration = BaseGenerationValue;
        }
        double increasePercentage = increase / BaseGenerationValue;
        ReceivedUseResources = Manager.CreateResources(BaseResourceValue * (increasePercentage + 1));
    }
    
    protected double GetBaseMaxPowerGeneration()
    {
        return 100 * Math.Pow(1.1, Level - 1);
    }
    protected double GetBasePowerGeneration()
    {
        return 10 * Math.Pow(1.1, Level - 1);
    }
    
}
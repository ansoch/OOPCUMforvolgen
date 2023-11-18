using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns.EnergyGenerationTowns;

public class ResourceExtractionModule : EnergyGenerationModule {
    public ResourceExtractionModule(
        Planet planet,
        int level
    )
    {
        Planet = planet;
        Level = level;
        
        IsWorking = true;
        
        TownType = TownType.ResourceExtractionModule;
        InitialResourceMultiplier = 1.2;
        ResourceMultiplier = 1.0;

        PowerGeneration = BaseGenerationValue;
        
        Manager = new ResourceManager(level, InitialResourceMultiplier, BaseResourceValue);


        RequiredBuildResources = Manager.GetRequiredBuildResources();
        RequiredUpgradeResources = Manager.GetRequiredUpgradeResources();
        ReceivedDestroyResources = Manager.GetReceivedDestroyResources();
        ReceivedUseResources = Manager.GetReceivedUseResources();
        
        MaxPowerGeneration = PowerCalculator.GetMaxGeneration(level);


        BaseGenerationValue = PowerCalculator.GetBaseGeneration(level);
        BaseResourceValue = ResourceCalculator.CalculateBaseValue(level);

    }

    public override void UpgradeTown()
    {
        Level++;
        ResourceMultiplier *= 1.1;
        PowerGeneration *= 1.1;
        BaseGenerationValue *= 1.1;
    }

    public override void StartWork()
    {
        IsWorking = true;
    }

    public override void StopWork()
    {
        IsWorking = false;
    }

    public override void ChangePowerGeneration(double powerGeneration, bool isIncrease)
    {
        double powerChange;
    
        if (isIncrease)
        {
            if (PowerGeneration + powerGeneration <= MaxPowerGeneration)
            {
                powerChange = powerGeneration;
                PowerGeneration += powerChange;
            }
            else
            {
                powerChange = MaxPowerGeneration - PowerGeneration;
                PowerGeneration = MaxPowerGeneration;
            }
        }
        else
        {
            if (PowerGeneration - powerGeneration > BaseGenerationValue)
            {
                powerChange = -powerGeneration;
                PowerGeneration += powerChange;
            }
            else
            {
                powerChange = PowerGeneration - BaseGenerationValue;
                PowerGeneration = BaseGenerationValue;
            }
        }
    
        double changePercentage = powerChange / BaseGenerationValue;
        ReceivedUseResources = Manager.CreateResources(BaseResourceValue * (changePercentage + 1));
    }
}
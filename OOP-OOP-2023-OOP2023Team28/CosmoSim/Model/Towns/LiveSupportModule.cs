using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class LiveSupportModule : Town {

    public double OxygenGeneration { get; private set; }
    public double FoodGeneration { get; private set; }
    public double WaterGeneration { get; private set; }
    public double BaseGenerationValue { get; private set; }
    public double MaxCapacity { get; private set; }

    public LiveSupportModule(
        Planet planet,
        int level
    )
    {
        Planet = planet;
        Level = level;
        
        IsWorking = true;

        TownType = TownType.SpaceshipRepairModule;

        InitialResourceMultiplier = 1.2;
        ResourceMultiplier = 1.0;
        BaseResourceValue = ResourceCalculator.CalculateBaseValue(level);
        BaseGenerationValue = GetBaseGenerationValue();
        
        Manager = new ResourceManager(level, InitialResourceMultiplier, BaseResourceValue);
        

        RequiredBuildResources = Manager.GetRequiredBuildResources();
        RequiredUpgradeResources = Manager.GetRequiredUpgradeResources();
        ReceivedDestroyResources = Manager.GetReceivedDestroyResources();
        ReceivedUseResources = Manager.GetReceivedUseResources();

        OxygenGeneration = BaseResourceValue;
        FoodGeneration = BaseResourceValue;
        WaterGeneration = BaseResourceValue;

        MaxCapacity = GetBaseMaxGeneration();
    }

    private double GetBaseGenerationValue()
    {
        return 10 * Math.Pow(1.1, Level - 1);
    }

    private double GetBaseMaxGeneration()
    {
        return 100 * Math.Pow(1.1, Level - 1);
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

    public void IncreaseOxygen(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Oxygen, value, true);
    }

    public void ReduceOxygen(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Oxygen, value, false);
    }


    public void IncreaseWater(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Water, value, true);
    }

    public void ReduceWater(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Oxygen, value, false);
    }

    public void IncreaseFood(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Food, value, true);
    }

    public void ReduceFood(double value)
    {
        ChangeResourceGeneration(GenerationResourceType.Oxygen, value, false);
    }

    private void ChangeResourceGeneration(GenerationResourceType type, double size, bool isIncrease)
    {
        double increase;

        double resourceSize = type switch {
            GenerationResourceType.Water  => WaterGeneration,
            GenerationResourceType.Food   => FoodGeneration,
            GenerationResourceType.Oxygen => OxygenGeneration,
            _                             => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
        
        if (isIncrease) {
            if (resourceSize + size <= MaxCapacity) {
                increase = resourceSize + size - BaseGenerationValue;
                resourceSize += size;
            }
            else {
                increase = MaxCapacity - resourceSize;
                resourceSize = MaxCapacity;
            }
        }
        else {
            if (resourceSize - size > BaseGenerationValue) {
                increase = resourceSize - size - BaseGenerationValue;
                resourceSize -= size;
            }
            else {
                increase = 0;
                resourceSize = BaseGenerationValue;
            }
        }

        switch (type) {
            case GenerationResourceType.Water:
                WaterGeneration = resourceSize;
                break;
            case GenerationResourceType.Food:
                FoodGeneration = resourceSize;
                break;
            case GenerationResourceType.Oxygen:
                OxygenGeneration = resourceSize;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }

        double increasePercentage = increase / BaseGenerationValue;
        ReceivedUseResources = Manager.CreateResources(BaseResourceValue * (1 + increasePercentage));
    }
    
}
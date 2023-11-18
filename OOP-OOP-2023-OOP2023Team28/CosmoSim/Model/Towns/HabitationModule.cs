using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class HabitationModule : Town {
    private int MaxCapacity { get; set; }
    private int CurrentCapacity { get; set; }

    public HabitationModule(
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

        Manager = new ResourceManager(level, InitialResourceMultiplier, BaseResourceValue);

        RequiredBuildResources = Manager.GetRequiredBuildResources();
        RequiredUpgradeResources = Manager.GetRequiredUpgradeResources();
        ReceivedDestroyResources = Manager.GetReceivedDestroyResources();
        ReceivedUseResources = Manager.GetReceivedUseResources();
        MaxCapacity = 100;
        CurrentCapacity = 30;
    }

    public override void UpgradeTown()
    {
        Level++;
        ResourceMultiplier *= 1.1;
        MaxCapacity += MaxCapacity / 10;
    }

    public override void StartWork()
    {
        IsWorking = true;
    }

    public override void StopWork()
    {
        IsWorking = false;
    }

    public void AddResident()
    {
        if (CurrentCapacity < MaxCapacity) {
            CurrentCapacity++;
        }
        // изменить расход ресурсов на поддержание модуля
    }

    public void RemoveResident()
    {
        if (CurrentCapacity > 0) {
            CurrentCapacity--;
        }
        // изменить расход ресурсов на поддержание модуля
    }

}
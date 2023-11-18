using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class PartInstallerPort : Town {
    public PartInstallerPort(
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

    public bool DockSpaceship()
    {
        Recources.Resources resources = Manager.CreateResources(10);
        if (!Planet.IsEnoughResources(resources)) return false;
        Planet.UpdateAvailableResources(resources);
        return true;
    }
}
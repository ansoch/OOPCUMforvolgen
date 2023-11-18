using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class GroundLab : Town {
    private double _newResearchMultiplier;
    private Recources.Resources _newResearchResources;

    public GroundLab(
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

        _newResearchMultiplier = 10.0;
        _newResearchResources = Manager.CreateResources(10.0);
    }

    public override void UpgradeTown()
    {
        Level++;
        ResourceMultiplier *= 1.1;
        _newResearchMultiplier *= 0.9;
        _newResearchResources = Manager.CreateResources(_newResearchMultiplier);
    }

    public override void StartWork()
    {
        IsWorking = true;
    }

    public override void StopWork()
    {
        IsWorking = false;
    }

    public Recources.Resources GetResourcesForNewResearch()
    {
        return _newResearchResources;
    }

    public bool StartResearchNewUpgrade()
    {
        Recources.Resources resources = Manager.CreateResources(10);
        if (!Planet.IsEnoughResources(resources)) return false;
        Planet.UpdateAvailableResources(_newResearchResources, ResourceMultiplier);
        return true;
    }

}
﻿using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns;

public class TradeRecourceModule : Town {
    public List<int> CurrentPrices { get; private set; }
    public Dictionary<string, int> AvailableResources { get; private set; }

    public TradeRecourceModule(
        Planet planet,
        int level
    )
    {
        Planet = planet;
        Level = level;
        
        IsWorking = true;

        TownType = TownType.TradeResourceModule;

        InitialResourceMultiplier = 1.2;
        ResourceMultiplier = 1.0;
        BaseResourceValue = ResourceCalculator.CalculateBaseValue(level);
        
        Manager = new ResourceManager(level, InitialResourceMultiplier, BaseResourceValue);


        RequiredBuildResources = Manager.GetRequiredBuildResources();
        RequiredUpgradeResources = Manager.GetRequiredUpgradeResources();
        ReceivedDestroyResources = Manager.GetReceivedDestroyResources();
        ReceivedUseResources = Manager.GetReceivedUseResources();

        CurrentPrices = new List<int>();
        AvailableResources = new Dictionary<string, int>();
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

    public void BuyRecource(string resource, int amount)
    {
        if (!AvailableResources.ContainsKey(resource) || AvailableResources[resource] < amount) return;
        AvailableResources[resource] -= amount;
        AddResource(resource, amount);
    }

    public void SellRecource(string resource, int amount)
    {
        if (!AvailableResources.ContainsKey(resource) || AvailableResources[resource] < amount) return;
        AvailableResources[resource] -= amount;
        AddResource(resource, amount);
    }
    private void AddResource(string resource, int amount)
    {
        if (AvailableResources.ContainsKey(resource)) {
            AvailableResources[resource] += amount;
        }
        else {
            AvailableResources.Add(resource, amount);
        }
    }

}
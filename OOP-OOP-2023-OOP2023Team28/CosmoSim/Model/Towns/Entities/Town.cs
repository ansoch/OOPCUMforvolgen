using CosmoSim.Model.Galaxy.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Towns.Entities;

public abstract class Town {
    public TownType TownType { get; protected set; }
    public int Level { get; protected set; }
    public Planet Planet { get; protected set; }
    
    public bool IsWorking { get; protected set; }
    
    public double InitialResourceMultiplier { get; protected set; }
    public double ResourceMultiplier { get; protected set; }
    public double BaseResourceValue { get; protected set; }
    
    public ResourceManager Manager { get; protected set; }
    
    public Model.Recources.Resources RequiredBuildResources { get; protected set; }

    public Model.Recources.Resources RequiredUpgradeResources { get; protected set; }
    public Model.Recources.Resources ReceivedDestroyResources { get; protected set; }
    public Model.Recources.Resources ReceivedUseResources { get; protected set; }

    public abstract void UpgradeTown();
    
    public abstract void StartWork();
    public abstract void StopWork();
    
    
}
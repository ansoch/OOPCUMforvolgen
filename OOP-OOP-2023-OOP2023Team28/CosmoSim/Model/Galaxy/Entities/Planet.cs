using CosmoSim.Model.Towns;
using CosmoSim.Model.Towns.EnergyGenerationTowns;
using CosmoSim.Model.Towns.Entities;
using CosmoSim.Model.Towns.Enums;

namespace CosmoSim.Model.Galaxy.Entities;

public class Planet {
    public Recources.Resources Resources { get; private set; }
    public List<Town> Towns { get; private set; }
    public int Radius { get; private set; }
    public PlanetCharacteristics PlanetCharacteristics { get; private set; }
    public int MaxTownsAmount { get; private set; }
    public Coordinates Coordinates { get; private set; }

    public Planet(
        int radius,
        PlanetCharacteristics planetCharacteristics,
        int maxTownsAmount,
        Recources.Resources resources,
        Coordinates coordinates
    )
    {
        Radius = radius;
        PlanetCharacteristics = planetCharacteristics;
        MaxTownsAmount = maxTownsAmount;
        Resources = resources;
        Coordinates = coordinates;

        Towns = GetInitialTownsList();
    }


    public void Update()
    {
        foreach (var town in Towns) {
            var receivedUseResources = town.ReceivedUseResources;
            
            if (IsEnoughResources(receivedUseResources)) {
                if (!town.IsWorking) {
                    town.StartWork();
                }
                UpdateAvailableResources(receivedUseResources);
            } else {
                town.StopWork();
            }
        }
    }

    public bool BuildTown(TownType townType, int level)
    {
        if (Towns.Count >= MaxTownsAmount) return false;
        var planet = this;
        var newTown = GetNewTown(townType, level, ref planet);
        if (!IsEnoughResources(newTown.RequiredBuildResources, newTown.ResourceMultiplier)) return false;
        Towns.Add(newTown);
        UpdateAvailableResources(newTown.RequiredBuildResources, newTown.ResourceMultiplier);
        return true;
    }

    public bool UpgradeTown(TownType townType, int level)
    {
        var townToUpgrade = Towns.Find(t => t.TownType == townType && t.Level == level);
        if (townToUpgrade == null || !Towns.Contains(townToUpgrade)) return false;

        var planet = this;
        var newTown = GetNewTown(townType, level + 1, ref planet);

        if (!IsEnoughResources(newTown.RequiredBuildResources, newTown.ResourceMultiplier)) return false;
        Towns.Remove(townToUpgrade);
        Towns.Add(newTown);
        UpdateAvailableResources(newTown.RequiredUpgradeResources, newTown.ResourceMultiplier);
        return true;
    }

    public bool DestroyTown(TownType townType, int level)
    {
        var townToDestroy = Towns.Find(t => t.TownType == townType && t.Level == level);
        if (townToDestroy == null || !Towns.Contains(townToDestroy)) return false;
        Towns.Remove(townToDestroy);
        UpdateAvailableResources(townToDestroy.ReceivedDestroyResources, townToDestroy.ResourceMultiplier);
        return true;
    }

    public void UpdateAvailableResources(Recources.Resources requiredResources, double resMultiplier = 1.0)
    {
        this.Resources.UpdateResources(
            this.Resources.Energy.Size - requiredResources.Energy.Size * resMultiplier,
            this.Resources.Food.Size - requiredResources.Food.Size * resMultiplier,
            this.Resources.Metal.Size - requiredResources.Metal.Size * resMultiplier,
            this.Resources.Water.Size - requiredResources.Water.Size * resMultiplier,
            this.Resources.ShipRecoveryResources.Size -
            requiredResources.ShipRecoveryResources.Size * resMultiplier,
            this.Resources.Oxygen.Size - requiredResources.Oxygen.Size * resMultiplier,
            this.Resources.People.Size - requiredResources.People.Size * resMultiplier,
            this.Resources.RocketFuel.Size - requiredResources.RocketFuel.Size * resMultiplier
        );
    }

    public bool IsEnoughResources(Recources.Resources resources, double resMultiplier = 1.0)
    {
        return (!(this.Resources.Water.Size < resources.Water.Size * resMultiplier))
               && (!(this.Resources.Food.Size < resources.Food.Size * resMultiplier))
               && (!(this.Resources.Metal.Size < resources.Metal.Size * resMultiplier))
               && (!(this.Resources.Energy.Size < resources.Energy.Size * resMultiplier))
               && (!(this.Resources.People.Size < resources.People.Size * resMultiplier))
               && (!(this.Resources.Water.Size < resources.Water.Size * resMultiplier));
    }

    private Town GetNewTown(TownType townType, int level, ref Planet planet)
    {
        return townType switch {
            TownType.GroundLab                => new GroundLab(planet, level),
            TownType.RecoveryRecourcesFactory => new RecoveryRecourcesFactory(planet, level),
            TownType.HabitationModule         => new HabitationModule(planet, level),
            TownType.NuclearReactor           => new NuclearReactor(planet, level),
            TownType.SolarPanel               => new SolarPanel(planet, level),
            TownType.ResourceExtractionModule => new ResourceExtractionModule(planet, level),
            TownType.LifeSupportModule        => new LiveSupportModule(planet, level),
            TownType.TradeResourceModule      => new TradeRecourceModule(planet, level),
            TownType.PartInstallerPort        => new PartInstallerPort(planet, level),
            TownType.SpaceshipRepairModule    => new SpaceshipRepairModule(planet, level),
            _                                 => throw new ArgumentOutOfRangeException(nameof(townType), townType, null)
        };
    }

    private List<Town> GetInitialTownsList()
    {
        var towns = new List<Town>();

        var townsCount = new Random().Next(0, MaxTownsAmount);
        var planet = this;
        
        for (var i = 0; i < townsCount; i++) {
            var townType = (TownType)new Random().Next(0, 10);
            var level = new Random().Next(1, 4);
            var newTown = GetNewTown(townType, level, ref planet);
            towns.Add(newTown);
        }

        return towns;
    }
}
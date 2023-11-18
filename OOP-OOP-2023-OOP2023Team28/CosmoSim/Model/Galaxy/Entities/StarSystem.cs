using CosmoSim.Model.Galaxy.Enums;

namespace CosmoSim.Model.Galaxy.Entities;

public class StarSystem {
    public StarSystemName Name { get; private set; }
    public Coordinates Coordinates { get; private set; }
    public int Radius { get; private set; }
    public int PlanetsCount { get; private set; }
    public List<Planet> Planets { get; private set; }
    public List<MeteorCluster> MeteorClusters { get; private set; }
    private List<Coordinates> _occupiedCoordinates;
    


    public StarSystem(StarSystemName name, Coordinates coordinates)
    {
        this.Name = name;
        Coordinates = coordinates;
        _occupiedCoordinates = new List<Coordinates>();
        this.PlanetsCount = GetPlanetsCount();
        this.Radius = GetRadius();
        this.Planets = GetGeneratedPlanetsList();
        this.MeteorClusters = GetGeneratedMeteorClustersList();
    }

    public void Update()
    {
        foreach (var planet in Planets) {
            planet.Update();
        }
    }


    private int GetPlanetsCount()
    {
        return this.Name switch {
            StarSystemName.Alhena => 3,
            StarSystemName.Alzirr => 4,
            StarSystemName.Castor => 5,
            StarSystemName.Propus => 5,
            StarSystemName.Wasat  => 4,
            _                     => 0
        };
    }

    private int GetRadius()
    {
        return this.Name switch {
            StarSystemName.Alhena => 20,
            StarSystemName.Alzirr => 20,
            StarSystemName.Castor => 19,
            StarSystemName.Propus => 15,
            StarSystemName.Wasat  => 18,
            _                     => 0
        };
    }

    private List<Planet> GetGeneratedPlanetsList()
    {
        var planets = new List<Planet>();
        for (var i = 0; i < PlanetsCount; i++) {
            planets.Add(
                new Planet(
                    GetRandomRadius(),
                    GetRandomPlanetCharacteristics(),
                    GetRandomTownsAmount(),
                    GetRandomResources(),
                    GetRandomCoordinates()
                )
            );
        }

        return planets;
    }

    private List<MeteorCluster> GetGeneratedMeteorClustersList()
    {
        var meteorClusters = new List<MeteorCluster>();
        for (var i = 0; i < PlanetsCount; i++) {
            meteorClusters.Add(
                new MeteorCluster(
                    GetRandomCoordinates()
                ));
        }

        return meteorClusters;
    }

    private static int GetRandomRadius()
    {
        return new Random().Next(1, 5);
    }

    private static PlanetCharacteristics GetRandomPlanetCharacteristics()
    {
        AtmosphereType atmosphereType = (AtmosphereType)new Random().Next(0, 4);
        SurfaceType surfaceType = (SurfaceType)new Random().Next(0, 5);
        TemperatureType temperatureType = (TemperatureType)new Random().Next(0, 3);
        CoreType coreType = (CoreType)new Random().Next(0, 3);
        return new PlanetCharacteristics(
            surfaceType,
            atmosphereType,
            temperatureType,
            coreType
        );
    }

    private static int GetRandomTownsAmount()
    {
        return new Random().Next(1, 15);
    }

    private static Recources.Resources GetRandomResources()
    {
        var res = new double[8];
        for (var i = 0; i < 8; i++) {
            res[i] = new Random().Next(0, 100);
        }

        return new Recources.Resources(res[0], res[1], res[2], res[3], res[4], res[5], res[6], res[7]);
    }

    private Coordinates GetRandomCoordinates()
    {
        Random random = new Random();
        while (true) {
            var x = random.Next(-this.Radius, this.Radius);
            var y = random.Next(-this.Radius, this.Radius);
            
            var coordinates = new Coordinates(x, y);
            if (_occupiedCoordinates.Contains(coordinates)) continue;
            _occupiedCoordinates.Add(coordinates);
            return coordinates;
        }
    }
    
    
}
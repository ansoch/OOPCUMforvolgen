using CosmoSim.Model.Galaxy.Enums;

namespace CosmoSim.Model.Galaxy.Entities;

public static class Galaxy {
    public static int Radius { get; set; } = 249;
    public static List<StarSystem> StarSystems { get; private set; } = GetGeneratedStarSystemsList();

    /*
    static Galaxy()
    {
        Radius = 249;
        StarSystems = GetGeneratedStarSystemsList();
    }
    */

    private static List<StarSystem> GetGeneratedStarSystemsList()
    {
        StarSystems = new List<StarSystem>();
        foreach (StarSystemName starSystemName in Enum.GetValues(typeof(StarSystemName))) {
            Coordinates coordinates = GetCoordinatesForStarSystem(starSystemName);
            StarSystem starSystem = new StarSystem(starSystemName, coordinates);
            StarSystems.Add(starSystem);
        }

        return StarSystems;
    }

    private static Coordinates GetCoordinatesForStarSystem(StarSystemName starSystemName)
    {
        return starSystemName switch {
            StarSystemName.Alhena => new Coordinates(90, 200),
            StarSystemName.Alzirr => new Coordinates(230, 300),
            StarSystemName.Castor => new Coordinates(400, 250),
            StarSystemName.Propus => new Coordinates(450, 70),
            StarSystemName.Wasat  => new Coordinates(210, 400),
            _                     => new Coordinates(100, 330)
        };
    }
}
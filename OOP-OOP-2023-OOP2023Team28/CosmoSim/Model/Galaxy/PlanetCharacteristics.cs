using CosmoSim.Model.Galaxy.Enums;

namespace CosmoSim;

public class PlanetCharacteristics
{
    private SurfaceType SurfaceType { get; set; }
    private AtmosphereType AtmosphereType { get; set; }
    private TemperatureType TemperatureType { get; set; }
    private CoreType CoreType { get; set; }
    
    public PlanetCharacteristics(
        SurfaceType surfaceType,
        AtmosphereType atmosphereType,
        TemperatureType temperatureType,
        CoreType coreType
    )
    {
        this.SurfaceType = surfaceType;
        this.AtmosphereType = atmosphereType;
        this.TemperatureType = temperatureType;
        this.CoreType = coreType;
    }
}
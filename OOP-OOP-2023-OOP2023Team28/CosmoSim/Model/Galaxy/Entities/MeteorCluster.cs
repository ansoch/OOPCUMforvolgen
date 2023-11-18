namespace CosmoSim.Model.Galaxy.Entities; 

public class MeteorCluster {
    public Coordinates Coordinates { get; private set; }
    public MeteorCluster(Coordinates coordinates)
    {
        this.Coordinates = coordinates;
    }
}
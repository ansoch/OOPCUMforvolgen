namespace CosmoSim.Model.Galaxy; 

public class Coordinates {
    public int X { get; protected set; }
    public int Y { get; protected set; }

    public Coordinates()
    {
        this.X = 0;
        this.Y = 0;
    }
    public Coordinates(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    
    public void SetCoordinates(int x, int y) {
        this.X = x;
        this.Y = y;
    }
    public static Coordinates TransformCoordinates(Coordinates coordinates)
    {
        int newX = coordinates.X + 27;
        int newY = coordinates.Y + 20;
        return new Coordinates(newX, newY);
    }
}
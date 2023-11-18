namespace CosmoSim.Model.Recources;

public class Resources
{

    public Energy Energy { get; private set; }
    public Food Food { get; private set; }
    public Metal Metal { get; private set; }
    public Water Water { get; private set; }
    public ShipRecoveryResources ShipRecoveryResources { get; private set; }
    public Oxygen Oxygen { get; private set; }
    public People People { get; private set; }
    public RocketFuel RocketFuel { get; private set; }
    

    public Resources(
        double energy, 
        double food, 
        double metal, 
        double water, 
        double shipRecoveryResources,
        double oxygen,
        double people,
        double rocketFuel
        ) {
        this.Energy = new Energy(energy);
        this.Food = new Food(food);
        this.Metal = new Metal(metal);
        this.Water = new Water(water);
        this.ShipRecoveryResources = new ShipRecoveryResources(shipRecoveryResources);
        this.Oxygen = new Oxygen(oxygen);
        this.People = new People(people);
        this.RocketFuel = new RocketFuel(rocketFuel);
    }

    
    public void UpdateResources(
        double energy, 
        double food, 
        double metal, 
        double water, 
        double shipRecoveryResources,
        double oxygen,
        double people,
        double rocketFuel
        ) {
        this.Energy.SetSize(energy > 0 ? energy : 0);
        this.Food.SetSize(food > 0 ? food : 0);
        this.Metal.SetSize(metal > 0 ? metal : 0);
        this.Water.SetSize(water > 0 ? water : 0);
        this.ShipRecoveryResources.SetSize(shipRecoveryResources > 0 ? shipRecoveryResources : 0);
        this.Oxygen.SetSize(oxygen > 0 ? oxygen : 0);
        this.People.SetSize(people > 0 ? people : 0);
        this.RocketFuel.SetSize(rocketFuel > 0 ? rocketFuel : 0);
        
    }
    
}
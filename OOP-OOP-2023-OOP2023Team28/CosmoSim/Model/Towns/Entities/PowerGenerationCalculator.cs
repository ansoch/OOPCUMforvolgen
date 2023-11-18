namespace CosmoSim.Model.Towns.EnergyGenerationTowns; 

public static class PowerCalculator {
    public static double GetMaxGeneration(int level, double maxValue = 50)
    {
        return maxValue * Math.Pow(1.1, level - 1);
    }

    public static double GetBaseGeneration(int level, double baseValue = 5)
    {
        return baseValue * Math.Pow(1.1, level - 1);
    }
}

namespace CosmoSim.Model.Towns; 

public static class ResourceCalculator  {
    public static double CalculateBaseValue(int level, int baseValue = 15)
    {
        return baseValue * Math.Pow(1.1, level - 1);
    }
}
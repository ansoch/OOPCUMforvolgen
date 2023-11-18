using System.Collections.Generic;
using CosmoSim.Model.Galaxy;
using CosmoSim.Model.Galaxy.Entities;

namespace CosmoSim {
    public static class Constants
    {
        public const int fuelCostConst = 1;
        public const int startFuelConst = 250;
        public const int startMoneyConst = 5000;
        public const int startOxygenConst = 5000;
        public const int timeOfLife = 40;
    }


    public enum Resource
    {
        money,
        fuel,
        metall,
        food,
        water,
        oxygen
    }

    public enum Frame
    {
        typical,
        strong,
        weak,
        flexible,
        pressure
    }

    public enum Skin
    {
        typical,
        weak,
        flexible,
        pressure
    }

    public enum LSS
    {
        typical,
        antiHot,
        antiCold,
        weak
    }

    public class Spaceship {
        public StarSystem CurrentSystem { get; private set; } = Galaxy.StarSystems[0];

        private Coordinates position;
        private EngineType engine;
        private Hold inventory;
        private Frame frameType;
        private Skin skinType;
        private LSS lssType;

        public Spaceship()
        {
            position = new Coordinates();
            //position.PlaceInCentre();
            engine = new ClassicEngine();
            inventory = new Hold();
            frameType = Frame.weak;
            skinType = Skin.weak;
            lssType = LSS.weak;
        }

        public Coordinates GetCoordinates()
        {
            return position;
        }
        /*
        public Coordinates[] GetPossibleMoves()
        {
            return engine.MakeMoves(position, inventory.GetResource(Resource.fuel));
        }

        public Coordinates[] GetPossibleSuperMoves()
        {
            return engine.MakeSuperMoves(position, inventory.GetResource(Resource.fuel),
                inventory.GetResource(Resource.accelerator));
        }
        */

        public bool Fly(int x, int y)
        {
            if (engine.CheckMove(x, y, position, GetResource(Resource.fuel)) == true)
            {
                SetResource((Math.Abs(x - position.X) + Math.Abs(y - position.Y)) * Constants.fuelCostConst * -1, Resource.fuel);

                foreach (Planet planet in CurrentSystem.Planets)
                {
                    Coordinates planetCoordinates = Coordinates.TransformCoordinates(planet.Coordinates);
                    if (planetCoordinates.X == x && planetCoordinates.Y == y && position.X == x && position.Y == y)
                    {
                        ReplenishFuel();
                        break;
                    }

                }

                position.SetCoordinates(x, y);
                Console.WriteLine("x ", position.X, " y ", position.Y);
                
                foreach (MeteorCluster meteor in CurrentSystem.MeteorClusters)
                {
                    Coordinates meteorCoordinates = Coordinates.TransformCoordinates(meteor.Coordinates);
                    if (meteorCoordinates.X == x && meteorCoordinates.Y == y)
                    {
                        SetResource(10, Resource.metall);
                        break;
                    }

                }

                return true;
            }
            Console.WriteLine("x ", position.X, " y ", position.Y);
            return false;
        }


        public void FlyOnTipicalPosition(int x, int y, int currentMove)
        {
            position.SetCoordinates(x, y);
            inventory.SetResource(
                -1 * (Math.Abs(position.X - x) + Math.Abs(position.Y - y)) *
                Constants.fuelCostConst, Resource.fuel);
            inventory.Rotting(currentMove);
        }

        public void ChangePosition(int x, int y)
        {
            position.SetCoordinates(x, y);
        }

        public void SetResource(int size, Resource type)
        {
            inventory.SetResource(size, type);
        }

        public bool AddFood(int size, int currentMove)
        {
            return inventory.AddFood(size, currentMove);
        }

        public bool TakeFood(int size)
        {
            return inventory.TakeFood(size);
        }

        public int GetResource(Resource type)
        {
            return inventory.GetResource(type);
        }

        public void ChangeEngine(EngineType newEngine)
        {
            engine = newEngine;
        }

        public void ChangeFrame(Frame newFrame)
        {
            frameType = newFrame;
        }

        public Frame GetFrameType()
        {
            return frameType;
        }

        public void ChangeSkin(Skin newSkin)
        {
            skinType = newSkin;
        }

        public Skin GetSkinType()
        {
            return skinType;
        }

        public void ChangeLSS(LSS newLSS)
        {
            lssType = newLSS;
        }

        public LSS GetLSSType()
        {
            return lssType;
        }

        public void InToTheStarSystem(StarSystem starSystem)
        {
            CurrentSystem = starSystem;
            position = new Coordinates(0, 0);
        }

        public void Landing()
        {
            // хз чо сюда писатьц
        }

        private void ReplenishFuel()
        {
            SetResource(10, Resource.fuel);
            SetResource(-5, Resource.money);
        }
    }

    public abstract class EngineType
    {
        public abstract bool CheckMove(int x, int y, Coordinates currentPosition, int fuel);

        public abstract string GetType();
    }

    public class ClassicEngine : EngineType
    {
        public override bool CheckMove(int x, int y, Coordinates currentPosition, int fuel)
        {
            if (((x == currentPosition.X) && (Math.Abs(y - currentPosition.Y) <= fuel / Constants.fuelCostConst))
             || ((y == currentPosition.Y) && (Math.Abs(x - currentPosition.X) <= fuel / Constants.fuelCostConst)))
            {
                return true;
            }
            return false;
        }
        public override string GetType()
        {
            return "Classic";
        }
    }

    public class DiagonaleEngine : EngineType
    {
        public override bool CheckMove(int x, int y, Coordinates currentPosition, int fuel)
        {
            if ((Math.Abs(y - currentPosition.Y) == Math.Abs(x - currentPosition.X))
                && (Math.Abs(y - currentPosition.Y) + Math.Abs(x - currentPosition.X) <= fuel / Constants.fuelCostConst))
            {
                return true;
            }
            return false;
        }
        public override string GetType()
        {
            return "Diagonale";
        }
    }

    public class HorseEngine : EngineType
    {
        public override bool CheckMove(int x, int y, Coordinates currentPosition, int fuel)
        {
            if (((Math.Abs(y - currentPosition.Y) * 2 == Math.Abs(x - currentPosition.X) * 3) || (Math.Abs(y - currentPosition.Y) * 3 == Math.Abs(x - currentPosition.X) * 2))
                && (Math.Abs(y - currentPosition.Y) + Math.Abs(x - currentPosition.X) <= fuel / Constants.fuelCostConst))
            {
                return true;
            }
            return false;
        }
        public override string GetType()
        {
            return "Horse";
        }
    }

    public class QueenEngine : EngineType
    {
        public override bool CheckMove(int x, int y, Coordinates currentPosition, int fuel)
        {
            if (Math.Abs(y - currentPosition.Y) + Math.Abs(x - currentPosition.X) <= fuel / (Constants.fuelCostConst * 10))
            {
                return true;
            }
            return false;
        }
        public override string GetType()
        {
            return "Queen";
        }
    }




    public class Hold
    {
        private FoodBatchesList? food;
        private int metall;
        private int fuel;
        private int money;
        private int water;
        private int oxygen;

        public Hold()
        {
            food = new FoodBatchesList();
            metall = 0;
            fuel = Constants.startFuelConst;
            money = Constants.startMoneyConst;
            water = 0;
            oxygen = Constants.startOxygenConst;
        }

        public void SetResource(int size, Resource type)
        {
            switch (type)
            {
                case Resource.money:
                    money += size;
                    break;
                case Resource.fuel:
                    fuel += size;
                    break;
                case Resource.metall:
                    metall += size;
                    break;
                case Resource.oxygen:
                    metall += size;
                    break;
            }
        }

        public bool AddFood(int size, int currentMove)
        {
            return food.AddBatch(size, currentMove);
        }

        public bool TakeFood(int size)
        {
            return food.SetSomeFood(size);
        }

        public int GetResource(Resource type)
        {
            switch (type)
            {
                case Resource.money:
                    return money;
                case Resource.fuel:
                    return fuel;
                case Resource.water:
                    return water;
                case Resource.metall:
                    return metall;
                case Resource.food:
                    return food.GetCurrentFoodNum();
                case Resource.oxygen:
                    return oxygen;
            }

            return 0;
        }

        public void Rotting(int currentMove)
        {
            food.Decay(currentMove);
        }
    }


    public class FoodBatchesList {
        private List<FoodBatch> batchesList = new List<FoodBatch>();
        private int maxNum = 10000;

        private int currentSize = 0;
        //private int maxNum = sizeOfFoodBatchesConst;

        public bool AddBatch(int size, int currentMove)
        {
            if ((currentSize + size <= maxNum) && (size > 0)) {
                FoodBatch batch = new FoodBatch(size, currentMove);
                batchesList.Add(batch);
                currentSize += size;
                return true;
            }

            return false;
        }

        public bool SetSomeFood(int num)
        {
            if (num > currentSize) {
                return false;
            }

            int remainingNum = num;
            for (int i = batchesList.Count - 1; i >= 0; i--) {
                if (batchesList[i].GetSize() > remainingNum) {
                    batchesList[i].ChangeSize(remainingNum);
                    currentSize -= remainingNum;
                    break;
                }
                else {
                    remainingNum -= batchesList[i].GetSize();
                    currentSize -= batchesList[i].GetSize();
                    batchesList.RemoveAt(i);
                }
            }

            return true;
        }

        public int GetCurrentFoodNum()
        {
            return currentSize;
        }

        public void Decay(int currentMove)
        {
            for (int i = 0; i < batchesList.Count; i++) {
                if (batchesList[i].GetTime() == currentMove) {
                    currentSize -= batchesList[i].GetSize();
                    batchesList.RemoveAt(i);
                }
            }
        }
    }

    public class FoodBatch {
        private int timeOfDeath;
        private int sizeOfBatch;

        public FoodBatch(int size, int currentMove)
        {
            timeOfDeath = currentMove + Constants.timeOfLife;
            sizeOfBatch = size;
        }

        public void SetBatch(int time, int size)
        {
            timeOfDeath = time;
            sizeOfBatch = size;
        }

        public void ChangeSize(int size)
        {
            sizeOfBatch -= size;
        }

        public int GetTime()
        {
            return timeOfDeath;
        }

        public int GetSize()
        {
            return sizeOfBatch;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmoSim.Model.Galaxy;
using CosmoSim.Model.Galaxy.Entities;

namespace CosmoSim
{
    public class Field
    {
        public string[,] SpaceField { get; private set; } = new string[34, 45];
        public Field(StarSystem starSystem)
        {
            SpaceField[0, 0] = "ship";
        }
        public void OnFlying(Coordinates newCoordinates, Coordinates oldCoordinates)
        {

        }
    }
}

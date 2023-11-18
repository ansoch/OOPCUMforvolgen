using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoSim
{
    static class Player
    {
        public static Spaceship SpaceShip { get; } = new Spaceship();
        public static Field SpaceField { get; private set; } = new Field(SpaceShip.CurrentSystem);
    }
}

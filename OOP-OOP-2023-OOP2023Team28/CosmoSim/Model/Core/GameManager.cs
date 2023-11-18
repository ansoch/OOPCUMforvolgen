using System.Timers;
using CosmoSim.Model.Galaxy.Entities;
using Timer = System.Timers.Timer;

namespace CosmoSim.Model.Core
{
    public static class GameManager
    {
        private static Timer timer;

        public static void Start()
        {
            timer = new Timer(1);
            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        private static void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdateGalaxy();
            UpdateSpaceship();
        }

        private static void UpdateGalaxy()
        {
            // Обновление состояния галактики
            foreach (StarSystem starSystem in Galaxy.Entities.Galaxy.StarSystems)
            {
                starSystem.Update();
            }
        }

        private static void UpdateSpaceship()
        {
            // Обновление состояния космического корабля
        }
    }
}
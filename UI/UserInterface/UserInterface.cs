using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class UserInterface
    {
        private static int totalDaysToSim;
        private static int ticksPerSecond;
        private static UserInput userInput = new UserInput();
        private static UserPrint userPrint = new UserPrint();
        private static CareHouseSimulation careHouseSimulation;

        public static void RunMainUI()
        {
            // Metod som visar nån meny-print
            // Meny-val för typ: Starta ny simulation, Sök på information om specifik simulation, etc

        }



        public static void StartSimulation(int daysToSimInput, int tickPerSecInput)
        {
            totalDaysToSim = daysToSimInput; // metod som returnerar antalet dagar att simulera
            ticksPerSecond = tickPerSecInput; // metod som returnerar antalet ticks per sekund
            careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);
        }
    }
}

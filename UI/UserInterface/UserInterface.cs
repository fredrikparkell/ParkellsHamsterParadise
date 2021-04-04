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
        private static int totalDaysToSim = 3; // default-värden
        private static int ticksPerSecond = 2; // default-värden
        private static UserInput userInput = new UserInput();
        private static UserPrint userPrint = new UserPrint();
        private static CareHouseSimulation careHouseSimulation;

        public static void RunMainUI()
        {
            MainMenu();
        }
        public static void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                string title = "Some big title I will add here later";
                // (Use the arrow keys to cycle through options and press enter to select an option.)
                string[] options = new string[] { "Start Simulation", "Change Values (default are already set)", "Look at specific simulation" };
                
                Menu mainMenu = new Menu(title, options);
                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        StartSimulation();
                        break;
                    case 1:
                        totalDaysToSim = DaysToSim();
                        ticksPerSecond = TicksPerSecond();
                        break;
                    case 2:
                        break;
                }
            }
        }
        public static int DaysToSim()
        {
            Console.Clear();

            return 3;
        }
        public static int TicksPerSecond()
        {
            Console.Clear();

            return 2;
        }


        private static void StartSimulation()
        {
            Console.WriteLine("\n\nStarting simulation..");
            Console.ReadKey();
            Console.Clear();
            //careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);
        }
    }
}

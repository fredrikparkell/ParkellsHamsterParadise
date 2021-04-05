using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public class UserInterface
    {
        private static Random random = new Random();
        private static int totalDaysToSim = 3; // default-värden 3
        private static int ticksPerSecond = 2; // default-värden 2
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
                Console.SetCursorPosition(120, 0); Console.Write($"Days to simulate: {totalDaysToSim}");
                Console.SetCursorPosition(120, 1); Console.Write($"Ticks per second: {ticksPerSecond}");

                string title = @"Some big title I will add later

(Use the arrow keys to cycle through options and press enter to select an option.)";
                string[] options = new string[] { "Start Simulation", "Change Values (default are already set)",
                                                  "Look at specific simulation","Show Credits", "Exit the program" };
                
                Menu mainMenu = new Menu(title, options, 0, 3);
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
                        SimulationMenu();
                        break;
                    case 3:
                        ShowCredits();
                        break;
                    case 4:
                        Console.WriteLine("\n\nShutting the program off..");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static void StartSimulation()
        {
            Console.WriteLine("\n\nStarting simulation..");
            //careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);
            Console.ReadKey();
            Console.Clear();
            //careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);
        }
        private static int DaysToSim()
        {
            while (true)
            {
                Console.Clear();
                string title = @"Some big title I will add here later";
                // (Use the arrow keys to cycle through options and press enter to select an option.)
                string[] options = new string[] { "  Default (3)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "      (4)      ", "      (5)      ", "      (6)      ",
                                                "      (7)      ", "      (8)      ", "      (9)      ", "Random (10->30)" };

                Menu daysToSimMenu = new Menu(title, options, 20, 40);
                int selectedIndex = daysToSimMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 3:
                        return 3;
                    case 1: case 2: case 4: case 5: case 6: case 7: case 8: case 9:
                        return selectedIndex;
                    case 10:
                        return random.Next(10, 31);
                }
            }
        }
        private static int TicksPerSecond()
        {
            while (true)
            {
                Console.Clear();
                string title = @"Some big title I will add here later";
                // (Use the arrow keys to cycle through options and press enter to select an option.)
                string[] options = new string[] { "  Default (2)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "      (4)      ", "      (5)      " };

                Menu ticksPerSecondMenu = new Menu(title, options, 20, 40);
                int selectedIndex = ticksPerSecondMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 2:
                        return 2;
                    case 1: case 3: case 4: case 5:
                        return selectedIndex;
                }
            }
        }
        private static void ShowCredits()
        {
            
        }
        private static void SimulationMenu()
        {

        }
    }
}

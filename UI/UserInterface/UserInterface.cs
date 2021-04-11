using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    /// <summary>
    /// A instance of the UserInterface-class starts up 
    /// the userexperience with the menu options.
    /// </summary>
    public class UserInterface
    {
        #region Fields
        private static Random random = new Random();
        private bool isTimerStopped;
        private int currentSimulationInfoId;
        private int totalDaysToSim = 2; // default-värden = 2
        private int ticksPerSecond = 2; // default-värden = 2
        private static UserPrint userPrint = new UserPrint();
        private static SimulationInfo simulationInfo = new SimulationInfo();
        #endregion

        #region Startup menu-methods
        public void RunMainUI()
        {
            MainMenu();
        }
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(125, 4); Console.Write($"Days to simulate: {totalDaysToSim}");
                Console.SetCursorPosition(125, 5); Console.Write($"Ticks per second: {ticksPerSecond}");
                Console.SetCursorPosition(1, 32); Console.ForegroundColor = ConsoleColor.Yellow; Console.Write("Just started up the 'game' for the first time? ");
                Console.SetCursorPosition(1, 33); Console.ResetColor(); Console.Write("Make sure the connectionstring in the HamsterDbContext.cs-file is right for your SQL Server on your computer");

                string title = @"
 ██░ ██ ▄▄▄      ███▄ ▄███▓ ██████▄▄▄█████▓█████ ██▀███      ██▓███  ▄▄▄      ██▀███  ▄▄▄     ▓█████▄ ██▓ ██████▓█████ 
▓██░ ██▒████▄   ▓██▒▀█▀ ██▒██    ▒▓  ██▒ ▓▓█   ▀▓██ ▒ ██▒   ▓██░  ██▒████▄   ▓██ ▒ ██▒████▄   ▒██▀ ██▓██▒██    ▒▓█   ▀ 
▒██▀▀██▒██  ▀█▄ ▓██    ▓██░ ▓██▄  ▒ ▓██░ ▒▒███  ▓██ ░▄█ ▒   ▓██░ ██▓▒██  ▀█▄ ▓██ ░▄█ ▒██  ▀█▄ ░██   █▒██░ ▓██▄  ▒███   
░▓█ ░██░██▄▄▄▄██▒██    ▒██  ▒   ██░ ▓██▓ ░▒▓█  ▄▒██▀▀█▄     ▒██▄█▓▒ ░██▄▄▄▄██▒██▀▀█▄ ░██▄▄▄▄██░▓█▄   ░██░ ▒   ██▒▓█  ▄ 
░▓█▒░██▓▓█   ▓██▒██▒   ░██▒██████▒▒ ▒██▒ ░░▒████░██▓ ▒██▒   ▒██▒ ░  ░▓█   ▓██░██▓ ▒██▒▓█   ▓██░▒████▓░██▒██████▒░▒████▒
 ▒ ░░▒░▒▒▒   ▓▒█░ ▒░   ░  ▒ ▒▓▒ ▒ ░ ▒ ░░  ░░ ▒░ ░ ▒▓ ░▒▓░   ▒▓▒░ ░  ░▒▒   ▓▒█░ ▒▓ ░▒▓░▒▒   ▓▒█░▒▒▓  ▒░▓ ▒ ▒▓▒ ▒ ░░ ▒░ ░
 ▒ ░▒░ ░ ▒   ▒▒ ░  ░      ░ ░▒  ░ ░   ░    ░ ░  ░ ░▒ ░ ▒░   ░▒ ░      ▒   ▒▒ ░ ░▒ ░ ▒░ ▒   ▒▒ ░░ ▒  ▒ ▒ ░ ░▒  ░ ░░ ░  ░
 ░  ░░ ░ ░   ▒  ░      ░  ░  ░  ░   ░        ░    ░░   ░    ░░        ░   ▒    ░░   ░  ░   ▒   ░ ░  ░ ▒ ░  ░  ░    ░   
 ░  ░  ░     ░  ░      ░        ░            ░  ░  ░                      ░  ░  ░          ░  ░  ░    ░       ░    ░  ░
                                                                                               ░                       
(Use the arrow keys to cycle through options and press enter to select an option.)";
                string[] options = new string[] { "Start Simulation", "Change Values (default are already set)",
                                                  "Look at specific simulation", "Exit the program" };
                
                UserMenu mainMenu = new UserMenu(title, options, 0, 3);
                int selectedIndex = mainMenu.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Console.WriteLine("\n\nLoading..");
                        StartSimulation();
                        break;
                    case 1:
                        totalDaysToSim = ChooseDaysToSimulate();
                        ticksPerSecond = ChooseTicksPerSecond();
                        break;
                    case 2:
                        Console.WriteLine("\n\nLoading..");
                        SimulationMenu();
                        break;
                    case 3:
                        Console.WriteLine("\n\nShutting the program off..");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                }
            }
        }
        #endregion

        #region Simulation control-methods
        private void StartSimulation()
        {
            CareHouseSimulation careHouseSimulation = new CareHouseSimulation(ticksPerSecond, totalDaysToSim);

            careHouseSimulation.SendTickInfo += userPrint.PrintTickInfo;
            careHouseSimulation.SendDayInfo += userPrint.PrintDayInfo;
            careHouseSimulation.SendSimulationSummary += userPrint.PrintSimulationSummary;
            careHouseSimulation.SendSimulationSummary += StopControlOfTimer;

            ControlTimer(careHouseSimulation);

            careHouseSimulation.SendTickInfo -= userPrint.PrintTickInfo;
            careHouseSimulation.SendDayInfo -= userPrint.PrintDayInfo;
            careHouseSimulation.SendSimulationSummary -= userPrint.PrintSimulationSummary;
            careHouseSimulation.SendSimulationSummary -= StopControlOfTimer;
        }
        private int ChooseDaysToSimulate()
        {
            while (true)
            {
                Console.Clear();
                string title = @"
  _____                    _              _                 _       _      ___  
 |  __ \                  | |            (_)               | |     | |    |__ \ 
 | |  | | __ _ _   _ ___  | |_ ___    ___ _ _ __ ___  _   _| | __ _| |_ ___  ) |
 | |  | |/ _` | | | / __| | __/ _ \  / __| | '_ ` _ \| | | | |/ _` | __/ _ \/ / 
 | |__| | (_| | |_| \__ \ | || (_) | \__ | | | | | | | |_| | | (_| | ||  __|_|  
 |_____/ \__,_|\__, |___/  \__\___/  |___|_|_| |_| |_|\__,_|_|\__,_|\__\___(_)  
                __/ |                                                           
               |___/                                                            
(Use the arrow keys to cycle through options and press enter to select an option.)";
                string[] options = new string[] { "  Default (2)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "      (4)      ", "      (5)      ", "      (6)      ",
                                                "      (7)      ", "      (8)      ", "      (9)      ", "Random (10->30)" };

                UserMenu daysToSimMenu = new UserMenu(title, options, 10, 25);
                int selectedIndex = daysToSimMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 2:
                        return 2;
                    case 1: case 3: case 4: case 5: case 6: case 7: case 8: case 9:
                        return selectedIndex;
                    case 10:
                        return random.Next(10, 31);
                }
            }
        }
        private int ChooseTicksPerSecond()
        {
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(1, 22); Console.ForegroundColor = ConsoleColor.DarkRed; Console.Write("NOTE: 5, 6 & 7 ticks per second has been tested a total of 50+ times (15-20+ times each) with a range of 2-8 days runtime.");
                Console.SetCursorPosition(1, 23); Console.Write("In only one, a 7 tick per second (142 ms/tick) 8 day simulation, did it crash mid-simulation. This due to a update going a little too slow.");
                Console.SetCursorPosition(1, 24); Console.Write("It ran fine on 7 tick simulations directly after that aswell. Thus, there should not be any issues running the simulation on up to 7 ticks per second."); Console.ResetColor();

                string title = @"
  _______ _      _                                                         _ ___  
 |__   __(_)    | |                                                       | |__ \ 
    | |   _  ___| | _____   _ __   ___ _ __   ___  ___  ___ ___  _ __   __| |  ) |
    | |  | |/ __| |/ / __| | '_ \ / _ | '__| / __|/ _ \/ __/ _ \| '_ \ / _` | / / 
    | |  | | (__|   <\__ \ | |_) |  __| |    \__ |  __| (_| (_) | | | | (_| ||_|  
    |_|  |_|\___|_|\_|___/ | .__/ \___|_|    |___/\___|\___\___/|_| |_|\__,_|(_)  
                           | |                                                    
                           |_|                                                    
(Use the arrow keys to cycle through options and press enter to select an option.)";
                string[] options = new string[] { "  Default (2)  ", "      (1)      ", "      (2)      ",
                                                "      (3)      ", "Recommended (4)", "      (5)      ",
                                                "      (6)      ", "      (7)      " };

                UserMenu ticksPerSecondMenu = new UserMenu(title, options, 10, 25);
                int selectedIndex = ticksPerSecondMenu.Run();

                switch (selectedIndex)
                {
                    case 0: case 2:
                        return 2;
                    case 1: case 3: case 4: case 5: case 6: case 7:
                        return selectedIndex;
                }
            }
        }
        private void ControlTimer(CareHouseSimulation careHouseSimulation)
        {
            isTimerStopped = false;
            ConsoleKeyInfo keyInfo;

            do
            {
                keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    careHouseSimulation.ManipulateTimer();
                }
            } while (isTimerStopped != true);
        }
        public async void StopControlOfTimer(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() => { isTimerStopped = true; });
        }
        #endregion

        #region Simulation display-methods
        private void SimulationMenu()
        {
            while (true)
            {
                string title = @"
   _____ _                                        _                 _       _   _             
  / ____| |                                      (_)               | |     | | (_)            
 | |    | |__   ___   ___  ___  ___    __ _   ___ _ _ __ ___  _   _| | __ _| |_ _  ___  _ __  
 | |    | '_ \ / _ \ / _ \/ __|/ _ \  / _` | / __| | '_ ` _ \| | | | |/ _` | __| |/ _ \| '_ \ 
 | |____| | | | (_) | (_) \__ |  __/ | (_| | \__ | | | | | | | |_| | | (_| | |_| | (_) | | | |
  \_____|_| |_|\___/ \___/|___/\___|  \__,_| |___|_|_| |_| |_|\__,_|_|\__,_|\__|_|\___/|_| |_|
                                                                                              
                                                                                              
(Use the arrow keys to cycle through options and press enter to select an option.)";
                List<string> allSimulations = new List<string>();
                allSimulations = simulationInfo.GetAllSimulations();
                allSimulations.Insert(0, "[Go back to previous menu]");
                string[] options = allSimulations.ToArray();
                Console.Clear();

                UserMenu simulationMenu = new UserMenu(title, options, 10, 20);
                currentSimulationInfoId = simulationMenu.Run();

                if (currentSimulationInfoId == 0) { break; }
                else 
                {
                    string currentSimString = options[currentSimulationInfoId];
                    string[] splitString = currentSimString.Split('-');
                    string currentSimSplitString = splitString[0];
                    string[] splitStringAgain = currentSimSplitString.Split(" ");
                    currentSimulationInfoId = int.Parse(splitStringAgain[1]);

                    SimulationInfoMenu(currentSimulationInfoId);
                }
            }
        }
        private void SimulationInfoMenu(int currentSimulationId)
        {
            while (true)
            {
                Console.Clear();
                string title = @"
  _____                             _        _        _ 
 |  __ \                           | |      | |      | |
 | |  | | __ _ _   _    ___  _ __  | |_ ___ | |_ __ _| |
 | |  | |/ _` | | | |  / _ \| '__| | __/ _ \| __/ _` | |
 | |__| | (_| | |_| | | (_) | |    | || (_) | || (_| | |
 |_____/ \__,_|\__, |  \___/|_|     \__\___/ \__\__,_|_|
                __/ |                                   
               |___/                                    
(Use the arrow keys to cycle through options and press enter to select an option.)";
                List<string> allDays = simulationInfo.GetAllDays(currentSimulationId);
                allDays.Insert(0, "[Go back to previous menu]");
                allDays.Add("Show total for all days");
                string[] options = allDays.ToArray();

                UserMenu simulationMenu = new UserMenu(title, options, 10, 20);
                int currentThingToCheck = simulationMenu.Run();

                if (currentThingToCheck == 0) { break; }
                else if (currentThingToCheck == options.Length - 1)
                {
                    PrintSimulationSummary(options.Length-2,currentSimulationId);
                }
                else
                {
                    PrintDaySimulationSummary(currentThingToCheck,currentSimulationId);
                }
            }
        }
        private void PrintDaySimulationSummary(int dayToCheck, int currentSimulationId)
        {
            DayInfoEventArgs allHamstersInfo = simulationInfo.GetAllActivityLogsPerDay(dayToCheck,currentSimulationId);

            userPrint.WriteOutDayInfo(allHamstersInfo);

            Console.ReadKey();
        }
        private void PrintSimulationSummary(int totalDays, int currentSimulationId)
        {
            SimulationSummaryEventArgs allInfo = simulationInfo.GetAllActivityLogsPerSimulation(totalDays, currentSimulationId);

            userPrint.WriteOutSimulationSummary(allInfo);

            Console.ReadKey();
        }
        #endregion
    }
}

using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public class UserPrint
    {
        private (int Left, int Top) currentPosition;

        #region PrintTickInfo-methods
        public async void PrintTickInfo(object sender, TickInfoEventArgs e)
        {        
            await Task.Run(() =>
            {
                if (e.ElapsedTicks == 1) { Console.Clear(); }

                Console.CursorVisible = false;
                ClearConsole();

                int titleDisplayPosition = 58; // 58
                int timeDisplayPosition = 54;
                int cageDisplayPosition = 70;

                Console.SetCursorPosition(titleDisplayPosition, 1);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"HAMSTER PARADISE");
                Console.ForegroundColor = ConsoleColor.White;

                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition+8, currentPosition.Top + 1); Console.Write($"Day: {e.ElapsedDays}");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(3, currentPosition.Top + 1); Console.Write("(Press Enter to Pause/Resume the simulation)");
                Console.SetCursorPosition(83, currentPosition.Top + 1); Console.Write("(Press Enter to Pause/Resume the simulation)");
                Console.ResetColor();
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write($"Date: {e.CurrentSimulationDate}");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");

                string header = String.Format("{0,-15} {1, 70}", "ACTIVITIES", "HAMSTER OVERVIEW");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(25, currentPosition.Top + 1);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(header); Console.ResetColor();
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(1, currentPosition.Top + 1);

                Console.Write("----------------------------------------------------------------------------------------------" +
                    "---------------------------------------------------");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(1, currentPosition.Top + 1);
                Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(1, currentPosition.Top + 1);
                Console.Write(" ");

                (int Left, int Top) tempCurrentPosition = Console.GetCursorPosition();
                int count = 0;
                int rowCount = 0;
                if (e.ActivityLogs.Count() != 0)
                {
                    while (true)
                    {
                        currentPosition.Left = tempCurrentPosition.Left + 1;
                        Console.SetCursorPosition(currentPosition.Left, currentPosition.Top + (rowCount + 1));
                        string something = String.Format("{0, -35} {1, -20}", $"{e.ActivityLogs[count].Hamster.Name} " +
                                                                        $"=> {e.ActivityLogs[count].Activity.ActivityName}",
                                                                       $"{e.ActivityLogs[count + 1].Hamster.Name}" +
                                                                       $" => {e.ActivityLogs[count + 1].Activity.ActivityName}");
                        Console.Write(something);
                        count += 2;

                        // kunna skriva ut endast en activityLog 

                        rowCount++;
                        if (count > e.ActivityLogs.Count() - 1)
                        {
                            break;
                        }
                    
                    }
                }

                Console.SetCursorPosition(cageDisplayPosition, tempCurrentPosition.Top);
                WriteOutCage(e.HamstersInCages, 0, 5, cageDisplayPosition, tempCurrentPosition.Top-1);
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                WriteOutCage(e.HamstersInCages, 5, 10, cageDisplayPosition, currentPosition.Top + 2);

                WriteOutExerciseArea(e.HamstersInExerciseArea);

                DrawVerticalLine(65);
                DrawVerticalLine(145);
            });
        }
        private void ClearConsole()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
        private void DrawVerticalLine(int leftPosition)
        {
            for (int i = 0; i < 30; i++)
            {
                Console.SetCursorPosition(leftPosition, 8 + i + 1);
                string something = String.Format("{0, 0}", "|");
                Console.Write(something);
            }
        }
        private void WriteOutExerciseArea(List<HamsterParadise.DataAccess.Hamster> hamstersInExerciseArea)
        {
            Console.SetCursorPosition(98, 25);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Exercise Area");
            Console.SetCursorPosition(98, 26);
            Console.Write(" ");
            Console.ResetColor();

            for (int i = 0; i < hamstersInExerciseArea.Count(); i++)
            {
                Console.SetCursorPosition(101, 27+i);
                Console.Write(hamstersInExerciseArea[i].Name);
            }
        }
        private void WriteOutCage(List<IGrouping<int?, HamsterParadise.DataAccess.Hamster>> hamsters, int startValue, int endValue,
                                                                int cageDisplayPosition, int tempCurrentPositionTop)
        {
            for (int i = startValue; i < endValue; i++)
            {
                var cage = hamsters.Where(c => c.Key == i + 1).Select(h => h).FirstOrDefault();
                if (cage != null)
                {
                    if (!cage.ElementAt(0).IsFemale)
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 1);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write($"Cage {i + 1} ");
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 2);
                        Console.Write(" ");
                    }
                    else
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"Cage {i + 1} ");
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 2);
                        Console.Write(" ");
                    }
                }
                else
                {
                    Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 1);
                    Console.Write($"Cage {i + 1} ");
                    Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 2);
                    Console.Write(" ");
                }
                Console.ResetColor();

                //Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 1);
                //Console.Write($"Cage {i + 1} ");
                //Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 2);
                //Console.Write(" ");

                if (hamsters.Contains(hamsters.Select(h => h).AsEnumerable().Where(c => c.Key == i + 1).FirstOrDefault()))
                {
                    //var cage = hamsters.Where(c => c.Key == i + 1).Select(h => h).First();

                    for (int j = 0; j < cage.Count(); j++)
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + (2 + j + 1));
                        Console.Write($"{cage.ElementAt(j).Name}");
                    }

                    if (cage.Count() != 3)
                    {
                        for (int x = 0; x < cage.Count() - 3; x++)
                        {
                            currentPosition = Console.GetCursorPosition();
                            Console.SetCursorPosition(cageDisplayPosition, currentPosition.Top + (1 + x + 1));
                            Console.Write("");
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < 3; m++)
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + (2 + m + 1));
                        Console.Write("");
                    }
                }

                cageDisplayPosition += 15;
            }
        }
        #endregion

        #region PrintDayInfo-methods
        public async void PrintDayInfo(object sender, DayInfoEventArgs e)
        {
            await Task.Run(() =>
            {
                Console.CursorVisible = false;
                ClearConsole();

                int titleDisplayPosition = 58; // 58
                int timeDisplayPosition = 54;
                int hamsterDisplayPosition = 5;

                Console.SetCursorPosition(titleDisplayPosition, 1);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write($"DAAAAAY OVERVIEW");
                Console.ForegroundColor = ConsoleColor.White;

                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition + 8, currentPosition.Top + 1); Console.Write($"Day: {e.ElapsedDays}");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.SetCursorPosition(3, currentPosition.Top + 1); Console.Write("(Press Enter to Pause/Resume the simulation)");
                Console.SetCursorPosition(83, currentPosition.Top + 1); Console.Write("(Press Enter to Pause/Resume the simulation)");
                Console.ResetColor();
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(titleDisplayPosition, currentPosition.Top + 1); Console.Write($"Date: {e.CurrentSimulationDate.ToShortDateString()}");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");

                // -Vad hamstrarna gjort (sammanfattning)
                //    - Avlämnade, Burtid, Motionstid, Hämtade
                // -Hur lång tid fick hamstrarna vänta på motion, dvs tid mellan incheckning och motionstillfälle
                //    - Tidsskillnaden mellan incheckning och första motionstillfället
                // -Hur många gånger har hamstrarna hunnit motionera
                //    - Kolla på ActivityLogs med hamster-id:t, simulations-id:t och activity-id:t           

                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(hamsterDisplayPosition, currentPosition.Top + 1);
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                string header = String.Format("{0, -20} {1, -20} {2, -20} {3, -20} {4, -20} {5, -20} {6, -20}", "Name", "Arrived", "Time in Cage",
                                            "Time in EA", "In Exercise", "Arrived => EA", "Departure");
                Console.Write(header);
                Console.ResetColor();

                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(hamsterDisplayPosition, currentPosition.Top + 1);
                Console.Write(" ");

                // HamsterName      Arrived       Time in Cage      Time in Exercise Area      In Exercise Area      Arri=>Exercise      Departure  

                for (int i = 0; i < e.ActivityLogsPerHamster.Count(); i++)
                {
                    var iGroupHamster = e.ActivityLogsPerHamster.Where(c => c.Key == i + 1).First();

                    var activityLogs = iGroupHamster.Where(c => c.Id > 0).ToList();
                    var hamster = e.Hamsters.Where(c => c.Id == iGroupHamster.Key).First();
                    var arrived = iGroupHamster.Where(c => c.ActivityId == 1).First();

                    var timeInCage = 0.0;
                    var timeInEA = 0.0;
                    TimeSpan timeBetween = new TimeSpan();
                    //var timesInCage = iGroupHamster.Where(c => c.ActivityId == 3).ToList();
                    var timesInEA = iGroupHamster.Where(c => c.ActivityId == 2).ToList();

                    for (int j = 0; j < activityLogs.Count(); j++)
                    {
                        if (j + 1 != activityLogs.Count())
                        {
                            timeBetween = activityLogs[j + 1].TimeStamp - activityLogs[j].TimeStamp;
                            if (activityLogs[j].ActivityId == 3)
                            {
                                timeInCage += timeBetween.TotalMinutes;
                            }
                            else if (activityLogs[j].ActivityId == 2)
                            {
                                timeInEA += timeBetween.TotalMinutes;
                            }
                        }
                    }

                    var firstExercise = iGroupHamster.Where(c => c.ActivityId == 2).First();
                    TimeSpan timeSpan = firstExercise.TimeStamp - arrived.TimeStamp;
                    var departure = iGroupHamster.Where(c => c.ActivityId == 4).First();

                    Console.SetCursorPosition(hamsterDisplayPosition, currentPosition.Top + (2+i));
                    string hamsterString = String.Format("{0, -20} {1, -20} {2, -20} {3, -20} {4, -20} {5, -20} {6, -20}", 
                                    $"{hamster.Name}", $"{arrived.TimeStamp.TimeOfDay}", $"{timeInCage}", $"{timeInEA}", $"{timesInEA.Count()} times",
                                    $"{timeSpan.TotalMinutes} minutes", $"{departure.TimeStamp.TimeOfDay}");
                    Console.Write(hamsterString);
                }

            });
        }
        #endregion

        #region PrintSimulationSummary
        public async void PrintSimulationSummary(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() =>
            {
                Console.Clear();
                Console.CursorVisible = false;


            });
        }
        #endregion
    }
}

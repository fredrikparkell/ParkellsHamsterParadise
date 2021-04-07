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
        private int dayCounter;
        private int prevDayTicker;
        private (int Left, int Top) currentPosition;
        public async void PrintTickInfo(object sender, TickInfoEventArgs e)
        {        
            // Kunna visa:
            // -Datumet/tiden/ticket/dag
            // -Vilka hamstrar i vilka burar + ExerciseArea
            // -Alla ActivityLogs som reggats detta tick

            await Task.Run(() =>
            {
                if (e.ElapsedTicks == 1) { Console.Clear(); }
                //Console.Clear();

                ClearConsole();

                Console.CursorVisible = false;
                int weekDisplayPosition = 25;
                int timeDisplayPosition = 60;
                int cageDisplayPosition = 70;
                string[] days = new string[7] { "[Thursday],", "[Friday],", "[Saturday],", "[Sunday]", "[Monday],", "[Tuesday],", "[Wednesday]," };

                // skriva ut veckodags-visning
                Console.SetCursorPosition(weekDisplayPosition, 1);
                for (int i = 0; i < days.Length; i++)
                {
                    if (i == dayCounter-1)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write($"{days[i]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.Write($"{days[i]} ");
                    }
                }



                // skriva ut Tick
                //currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write($"Ticks: {e.ElapsedTicks}");
                // skriva ut Dag
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition+8, currentPosition.Top + 1); Console.Write($"Day: {e.ElapsedDays}");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                // skriva ut Klockslag/datum
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write($"Date: {e.CurrentSimulationDate}");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");



                // skriva ut med String.Format     ACTIVITY          HAMSTER OVERVIEW
                string header = String.Format("{0,-15} {1, 65}", "ACTIVITIES", "HAMSTER OVERVIEW");
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(15, currentPosition.Top + 1);
                Console.ForegroundColor = ConsoleColor.Yellow; Console.Write(header); Console.ResetColor();
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(1, currentPosition.Top + 1);



                // skriva ut ----------------------------
                Console.Write("-------------------------------------------------------------------------------------------------------------------------------------------------------");
                // 
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(1, currentPosition.Top + 1);
                Console.Write(" ");



                //for (int i = 0; i < 30; i++)
                //{
                //    Console.SetCursorPosition(30, 8+i+1);
                //    string something = String.Format("{0, 20}", "|");
                //    Console.Write(something);
                //}


                (int Left, int Top) tempCurrentPosition = Console.GetCursorPosition();
                // skriva ut alla ActivityLogs:      activity    |
                for (int i = 0; i < e.ActivityLogs.Count(); i++)
                {
                    currentPosition.Left = tempCurrentPosition.Left + 1;
                    Console.SetCursorPosition(currentPosition.Left, currentPosition.Top + (i+1));
                    string something = String.Format("{0, -30} {1, 20}", $"{e.ActivityLogs[i].Hamster.Name} => " +
                                                              $"{e.ActivityLogs[i].Activity.ActivityName}", "|");
                    Console.Write(something);
                }




                Console.SetCursorPosition(cageDisplayPosition, tempCurrentPosition.Top);
                WriteOutCage(e.HamstersInCages, 0, 5, cageDisplayPosition, tempCurrentPosition.Top);
                currentPosition = Console.GetCursorPosition(); Console.SetCursorPosition(timeDisplayPosition, currentPosition.Top + 1); Console.Write(" ");
                WriteOutCage(e.HamstersInCages, 5, 10, cageDisplayPosition, currentPosition.Top + 2);

                // skriva ut ----------------------------
                // 
                //                          ExerciseArea
                //
                //                            Hamster
                //                            Hamster
                //                            Hamster
                //                            osv
            });
        }
        public static void ClearConsole()
        {
            for (int i = 0; i < 40; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }
        private void WriteOutCage(List<IGrouping<int?, HamsterParadise.DataAccess.Hamster>> hamsters, int startValue, int endValue,
                                                                int cageDisplayPosition, int tempCurrentPositionTop)
        {
            for (int i = startValue; i < endValue; i++)
            {
                Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 1);
                Console.Write($"Cage {i + 1} ");
                Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + 2);
                Console.Write(" ");

                if (hamsters.Contains(hamsters.Select(h => h).AsEnumerable().Where(c => c.Key == i + 1).FirstOrDefault()))
                {
                    var cage = hamsters.Where(c => c.Key == i + 1).Select(h => h).First();

                    for (int j = 0; j < cage.Count(); j++)
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + (2 + j + 1));
                        Console.Write($"{cage.ElementAt(j).Name}     ");
                    }

                    if (cage.Count() != 3)
                    {
                        for (int x = 0; x < cage.Count() - 3; x++)
                        {
                            currentPosition = Console.GetCursorPosition();
                            Console.SetCursorPosition(cageDisplayPosition, currentPosition.Top + (1 + x + 1));
                            Console.Write("          ");
                        }
                    }
                }
                else
                {
                    for (int m = 0; m < 3; m++)
                    {
                        Console.SetCursorPosition(cageDisplayPosition, tempCurrentPositionTop + (2 + m + 1));
                        Console.Write("          ");
                    }
                }

                cageDisplayPosition += 15;
            }
        }

        public async void PrintDayInfo(object sender, DayInfoEventArgs e)
        {
            // Kunna visa:
            // -Datumet/tiden/ticket/dag
            // -Vad hamstrarna gjort (sammanfattning)
            //    - Avlämnade, Burtid, Motionstid, Hämtade
            // -Hur lång tid fick hamstrarna vänta på motion, dvs tid mellan incheckning och motionstillfälle
            //    - Tidsskillnaden mellan incheckning och första motionstillfället
            // -Hur många gånger har hamstrarna hunnit motionera
            //    - Kolla på ActivityLogs med hamster-id:t, simulations-id:t och activity-id:t

            await Task.Run(() =>
            {
                Console.Clear();
                Console.CursorVisible = false;


            });
        }
        public async void PrintSimulationSummary(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() =>
            {
                Console.Clear();
                Console.CursorVisible = false;


            });
        }
    }
}

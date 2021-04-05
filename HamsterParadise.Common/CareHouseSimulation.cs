using HamsterParadise.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    // eventuellt lägga till ett till projekt/assembly för API eller annan kommunikation utåt, dvs inte UI
    public class CareHouseSimulation
    {
        private int elapsedTicks;
        private int elapsedDays;
        private int tickTotalRunTime; // om 3 dagar, typ 3*100 = 300 ticks varpå varje 100 ticks
                                      // sover den i några sekunder för att simulera tiden mellan
                                      // 17:00 -> 07:00 samt ger tid för att läsa sammanfattningen på skärmen
        private int currentSimulationId;
        private DateTime currentSimulationDate;
        private TimeTicker timeTicker;

        public CareHouseSimulation(int tickSpeed, int daysToRun) 
        {
            CheckIsDatabaseCreated();
            //Console.WriteLine("made it this far");
            //Console.ReadKey();
            //Console.ReadKey();

            var taskNullify = NullifyHamsters();
            var taskCreateNewSim = CreateAddSimulation();

            elapsedTicks = 0;
            elapsedDays = 0;
            tickTotalRunTime = daysToRun * 100; // tex 3 * 100 = 300 ticks totalt

            timeTicker = new TimeTicker(1000/tickSpeed); // sets time of the ticker to 1000 ms (1 second)
                                                         // divided by ex. 3 (ticks per second) = 333 ms
            timeTicker.SendOutTick += MethodListeningToTimerEvent;
            timeTicker.StartTimer();
        }

        internal async void MethodListeningToTimerEvent(object sender, TimerEventArgs e)
        {
            elapsedTicks = e.TickCounter;
            elapsedDays = e.TickDays;
            currentSimulationDate = e.TickDate;

            if (currentSimulationDate.Hour == 17 && currentSimulationDate.Minute == 0)
            {
                // avsluta dagen, avhämtning för hamstrarna
            }
            else
            {
                // kolla olika saker, do stuff
                var taskInitialMove = MoveToCages();
            }



        }


        private async Task MoveToCages()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    var hamstersWithNoCage = hamsterDb.Hamsters.Where(c => c.CageId == null && c.ExerciseAreaId == null).ToList();

                    for (int i = 0; i < hamstersWithNoCage.Count(); i++)
                    {
                        TimeSpan timeSpan = currentSimulationDate-hamstersWithNoCage[i].ActivityLogs.Where(a => 
                                                            a.Activity.ActivityName == "Exercise" && a.SimulationId == currentSimulationId)
                                                            .Select(a => a.TimeStamp).Single();
                        if (timeSpan.TotalMinutes > 60)
                        {
                            var exerciseArea = hamsterDb.ExerciseAreas.Where(e => e.Id == hamstersWithNoCage[i].ExerciseAreaId).Single();
                            hamstersWithNoCage[i].ExerciseAreaId = null;
                            exerciseArea.CageSize--;
                        }



                        hamstersWithNoCage[i].CageId = hamsterDb.Cages.Where(c => c.CageSize != 3 &&
                                                        c.Hamsters.FirstOrDefault().IsFemale == hamstersWithNoCage[i].IsFemale)
                                                        .Select(c => c.Id).First();
                        if (hamstersWithNoCage[i].CheckedInTime == null)
                        {
                            var addActivityLogTask = CreateAddActivityLog(hamsterDb.Activities.Where(a => a.ActivityName == "Arrived")
                                                .Select(a => a.Id).Single(), hamstersWithNoCage[i].Id);
                        }
                    }
                }
            });
        } // MoveArrivedHamstersToCages? Behöver den här vara async?
        private async Task MoveToExerciseArea()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {

                }
            });
        } // Behöver den här vara async?



        private async Task CreateAddSimulation()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    hamsterDb.Simulations.Add(new Simulation { Name = DateTime.Now.ToString() });
                    var currentSimulation = hamsterDb.Simulations.Last();
                    currentSimulationId = currentSimulation.Id;
                    hamsterDb.SaveChanges();
                }
            });
        }
        private async Task CreateAddActivityLog(int activityId, int hamsterId)
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    hamsterDb.ActivityLogs.Add(new ActivityLog { ActivityId = activityId, HamsterId = hamsterId,
                                                            SimulationId = currentSimulationId, TimeStamp = currentSimulationDate });
                    hamsterDb.SaveChanges();
                }
            });
        }



        private async Task NullifyHamsters()
        {
            var nullHamsterTask = NullHamster();
            var nullCageCageSize = NullCageSize(new Cage());
            var nullExerciseAreaCageSize = NullCageSize(new ExerciseArea());

            var taskArray = new Task[] { nullHamsterTask, nullCageCageSize, nullExerciseAreaCageSize };
            await Task.WhenAll(taskArray);
        }
        private async Task NullHamster()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    hamsterDb.Database.ExecuteSqlRaw("EXEC NullHamsterForNewSimulation"); // calling a stored procedure made in EF with migration
                    hamsterDb.SaveChanges();
                }
            });
        } // this method is for when for example the simulation was ended prematurely
        private async Task NullCageSize<T>(T cageType)
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    if (cageType.GetType() == typeof(Cage))
                    {
                        hamsterDb.Database.ExecuteSqlRaw("EXEC NullCageCageSize"); // calling a stored procedure made in EF with migration
                    }
                    else if (cageType.GetType() == typeof(ExerciseArea))
                    {
                        hamsterDb.Database.ExecuteSqlRaw("EXEC NullExerciseAreaCageSize"); // calling a stored procedure made in EF with migration
                    }
                    hamsterDb.SaveChanges();
                }
            });
        } // this method is for when for example the simulation was ended prematurely






        private void CheckIsDatabaseCreated()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                hamsterDb.Database.Migrate(); // EnsureCreated?
            }
        } // eventuellt ändra till EnsureCreated istället för Migrate
    }
}

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
        public event EventHandler<TickInfoEventArgs> SendTickInfo;
        public event EventHandler<DayInfoEventArgs> SendDayInfo;
        public event EventHandler<SimulationSummaryEventArgs> SendSimulationSummary;

        private int elapsedTicks;
        private int elapsedDays;
        private int tickTotalRunTime; // om 3 dagar, typ 3*100 = 300 ticks varpå varje 100 ticks
                                      // sover den i några sekunder för att simulera tiden mellan
                                      // 17:00 -> 07:00 samt ger tid för att läsa sammanfattningen på skärmen
        private int currentSimulationId;
        private DateTime currentSimulationDate;
        private TimeTicker timeTicker;
        public void ManipulateTimer()
        {
            timeTicker.ManipulateTimer();
        }

        public CareHouseSimulation(int tickSpeed, int daysToRun) 
        {
            CheckIsDatabaseCreated();

            var taskNullify = NullifyHamsters();
            var taskCreateNewSim = CreateAddSimulation();

            elapsedTicks = 0;
            elapsedDays = 0;
            tickTotalRunTime = daysToRun * 100; // tex 3 * 100 = 300 ticks totalt

            timeTicker = new TimeTicker(1000/tickSpeed); // sets time of the ticker to 1000 ms (1 second)
                                                         // divided by ex. 3 (ticks per second) = 333 ms
            SendSimulationSummary += timeTicker.StopTimer;
            timeTicker.SendOutTick += MethodListeningToTimerEvent;
            timeTicker.StartTimer();
        }

        internal async void MethodListeningToTimerEvent(object sender, TimerEventArgs e)
        {
            elapsedTicks = e.TickCounter;
            elapsedDays = e.TickDays;
            currentSimulationDate = e.TickDate;

            if (elapsedDays == tickTotalRunTime + 1) // avsluta timern och skicka ut total sammanställning av simulationen via event
            {
                OnSendSimulationSummary();
            }
            else if (currentSimulationDate.Hour == 7 && currentSimulationDate.Minute == 0) // påbörja dagen, ankomst av hamstrarna
            {
                await ArrivalOfHamsters();
                OnSendTickInfo();
            }
            else if (currentSimulationDate.Hour == 17 && currentSimulationDate.Minute == 0) // avsluta dagen, avhämtning för hamstrarna
            {
                await PickUpFromCages();
                OnSendDayInfo();
            }
            else
            {
                await MoveToExerciseArea(); // kolla olika saker, do stuff, every 6th minute-check. flytta till och från motion osv
                OnSendTickInfo();
            }
        }

        #region Event-Triggers
        private void OnSendTickInfo()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var hamstersInCages = hamsterDb.Hamsters.Where(h => h.CageId != null).GroupBy(c => c.CageId).ToList();
                var hamstersInExerciseArea = hamsterDb.Hamsters.Where(h => h.ExerciseAreaId != null).ToList();
                var tickSpecificActivityLogs = hamsterDb.ActivityLogs.Where(a => a.TimeStamp == currentSimulationDate
                                                                && a.SimulationId == currentSimulationId)
                                                                .OrderBy(a => a.Id).ToList();

                SendTickInfo?.Invoke(this, new TickInfoEventArgs(elapsedTicks, elapsedDays, currentSimulationId, 
                                        currentSimulationDate, hamstersInCages, hamstersInExerciseArea, tickSpecificActivityLogs));
            }
        }
        private void OnSendDayInfo()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var activityLogsPerHamster = hamsterDb.ActivityLogs.Where(a => a.TimeStamp.Day == currentSimulationDate.Day
                                                                 && a.SimulationId == currentSimulationId).GroupBy(h => h.HamsterId)
                                                                 .OrderBy(h => h.Key).ToList();

                SendDayInfo?.Invoke(this, new DayInfoEventArgs(elapsedTicks, elapsedDays, currentSimulationId,
                                        currentSimulationDate, activityLogsPerHamster));
            }
        }
        private void OnSendSimulationSummary()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var activityLogsPerHamster = hamsterDb.ActivityLogs.Where(a => a.SimulationId == currentSimulationId).GroupBy(h => h.HamsterId)
                                                                 .OrderBy(h => h.Key).ToList();

                SendSimulationSummary?.Invoke(this, new SimulationSummaryEventArgs(elapsedTicks, elapsedDays, currentSimulationId,
                                        currentSimulationDate, activityLogsPerHamster));
            }
        }
        #endregion

        #region Hamster Movement-functionality
        private async Task ArrivalOfHamsters()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var hamstersWithNoCageTask = hamsterDb.Hamsters.ToListAsync();
                var cagesTask = hamsterDb.Cages.ToListAsync();

                var taskArray = new Task[] { hamstersWithNoCageTask, cagesTask };
                await Task.WhenAll(taskArray);

                var hamstersWithNoCage = hamstersWithNoCageTask.Result;
                var cages = cagesTask.Result;

                var arrivalActivityId = hamsterDb.Activities.Where(a => a.ActivityName == "Arrival")
                                            .Select(a => a.Id).Single();
                var cageActivityId = hamsterDb.Activities.Where(a => a.ActivityName == "Cage")
                                            .Select(a => a.Id).Single();

                var taskList = new List<Task>();

                for (int i = 0; i < hamstersWithNoCage.Count(); i++)
                {
                    hamstersWithNoCage[i].CageId = cages.Where(c => c.CageSize < 4 
                                                && c.Hamsters.First().IsFemale == hamstersWithNoCage[i].IsFemale
                                                || c.CageSize == 0)
                                                .Select(c => c.Id).First();

                    var cage = cages.Where(c => c.Id == hamstersWithNoCage[i].CageId).Single();

                    var addArrivalActivityLogTask = CreateAddActivityLog(arrivalActivityId, hamstersWithNoCage[i].Id);
                    taskList.Add(addArrivalActivityLogTask);

                    var addCageActivityLogTask = CreateAddActivityLog(cageActivityId, hamstersWithNoCage[i].Id);
                    taskList.Add(addCageActivityLogTask);

                    hamstersWithNoCage[i].CheckedInTime = currentSimulationDate;

                    cage.CageSize++;
                }

                await Task.WhenAll(taskList);
                hamsterDb.SaveChanges();
            }
        } // typ klar?
        private async Task PickUpFromCages()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var taskOne = Task.Run(() => NullifyHamsters());
                var taskTwo = Task.Run(async () =>
                {
                    var hamsters = hamsterDb.Hamsters.ToList();
                    var activityId = hamsterDb.Activities.Where(a => a.ActivityName == "Departure")
                                            .Select(a => a.Id).Single();
                    var taskList = new List<Task>();

                    for (int i = 0; i < hamsters.Count(); i++)
                    {
                        var addActivityLogTask = CreateAddActivityLog(activityId, hamsters[i].Id);
                        taskList.Add(addActivityLogTask);
                    }

                    await Task.WhenAll(taskList);
                    hamsterDb.SaveChanges();
                });

                var taskArray = new Task[] { taskOne, taskTwo };
                await Task.WhenAll(taskArray);
            }
        } // typ klar?
        private async Task MoveToExerciseArea()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var exerciseArea = hamsterDb.ExerciseAreas.First();
                var activityId = hamsterDb.Activities.Where(a => a.ActivityName == "Cage")
                                .Select(a => a.Id).Single();

                var taskList = new List<Task>();

                if (exerciseArea.CageSize != 0)
                {
                    var hamstersInExerciseArea = hamsterDb.Hamsters.Where(h => h.ExerciseAreaId != null).ToList();

                    for (int i = 0; i < hamstersInExerciseArea.Count(); i++)
                    {
                        var checkTimeTask = TryRemoveFromExerciseArea(hamstersInExerciseArea[i], exerciseArea, activityId);
                        taskList.Add(checkTimeTask);
                    }

                    await Task.WhenAll(taskList);
                    hamsterDb.SaveChanges();

                    await TryAddToExerciseArea(exerciseArea);
                }
                else
                {
                    await TryAddToExerciseArea(exerciseArea); // lägga till 3-6 stycken hamstrar av samma kön
                }
            }
        }
        private async Task TryAddToExerciseArea(ExerciseArea exerciseArea)
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var activityId = hamsterDb.Activities.Where(a => a.ActivityName == "Exercise")
                            .Select(a => a.Id).Single();

                var hamstersNotInExerciseArea = hamsterDb.Hamsters.Where(c => c.ExerciseAreaId == null)
                                                    .OrderBy(t => t.LastExerciseTime)
                                                    .ThenBy(t => t.LastExerciseTime.HasValue)
                                                    .Select(c => c).ToList();

                var hamstersToAdd = hamstersNotInExerciseArea.Where(c => c.IsFemale == hamstersNotInExerciseArea.First().IsFemale)
                                                             .Take(6).ToList();

                var taskList = new List<Task>();

                for (int i = 0; i < hamstersToAdd.Count(); i++)
                {
                    var cage = hamsterDb.Cages.Where(c => c.Id == hamstersToAdd[i].CageId).Single();

                    hamstersToAdd[i].CageId = null;
                    cage.CageSize--;

                    hamstersToAdd[i].ExerciseAreaId = exerciseArea.Id;
                    exerciseArea.CageSize++;

                    var addActivityLogTask = CreateAddActivityLog(activityId, hamstersToAdd[i].Id);
                    taskList.Add(addActivityLogTask);

                    hamstersToAdd[i].LastExerciseTime = currentSimulationDate;
                }

                await Task.WhenAll(taskList);
                hamsterDb.SaveChanges();
            }
        }
        private async Task TryRemoveFromExerciseArea(Hamster hamster, ExerciseArea exerciseArea, int activityId)
        {
            await Task.Run(() =>
            {
                TimeSpan timeUntilEndOfDay = currentSimulationDate - new DateTime(currentSimulationDate.Year, currentSimulationDate.Month,
                            currentSimulationDate.Day, 17, 0, 0);

                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    TimeSpan timeSpan = currentSimulationDate - hamster.ActivityLogs.Where(a =>
                                        a.Activity.ActivityName == "Exercise" && a.SimulationId == currentSimulationId)
                                        .Select(a => a.TimeStamp).Single();

                    if (timeSpan.TotalMinutes >= 60)
                    {
                        if (timeUntilEndOfDay.TotalMinutes >= 120)
                        {
                            hamster.ExerciseAreaId = null;
                            exerciseArea.CageSize--;

                            var addCageActivityLogTask = CreateAddActivityLog(activityId, hamster.Id);

                            hamster.CageId = hamsterDb.Cages.Where(c => c.CageSize < 4
                                                    && c.Hamsters.First().IsFemale == hamster.IsFemale
                                                    || c.CageSize == 0)
                                                    .Select(c => c.Id).First();
                        }
                    }
                }
            });
        }
        #endregion






        #region Create Simulation & ActivityLog
        private async Task CreateAddSimulation()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {
                    hamsterDb.Simulations.Add(new Simulation { Name = "Temp" });
                    var currentSimulation = hamsterDb.Simulations.Last();
                    currentSimulation.Name = $"Simulation {currentSimulation.Id}- " + DateTime.Now.ToString();
                    currentSimulationId = currentSimulation.Id;
                    hamsterDb.SaveChanges();
                }
            });
        } // typ klar? göra till stored procedure ist?
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
        } // typ klar? göra till stored procedure ist?
        #endregion

        #region Reset or Nullify for new simulation
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
        #endregion

        #region Check if database is created
        private void CheckIsDatabaseCreated()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                hamsterDb.Database.Migrate(); // EnsureCreated?
            }
        } // eventuellt ändra till EnsureCreated istället för Migrate
        #endregion
    }
}

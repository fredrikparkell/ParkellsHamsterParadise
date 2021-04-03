using HamsterParadise.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
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
        private int tickTotalRunTime; // om 3 dagar, typ 3*100 = 300 ticks varpå varje 100 ticks
                                      // sover den i några sekunder för att simulera tiden mellan
                                      // 17:00 -> 07:00 samt ger tid för att läsa sammanfattningen på skärmen
        private int currentSimulationId;
        private DateTime currentSimulationDate;
        private TimeTicker timeTicker;

        public CareHouseSimulation(int tickSpeed, int daysToRun) 
        {
            CheckIsDatabaseCreated();

            elapsedTicks = 0;
            tickTotalRunTime = daysToRun * 100; // tex 3 * 100 = 300 ticks totalt
            currentSimulationDate = new DateTime(2021, 4, 01, 7, 0, 0);

            var taskOne = CreateAddSimulation();
            var taskTwo = MoveToCages();


            timeTicker = new TimeTicker(1000/tickSpeed); // sets time of the ticker to 1000 ms (1 second)
                                                         // divided by ex. 3 (ticks per second) = 333 ms
            timeTicker.SendTick += MethodListeningToTimerEvent;
            timeTicker.StartTimer();
        }

        internal async void MethodListeningToTimerEvent(object sender, TimerEventArgs e)
        {
            elapsedTicks = e.TickCounter;

            currentSimulationDate.AddMinutes(6);
            if (currentSimulationDate.Hour == 17 && currentSimulationDate.Minute == 0)
            {
                // avsluta dagen, avhämtning för hamstrarna
            }



        }


        private async Task MoveToCages()
        {
            await Task.Run(() =>
            {
                using (HamsterDbContext hamsterDb = new HamsterDbContext())
                {

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

        private void CheckIsDatabaseCreated()
        {
            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                hamsterDb.Database.Migrate(); // EnsureCreated?
            }
        } // eventuellt ändra till EnsureCreated istället för Migrate
    }
}

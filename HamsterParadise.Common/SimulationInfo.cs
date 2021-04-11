using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Includes methods that are using LINQ to query out information to send out to 
    /// the frontend where you can create reports of previously ran simulations
    /// </summary>
    public class SimulationInfo
    {
        #region Get Simulation information-methods
        /// <summary>
        /// Gets all Simulations that are registered in the database
        /// </summary>
        /// <returns>List<string> including the names of the simulations in the database</returns>
        public List<string> GetAllSimulations()
        {
            List<string> allSimulations = new List<string>();

            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                if (hamsterDb.Database.CanConnect())
                {
                    allSimulations = hamsterDb.Simulations.Where(s => s.ActivityLogs.Count() > 0)
                                      .OrderByDescending(s => s.Id)
                                      .Select(s => s.Name).ToList();
                }
            }

            if (allSimulations == null)
            {
                return new List<string>();
            }

            return allSimulations;
        }
        /// <summary>
        /// Gets all days that are connected to the specific simulation-id. 
        /// "How many days did the simulation run for?"
        /// </summary>
        /// <param name="simulationId"></param>
        /// <returns>List<string> including every day the simulation ran for</returns>
        public List<string> GetAllDays(int simulationId)
        {
            List<string> allDays = new List<string>();

            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                var allDaysIGrouping = hamsterDb.ActivityLogs.Where(s => s.SimulationId == simulationId)
                                                .AsEnumerable().GroupBy(t => t.TimeStamp.Day).ToList();
                allDays = allDaysIGrouping.Select(t => t.Key.ToString()).ToList();
            }

            return allDays;
        }
        #endregion

        #region Get Simulation data-methods
        /// <summary>
        /// Gets all ActivityLogs connected to each hamster specificed to a particular
        /// day and a specific simulation.
        /// </summary>
        /// <param name="day"></param>
        /// <param name="simulationId"></param>
        /// <returns>Uses the DayInfoEventArgs as a return type to include the data
        /// needed to create reports about the hamsters in the particular day with
        /// the specific simulation-id</returns>
        public DayInfoEventArgs GetAllActivityLogsPerDay(int day, int simulationId)
        {
            List<IGrouping<int, ActivityLog>> activityLogsPerHamster;
            List<Hamster> hamsters = new List<Hamster>();
            DateTime currentDate = new DateTime();

            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                activityLogsPerHamster = hamsterDb.ActivityLogs.Where(a => a.TimeStamp.Day == day
                                                                 && a.SimulationId == simulationId).AsEnumerable().GroupBy(h => h.HamsterId)
                                                                 .OrderBy(h => h.Key).ToList();
                hamsters = hamsterDb.Hamsters.ToList();

                currentDate = hamsterDb.ActivityLogs.Where(a => a.TimeStamp.Day == day && a.SimulationId == simulationId)
                                                    .Select(t => t.TimeStamp).First();
            }

            return new DayInfoEventArgs(0, day, simulationId, currentDate, activityLogsPerHamster, hamsters);
        }
        /// <summary>
        /// Gets all ActivityLogs connected to each hamster specificed to a specific simulation.
        /// </summary>
        /// <param name="totalDays"></param>
        /// <param name="simulationId"></param>
        /// <returns>Uses the SimulationSummaryEventArgs as a return type to include the data
        /// needed to create reports about the hamsters for the whole run of days that the simulation
        /// ran with the specific simulation-id</returns>
        public SimulationSummaryEventArgs GetAllActivityLogsPerSimulation(int totalDays, int simulationId)
        {
            List<IGrouping<int, ActivityLog>> activityLogsPerHamster;
            List<Hamster> hamsters = new List<Hamster>();

            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                activityLogsPerHamster = hamsterDb.ActivityLogs.Where(a => a.SimulationId == simulationId).AsEnumerable().GroupBy(h => h.HamsterId)
                                                                 .OrderBy(h => h.Key).ToList();
                hamsters = hamsterDb.Hamsters.ToList();
            }

            return new SimulationSummaryEventArgs(0,totalDays,simulationId,DateTime.Now,activityLogsPerHamster, hamsters);
        }
        #endregion
    }
}

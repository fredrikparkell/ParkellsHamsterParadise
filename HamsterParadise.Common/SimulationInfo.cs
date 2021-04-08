using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    public class SimulationInfo
    {
        public List<string> GetAllSimulations()
        {
            List<string> allSimulations = new List<string>();

            using (HamsterDbContext hamsterDb = new HamsterDbContext())
            {
                allSimulations = hamsterDb.Simulations.Where(s => s.ActivityLogs.Count() > 0)
                                                      .OrderByDescending(s => s.Id)
                                                      .Select(s => s.Name).ToList();
            }

            return allSimulations;
        }
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
    }
}

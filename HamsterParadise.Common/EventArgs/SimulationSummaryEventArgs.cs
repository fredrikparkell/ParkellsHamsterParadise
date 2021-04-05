using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    public class SimulationSummaryEventArgs
    {
        public int ElapsedTicks { get; }
        public int ElapsedDays { get; }
        public int CurrentSimulationId { get; }
        public DateTime CurrentSimulationDate { get; }

        public List<IGrouping<int, ActivityLog>> ActivityLogsPerHamster { get; }

        public SimulationSummaryEventArgs(int elapsedTicks, int elapsedDays, int currentSimulationId, DateTime currentSimulationDate, List<IGrouping<int, ActivityLog>> activityLogsPerHamster)
        {
            ElapsedTicks = elapsedTicks;
            ElapsedDays = elapsedDays;
            CurrentSimulationId = currentSimulationId;
            CurrentSimulationDate = currentSimulationDate;

            ActivityLogsPerHamster = activityLogsPerHamster;
        }
    }
}

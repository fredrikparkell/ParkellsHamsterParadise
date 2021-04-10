using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Custom EventArgs sending out information that is used for creating 
    /// and printing/writing reports. In my UI this goes to the UserPrint-class
    /// where its written out in the console window. This EventArgs is used for the current Tick reports.
    /// </summary>
    public class TickInfoEventArgs
    {
        public int ElapsedTicks { get; }
        public int ElapsedDays { get; }
        public int CurrentSimulationId { get; }
        public DateTime CurrentSimulationDate { get; }

        public List<IGrouping<int?,Hamster>> HamstersInCages { get; }
        public List<Hamster> HamstersInExerciseArea { get; }
        public List<ActivityLog> ActivityLogs { get; }
        
        public TickInfoEventArgs(int elapsedTicks, int elapsedDays, int currentSimulationId, DateTime currentSimulationDate,
                              List<IGrouping<int?,Hamster>> hamstersInCages, List<Hamster> hamstersInExerciseArea, List<ActivityLog> activityLogs)
        {
            ElapsedTicks = elapsedTicks;
            ElapsedDays = elapsedDays;
            CurrentSimulationId = currentSimulationId;
            CurrentSimulationDate = currentSimulationDate;

            HamstersInCages = hamstersInCages;
            HamstersInExerciseArea = hamstersInExerciseArea;
            ActivityLogs = activityLogs;
        }
    }
}

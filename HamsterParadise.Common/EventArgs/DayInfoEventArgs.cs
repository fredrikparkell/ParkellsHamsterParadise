using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Custom EventArgs used for sending out information that is used for creating 
    /// and printing/writing reports. In my UI this goes to the UserPrint-class
    /// where its written out in the console window. This EventArgs is used for Daily reports.
    /// </summary>
    public class DayInfoEventArgs
    {
        #region Propertys
        public int ElapsedTicks { get; }
        public int ElapsedDays { get; }
        public int CurrentSimulationId { get; }
        public DateTime CurrentSimulationDate { get; }

        public List<IGrouping<int,ActivityLog>> ActivityLogsPerHamster { get; }
        public List<Hamster> Hamsters { get; }
        #endregion

        #region Constructor
        public DayInfoEventArgs(int elapsedTicks, int elapsedDays, int currentSimulationId, DateTime currentSimulationDate,
                                        List<IGrouping<int, ActivityLog>> activityLogsPerHamster, List<Hamster> hamsters)
        {
            ElapsedTicks = elapsedTicks;
            ElapsedDays = elapsedDays;
            CurrentSimulationId = currentSimulationId;
            CurrentSimulationDate = currentSimulationDate;

            ActivityLogsPerHamster = activityLogsPerHamster;
            Hamsters = hamsters;
        }
        #endregion
    }
}

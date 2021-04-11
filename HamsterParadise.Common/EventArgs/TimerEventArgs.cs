using System;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Custom EventArgs used for sending out information that is used and needed for
    /// the simulation to run its course, from the TimeTicker-class to the CareHouseSimulation-class. 
    /// This EventArgs is used to link up the Timer with the Simulation.
    /// </summary>
    internal class TimerEventArgs
    {
        #region Propertys
        public int TickCounter { get; }
        public DateTime TickDate { get; }
        public int TickDays { get; }
        #endregion

        #region Constructor
        public TimerEventArgs(int tickCounter, DateTime tickDate, int tickDays)
        {
            TickCounter = tickCounter;
            TickDate = tickDate;
            TickDays = tickDays;
        }
        #endregion
    }
}
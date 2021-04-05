using System;

namespace HamsterParadise.Common
{
    internal class TimerEventArgs
    {
        public int TickCounter { get; }
        public DateTime TickDate { get; }
        public int TickDays { get; }

        public TimerEventArgs(int tickCounter, DateTime tickDate, int tickDays)
        {
            TickCounter = tickCounter;
            TickDate = tickDate;
            TickDays = tickDays;
        }
    }
}
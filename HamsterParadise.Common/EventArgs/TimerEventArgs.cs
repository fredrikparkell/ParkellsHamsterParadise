namespace HamsterParadise.Common
{
    internal class TimerEventArgs
    {
        public int TickCounter { get; }
        public TimerEventArgs(int tickCounter)
        {
            TickCounter = tickCounter;
        }
    }
}
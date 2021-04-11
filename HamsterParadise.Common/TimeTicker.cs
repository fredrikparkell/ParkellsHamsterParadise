using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    /// <summary>
    /// Custom-made ticker-class that keeps the simulation running by
    /// running the SendOutTicks-method every time the Timer's TimerCallback is
    /// called (based on the tickspeed that the user chooses).
    /// </summary>
    internal class TimeTicker
    {
        #region Events
        public event EventHandler<TimerEventArgs> SendOutTick;
        #endregion

        #region Fields & Propertys
        private int tickCounter;
        private int tickDayCounter;
        private int tickSpeed;
        private bool isRunning;
        private DateTime currentSimulationDate;
        private Timer tickTimer;
        internal Timer TickTimer { get => tickTimer; set => tickTimer = value; }
        #endregion

        public TimeTicker(int ticktickSpeed)
        {
            tickSpeed = ticktickSpeed;
            tickCounter = 0;
            tickDayCounter = 1;
            currentSimulationDate = new DateTime(2021, 4, 01, 7, 0, 0);
        }

        private void SendOutTicks(object state)
        {
            tickCounter++;

            if (currentSimulationDate.Hour == 17 && currentSimulationDate.Minute == 0)
            { 
                currentSimulationDate = currentSimulationDate.AddHours(14);
                tickDayCounter++;
            }
            else
            {
                currentSimulationDate = currentSimulationDate.AddMinutes(6);
            }

            SendOutTick?.Invoke(this, new TimerEventArgs(tickCounter, currentSimulationDate, tickDayCounter));
        }

        #region Timer-methods
        internal void StartTimer()
        {
            isRunning = true;
            TickTimer = new Timer(new TimerCallback(SendOutTicks), null, 1000, tickSpeed);
        }
        internal void ManipulateTimer()
        {
            if (isRunning)
            {
                TickTimer.Change(Timeout.Infinite, Timeout.Infinite);
                isRunning = false;
            }
            else
            {
                TickTimer.Change(0, tickSpeed);
                isRunning = true;
            }
        }
        internal async void StopTimer(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() =>
            {
                TickTimer.Change(Timeout.Infinite, Timeout.Infinite);
            });
        }
        #endregion
    }
}

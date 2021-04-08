using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    internal class TimeTicker
    {
        public event EventHandler<TimerEventArgs> SendOutTick;

        private int tickCounter;
        private int tickDayCounter;
        private int tickSpeed;

        private bool isRunning;
        private DateTime currentSimulationDate;
        private Timer tickTimer;
        internal Timer TickTimer { get => tickTimer; set => tickTimer = value; }

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
    }
}

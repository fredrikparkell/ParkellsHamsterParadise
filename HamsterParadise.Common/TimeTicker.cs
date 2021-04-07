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
        DateTime startTime = new DateTime();
        DateTime newTime = new DateTime();

        public event EventHandler<TimerEventArgs> SendOutTick;

        private int tickCounter;
        private int tickDayCounter; // kanske kan klara mig utan den här eller lägga den nån annanstans
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
            
            //if (tickCounter == 0) { startTime = DateTime.Now; Console.WriteLine("\n"); }
            //else { startTime = newTime; }
            //newTime = DateTime.Now;

            //if (tickCounter != 0) { TimeSpan time = startTime - newTime; Console.WriteLine(time); }

            tickCounter++;

            if (currentSimulationDate.Hour == 17 && currentSimulationDate.Minute == 0) // ful-lösning?? tickCounter.ToString().EndsWith("01")
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

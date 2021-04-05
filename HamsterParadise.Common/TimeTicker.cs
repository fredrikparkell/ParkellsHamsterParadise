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
        private int tickDayCounter; // kanske kan klara mig utan den här eller lägga den nån annanstans
        private int tickSpeed;
        private DateTime currentSimulationDate;
        private Timer tickTimer;
        internal Timer TickTimer { get => tickTimer; set => tickTimer = value; }

        public TimeTicker(int ticktickSpeed)
        {
            tickSpeed = ticktickSpeed;
            tickCounter = 0;
            tickDayCounter = 1;
            currentSimulationDate = new DateTime(2021, 4, 01, 6, 54, 0);
        }

        private void SendOutTicks(object state)
        {
            tickCounter++;

            if (tickCounter.ToString().EndsWith("01")) // ful-lösning?? 
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
            TickTimer = new Timer(new TimerCallback(SendOutTicks), null, 1000, tickSpeed);
        }
        internal void ManipulateTimer() // koppla ihop den här metoden med att man trycker enter eller nån knapp så körs detta. nått event typ
        {
            // pause or resume timer
        }
        internal void StopTimer() // koppla ihop den här metoden med att när dagarna är slut så körs detta. nått event typ
        {
            // stop timer
        }
    }
}

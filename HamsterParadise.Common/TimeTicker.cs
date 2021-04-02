using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    //+ TickEvent:event
    //- TickCounter:int
    //- TickDayCounter:int
    //- TickSpeed:int
    //- TickTimer:Timer

    //+ StartTimer() :void
    //+ ManipulateTimer(string resume/pause) :void
    //+ StopTimer() :void
    internal class TimeTicker
    {
        public event EventHandler<TimerEventArgs> SendTick;
        private int tickCounter;
        private int tickDayCounter; // kanske kan klara mig utan den här eller lägga den nån annanstans
        private int tickSpeed;
        private Timer tickTimer;
        internal Timer TickTimer { get => tickTimer; set => tickTimer = value; }

        public TimeTicker(int ticktickSpeed)
        {
            tickSpeed = ticktickSpeed;
            tickCounter = 0;
        }

        private void OnSendTicks(object state)
        {

        }

        internal void StartTimer()
        {
            // start timer
            TickTimer = new Timer(new TimerCallback(this.OnSendTicks), null, 1000, tickSpeed);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    // eventuellt lägga till ett till projekt/assembly för API eller annan kommunikation utåt, dvs inte UI
    public class CareHouseSimulation
    {
        private int elapsedTicks;
        private int tickTotalRunTime; // om 3 dagar, typ 3*100 = 300 ticks varpå varje 100 ticks
                                      // sover den i några sekunder för att simulera tiden mellan
                                      // 17:00 -> 07:00 samt ger tid för att läsa sammanfattningen på skärmen


        public CareHouseSimulation(int tickSpeed, int daysToRun) // tickSpeed = ticks per sekund, får göra nån omvandling
        {
            tickTotalRunTime = daysToRun;



            TimeTicker timeTicker = new TimeTicker(tickSpeed);
            timeTicker.StartTimer();
        }

        internal async void MethodListeningToTimerEvent(object sender, TimerEventArgs e)
        {

        }
    }
}

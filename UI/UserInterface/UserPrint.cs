using HamsterParadise.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace UI
{
    public class UserPrint
    {
        public async void PrintTickInfo(object sender, TickInfoEventArgs e)
        {        
            // Kunna visa:
            // -Datumet/tiden/ticket/dag
            // -Vilka hamstrar i vilka burar + ExerciseArea
            // -Alla ActivityLogs som reggats detta tick

            await Task.Run(() =>
            {
                Console.CursorVisible = false;
                string[] days = new string[7] { "[Monday],", "[Tuesday],", "[Wednesday],", "[Thursday],", "[Friday],", "[Saturday],", "[Sunday]" };

            });
        }
        public async void PrintDayInfo(object sender, DayInfoEventArgs e)
        {
            // Kunna visa:
            // -Datumet/tiden/ticket/dag
            // -Vad hamstrarna gjort (sammanfattning)
            //    - Avlämnade, Burtid, Motionstid, Hämtade
            // -Hur lång tid fick hamstrarna vänta på motion, dvs tid mellan incheckning och motionstillfälle
            //    - Tidsskillnaden mellan incheckning och första motionstillfället
            // -Hur många gånger har hamstrarna hunnit motionera
            //    - Kolla på ActivityLogs med hamster-id:t, simulations-id:t och activity-id:t

            await Task.Run(() =>
            {
                Console.Clear();
                Console.CursorVisible = false;


            });
        }
        public async void PrintSimulationSummary(object sender, SimulationSummaryEventArgs e)
        {
            await Task.Run(() =>
            {
                Console.Clear();
                Console.CursorVisible = false;


            });
        }
    }
}

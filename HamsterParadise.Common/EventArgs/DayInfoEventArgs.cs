using HamsterParadise.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.Common
{
    public class DayInfoEventArgs
    {
        // Kunna visa:
        // -Datumet/tiden/ticket/dag
        // -Vad hamstrarna gjort (sammanfattning)
        //    - Avlämnade, Burtid, Motionstid, Hämtade
        // -Hur lång tid fick hamstrarna vänta på motion, dvs tid mellan incheckning och motionstillfälle
        //    - Tidsskillnaden mellan incheckning och första motionstillfället
        // -Hur många gånger har hamstrarna hunnit motionera
        //    - Kolla på ActivityLogs med hamster-id:t, simulations-id:t och activity-id:t

        public int ElapsedTicks { get; }
        public int ElapsedDays { get; }
        public int CurrentSimulationId { get; }
        public DateTime CurrentSimulationDate { get; }

        public List<IGrouping<int,ActivityLog>> ActivityLogsPerHamster { get; }

        public DayInfoEventArgs(int elapsedTicks, int elapsedDays, int currentSimulationId, DateTime currentSimulationDate,
                                        List<IGrouping<int, ActivityLog>> activityLogsPerHamster)
        {
            ElapsedTicks = elapsedTicks;
            ElapsedDays = elapsedDays;
            CurrentSimulationId = currentSimulationId;
            CurrentSimulationDate = currentSimulationDate;

            ActivityLogsPerHamster = activityLogsPerHamster;
        }
    }
}

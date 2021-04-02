using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class ActivityLog
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }

        public int HamsterId { get; set; }
        public virtual Hamster Hamster { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public int SimulationId { get; set; }
        public virtual Simulation Simulation { get; set; }
    }
}

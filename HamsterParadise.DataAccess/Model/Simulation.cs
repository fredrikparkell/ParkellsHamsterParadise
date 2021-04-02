using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class Simulation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}

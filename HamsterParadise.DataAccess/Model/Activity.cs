using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class Activity
    {
        public int Id { get; set; }
        public string ActivityName { get; set; } // ankomst, motion, dagbur, avhämtning

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}

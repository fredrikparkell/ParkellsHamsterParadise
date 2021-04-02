using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class Cage
    {
        public int Id { get; set; }
        public int CageSize { get; set; }

        public virtual ICollection<Hamster> Hamsters { get; set; }
    }
}

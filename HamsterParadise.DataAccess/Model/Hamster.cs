using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterParadise.DataAccess
{
    public class Hamster
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsFemale { get; set; }
        public DateTime? CheckedInTime { get; set; }
        public DateTime? LastExerciseTime { get; set; }

        public int OwnerId { get; set; }
        public virtual Owner Owner { get; set; }

        public int? CageId { get; set; }
        public virtual Cage Cage { get; set; }

        public int? ExerciseAreaId { get; set; }
        public virtual ExerciseArea ExerciseArea { get; set; }

        public virtual ICollection<ActivityLog> ActivityLogs { get; set; }
    }
}

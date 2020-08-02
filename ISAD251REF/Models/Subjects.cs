using System;
using System.Collections.Generic;

namespace ISAD251REF.Models
{
    public partial class Subjects
    {
        public Subjects()
        {
            Deadlines = new HashSet<Deadlines>();
        }

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }

        public virtual ICollection<Deadlines> Deadlines { get; set; }
    }
}

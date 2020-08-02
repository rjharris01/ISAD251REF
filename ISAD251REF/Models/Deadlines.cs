using System;
using System.Collections.Generic;

namespace ISAD251REF.Models
{
    public partial class Deadlines
    {
        public int DeadlineId { get; set; }
        public int? SubjectId { get; set; }
        public int? DeadlineTypeId { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string DeadlineNotes { get; set; }

        public virtual DeadlineTypes DeadlineType { get; set; }
        public virtual Subjects Subject { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISAD251REF.Models
{
    public class New_Deadline
    {
        public int SubjectID { get; set; }
        public int DeadlineTypeID { get; set; }
        public DateTime DeadlineDate { get; set; }
        public string DeadlineNotes { get; set; }
    }
}

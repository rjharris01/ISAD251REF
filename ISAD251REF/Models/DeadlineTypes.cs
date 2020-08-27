using System;
using System.Collections.Generic;

//stores the Deadlines model class
namespace ISAD251REF.Models
{
    public partial class DeadlineTypes
    {
        public DeadlineTypes()
        {
            Deadlines = new HashSet<Deadlines>();
        }

        public int DeadlineTypeId { get; set; }
        public string DeadlineTypeName { get; set; }

        public virtual ICollection<Deadlines> Deadlines { get; set; }
    }
}

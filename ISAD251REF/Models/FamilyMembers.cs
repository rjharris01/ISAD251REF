using System;
using System.Collections.Generic;

namespace ISAD251REF.Models
{
    public partial class FamilyMembers
    {
        public FamilyMembers()
        {
            Appointments = new HashSet<Appointments>();
        }

        public int FamilyMemberId { get; set; }
        public string FamilyMemberName { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}

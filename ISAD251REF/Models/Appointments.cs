using System;
using System.Collections.Generic;

namespace ISAD251REF.Models
{
    public partial class Appointments
    {
        public int AppointmentId { get; set; }
        public int? FamilyMemberId { get; set; }
        public int? AppointmentTypeId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }

        public virtual AppointmentTypes AppointmentType { get; set; }
        public virtual FamilyMembers FamilyMember { get; set; }
    }
}

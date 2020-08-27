using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISAD251REF.Models
{
    public class New_Appointment
    { 
        public int FamilyMemberID { get; set; }
        public int AppointmentTypeID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string AppointmentNotes { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace ISAD251REF.Models
{
    public partial class AppointmentTypes
    {
        public AppointmentTypes()
        {
            Appointments = new HashSet<Appointments>();
        }

        public int AppointmentTypesId { get; set; }
        public string AppointmentTypeName { get; set; }

        public virtual ICollection<Appointments> Appointments { get; set; }
    }
}

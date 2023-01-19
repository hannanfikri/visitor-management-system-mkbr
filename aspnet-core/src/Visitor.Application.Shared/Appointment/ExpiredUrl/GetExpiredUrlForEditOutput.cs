using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment.ExpiredUrl
{
    public class GetExpiredUrlForEditOutput
    {
        public CreateOrEditExpiredUrlDto ExpiredUrl { get; set; }

        public string AppointmentFullName { get; set; }
    }
}

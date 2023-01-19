using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment.ExpiredUrl
{
    public class GetExpiredUrlForViewDto
    {
        public ExpiredUrlDto ExpiredUrl { get; set; }

        public string AppointmentFullName { get; set; }
    }
}

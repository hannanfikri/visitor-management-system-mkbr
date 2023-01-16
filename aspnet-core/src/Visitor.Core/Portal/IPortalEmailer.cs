using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment;

namespace Visitor.core.Portal
{
    public interface IPortalEmailer
    {
        Task SendEmailDetailBookingAsync(AppointmentEnt appointment);
    }
}

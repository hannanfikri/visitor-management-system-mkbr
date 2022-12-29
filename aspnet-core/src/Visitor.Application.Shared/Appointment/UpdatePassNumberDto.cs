using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    public class UpdatePassNumberDto: Entity<Guid>
    {
        public string PassNumber { get; set; }
    }
}

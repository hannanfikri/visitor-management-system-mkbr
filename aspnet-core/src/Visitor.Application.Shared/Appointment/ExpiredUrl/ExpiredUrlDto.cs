using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment.ExpiredUrl
{
    public class ExpiredUrlDto : EntityDto<Guid>
    {
        public DateTime UrlCreateDate { get; set; }

        public DateTime UrlExpiredDate { get; set; }

        public string Item { get; set; }

        public string Status { get; set; }

        public Guid? AppointmentId { get; set; }
    }
}

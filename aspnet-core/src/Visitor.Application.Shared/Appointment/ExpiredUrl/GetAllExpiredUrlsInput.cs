using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment.ExpiredUrl
{
    public class GetAllExpiredUrlsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public DateTime? MaxUrlCreateDateFilter { get; set; }
        public DateTime? MinUrlCreateDateFilter { get; set; }

        public DateTime? MaxUrlExpiredDateFilter { get; set; }
        public DateTime? MinUrlExpiredDateFilter { get; set; }

        public string ItemFilter { get; set; }

        public string AppointmentFullNameFilter { get; set; }
    }
}

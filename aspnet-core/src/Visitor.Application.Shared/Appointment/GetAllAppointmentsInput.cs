


using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    public class GetAllAppointmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string FullNameFilter { get; set; }


    }
}

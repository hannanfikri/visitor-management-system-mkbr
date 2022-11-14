using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    public class GetAllForLookUpTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        //public  DateTime Today { get; set; }
    }
}

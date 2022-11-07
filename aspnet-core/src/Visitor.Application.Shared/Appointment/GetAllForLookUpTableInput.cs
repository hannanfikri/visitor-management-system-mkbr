using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Appointment
{
    internal class GetAllForLookUpTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}

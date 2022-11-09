using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Status.Dtos
{
    public class GetAllStatusesInput :  PagedSortedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string StatusFilter { get; set; }
    }
}

using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.PurposeOfVisit.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedInputDto
    {
        public string filter { get; set; }
    }
}

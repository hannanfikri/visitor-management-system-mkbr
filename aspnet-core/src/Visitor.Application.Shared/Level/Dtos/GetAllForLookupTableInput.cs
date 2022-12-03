using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Level.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedInputDto
    {
        public string filter { get; set; }
    }
}

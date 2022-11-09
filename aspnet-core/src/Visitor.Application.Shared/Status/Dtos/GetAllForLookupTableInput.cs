using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Status.Dtos
{
    public class GetAllForLookupTableInput :PagedAndSortedInputDto
    {
        public string filter { get; set; }
    }
}

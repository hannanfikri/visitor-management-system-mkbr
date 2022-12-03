using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Title.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedInputDto
    {
        public string VisitorTitle { get; set; }
    }
}

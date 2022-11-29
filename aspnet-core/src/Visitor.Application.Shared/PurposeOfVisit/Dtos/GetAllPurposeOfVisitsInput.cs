using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.PurposeOfVisit.Dtos
{
    public class GetAllPurposeOfVisitsInput: PagedSortedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string POFFilter { get; set; }
    }
}

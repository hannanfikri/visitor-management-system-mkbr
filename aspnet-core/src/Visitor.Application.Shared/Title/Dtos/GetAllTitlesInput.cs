using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Title.Dtos
{
    public  class GetAllTitlesInput : PagedSortedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string TitleFilter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Blacklist.Dtos
{
    public class GetAllBlacklistsInput: PagedSortedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string FullNameFilter { get; set; }


    }
}

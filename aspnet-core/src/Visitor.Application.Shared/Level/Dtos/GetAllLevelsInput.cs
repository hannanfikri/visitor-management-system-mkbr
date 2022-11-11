using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Level.Dtos
{
    public class GetAllLevelsInput : PagedSortedAndFilteredInputDto
    {
        public string Filter { get; set; }
        public string LevelFilter { get; set; }
    }
}

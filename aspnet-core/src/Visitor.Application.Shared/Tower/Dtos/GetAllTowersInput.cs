using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Tower.Dtos
{
    public class GetAllTowersInput:PagedSortedAndFilteredInputDto
    {
            public string Filter { get; set; }
            public string TowerFilter { get; set; }
    }
}

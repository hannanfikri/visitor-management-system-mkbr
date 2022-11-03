using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Blacklist.Dtos
{
    public class GetAllBlacklistsForExcelInput: EntityDto<Guid?>
    {
        public string Filter { get; set; }
        public string FullNameFilter { get; set; }
    }
}

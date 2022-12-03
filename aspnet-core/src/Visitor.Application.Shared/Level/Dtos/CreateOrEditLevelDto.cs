using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Visitor.Level.Dtos
{
    public class CreateOrEditLevelDto : EntityDto<Guid?>
    {
        public string LevelBankRakyat { get; set; }
    }
}

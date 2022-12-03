using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Level.Dtos
{
    public class LevelDto : EntityDto<Guid>
    {
        public string LevelBankRakyat { get; set; }
    }
}

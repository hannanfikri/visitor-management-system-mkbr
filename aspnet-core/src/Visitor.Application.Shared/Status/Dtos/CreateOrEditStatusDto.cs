using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Status.Dtos
{
    public class CreateOrEditStatusDto:EntityDto<Guid?>
    {
        public string StatusApp { get; set; }
    }
}

using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace Visitor.Title.Dtos
{
    public class CreateOrEditTitleDto : EntityDto<Guid?>
    {
        public string VisitorTitle { get; set; }
    }
}

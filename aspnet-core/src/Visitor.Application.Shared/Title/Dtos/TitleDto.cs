using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Title.Dtos
{
    public class TitleDto : EntityDto<Guid>
    {
        public string VisitorTitle { get; set; }
    }
}

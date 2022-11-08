using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Visitor.Departments.Dtos
{
    public class DepartmentDto : EntityDto<Guid?>
    {
        public string DepartmentName { get; set; }
    }
}

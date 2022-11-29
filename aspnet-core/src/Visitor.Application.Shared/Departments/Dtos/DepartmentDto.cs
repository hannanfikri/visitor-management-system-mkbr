using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Visitor.Appointment;

namespace Visitor.Departments.Dtos
{
    public class DepartmentDto : EntityDto<Guid?>
    {
        public string DepartmentName { get; set; }
    }
}

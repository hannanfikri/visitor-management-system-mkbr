using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Visitor.Dto;

namespace Visitor.Departments.Dtos
{
    public class GetAllDepartmentsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string DepartmentNameFilter { get; set; }
    }
}

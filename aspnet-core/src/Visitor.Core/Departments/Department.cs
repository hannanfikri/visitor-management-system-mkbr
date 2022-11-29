using Abp.Domain.Entities;
using Org.BouncyCastle.Asn1.Mozilla;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment;

namespace Visitor.Departments
{
    public class Department : Entity<Guid>
    {
        public string DepartmentName { get; set; }

        private Department() { }
        public Department(string departmentName)
        {
            DepartmentName = departmentName;
        }
    }
}

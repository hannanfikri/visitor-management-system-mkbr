using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace Visitor.Company
{
    [Table("companies")]
    [Audited]
    public class CompanyEnt : Entity<Guid> 
    {
        [Required]
        public virtual string CompanyName { get; set; }
        public virtual string CompanyAddress { get; set; }
        public virtual string OfficePhoneNumber { get; set; }
        public virtual string CompanyEmail { get; set; }

    }
}
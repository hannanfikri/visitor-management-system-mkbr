using System;
using Abp.Application.Services.Dto;

namespace Visitor.Company.Dtos
{
    public class CompanyDto : EntityDto<Guid>
    {
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string CompanyAddress { get; set; }

    }
}
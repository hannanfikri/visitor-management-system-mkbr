using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Visitor.Company.Dtos
{
    public class CreateOrEditCompanyDto : EntityDto<Guid?>
    {

        [Required]
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string OfficePhoneNumber { get; set; }
        public string CompanyAddress { get; set; }

    }
}
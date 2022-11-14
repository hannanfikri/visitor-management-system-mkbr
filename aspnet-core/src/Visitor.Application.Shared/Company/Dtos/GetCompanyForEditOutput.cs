using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Visitor.Company.Dtos
{
    public class GetCompanyForEditOutput
    {
        public CreateOrEditCompanyDto Company { get; set; }

    }
}
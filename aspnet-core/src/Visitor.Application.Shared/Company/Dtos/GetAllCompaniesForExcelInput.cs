using Abp.Application.Services.Dto;
using System;

namespace Visitor.Company.Dtos
{
    public class GetAllCompaniesForExcelInput
    {
        public string Filter { get; set; }

        public string CompanyNameFilter { get; set; }
        

    }
}
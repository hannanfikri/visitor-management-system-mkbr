using Abp.Application.Services.Dto;
using System;

namespace Visitor.Company.Dtos
{
    public class GetAllCompaniesInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }

        public string CompanyNameFilter { get; set; }

    }
}
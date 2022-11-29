using System.Collections.Generic;
using Visitor.Company.Dtos;
using Visitor.Dto;

namespace Visitor.Company.Exporting
{
    public interface ICompaniesExcelExporter
    {
        FileDto ExportToFile(List<GetCompanyForViewDto> companies);
    }
}
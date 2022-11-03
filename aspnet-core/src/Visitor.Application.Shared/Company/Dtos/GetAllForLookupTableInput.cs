using Abp.Application.Services.Dto;

namespace Visitor.Company.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}
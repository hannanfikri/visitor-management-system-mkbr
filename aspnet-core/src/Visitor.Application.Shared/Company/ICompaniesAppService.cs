using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Visitor.Company.Dtos;
using Visitor.Dto;

namespace Visitor.Company
{
    public interface ICompaniesAppService : IApplicationService
    {
        Task<PagedResultDto<GetCompanyForViewDto>> GetAll(GetAllCompaniesInput input);

        Task<GetCompanyForViewDto> GetCompanyForView(Guid id);
        
        Task<GetCompanyForEditOutput> GetCompanyForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditCompanyDto input);

        Task Delete(EntityDto<Guid> input);
    }
}
using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.Editions.Dto;
using Visitor.MultiTenancy.Dto;

namespace Visitor.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}
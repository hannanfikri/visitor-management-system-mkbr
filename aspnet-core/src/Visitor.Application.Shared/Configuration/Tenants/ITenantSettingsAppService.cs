using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.Configuration.Tenants.Dto;

namespace Visitor.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}

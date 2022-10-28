using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.Configuration.Host.Dto;

namespace Visitor.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}

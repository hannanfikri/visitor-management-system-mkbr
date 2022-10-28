using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.Install.Dto;

namespace Visitor.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}
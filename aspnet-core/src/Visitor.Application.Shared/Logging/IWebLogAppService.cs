using Abp.Application.Services;
using Visitor.Dto;
using Visitor.Logging.Dto;

namespace Visitor.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Visitor.Caching.Dto;

namespace Visitor.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}

using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Visitor.Common.Dto;
using Visitor.Editions.Dto;

namespace Visitor.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}
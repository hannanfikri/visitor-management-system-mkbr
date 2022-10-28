using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Visitor.Authorization.Permissions.Dto;

namespace Visitor.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}

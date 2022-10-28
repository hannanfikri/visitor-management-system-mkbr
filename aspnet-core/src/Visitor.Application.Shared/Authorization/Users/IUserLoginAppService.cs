using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Visitor.Authorization.Users.Dto;

namespace Visitor.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    }
}

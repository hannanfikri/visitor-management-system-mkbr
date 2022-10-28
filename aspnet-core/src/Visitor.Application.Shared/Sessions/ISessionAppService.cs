using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.Sessions.Dto;

namespace Visitor.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}

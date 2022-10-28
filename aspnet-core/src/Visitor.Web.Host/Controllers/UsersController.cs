using Abp.AspNetCore.Mvc.Authorization;
using Visitor.Authorization;
using Visitor.Storage;
using Abp.BackgroundJobs;

namespace Visitor.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}
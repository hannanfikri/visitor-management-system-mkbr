using Abp.Authorization;
using Visitor.Authorization.Roles;
using Visitor.Authorization.Users;

namespace Visitor.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}

using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Identity;
using Visitor.Authorization.Roles;
using Visitor.Authorization.Users;
using Visitor.MultiTenancy;

namespace Visitor.Authorization
{
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        public LogInManager(
            UserManager userManager, 
            IMultiTenancyConfig multiTenancyConfig, 
            IRepository<Tenant> tenantRepository, 
            IUnitOfWorkManager unitOfWorkManager, 
            ISettingManager settingManager, 
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository, 
            IUserManagementConfig userManagementConfig, 
            IIocResolver iocResolver, 
            RoleManager roleManager,
            IPasswordHasher<User> passwordHasher,
            UserClaimsPrincipalFactory claimsPrincipalFactory)
            : base(
                  userManager, 
                  multiTenancyConfig, 
                  tenantRepository, 
                  unitOfWorkManager, 
                  settingManager, 
                  userLoginAttemptRepository, 
                  userManagementConfig, 
                  iocResolver, 
                  passwordHasher,
                  roleManager,
                  claimsPrincipalFactory)
        {

        }
    }
}
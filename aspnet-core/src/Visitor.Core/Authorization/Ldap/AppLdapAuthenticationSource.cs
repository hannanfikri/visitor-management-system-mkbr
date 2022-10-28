using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Visitor.Authorization.Users;
using Visitor.MultiTenancy;

namespace Visitor.Authorization.Ldap
{
    public class AppLdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public AppLdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig)
            : base(settings, ldapModuleConfig)
        {
        }
    }
}
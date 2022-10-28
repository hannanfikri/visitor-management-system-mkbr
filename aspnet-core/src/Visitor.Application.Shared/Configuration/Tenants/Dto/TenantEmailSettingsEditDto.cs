using Abp.Auditing;
using Visitor.Configuration.Dto;

namespace Visitor.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}
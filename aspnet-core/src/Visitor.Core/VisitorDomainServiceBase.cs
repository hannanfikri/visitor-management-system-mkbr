using Abp.Domain.Services;

namespace Visitor
{
    public abstract class VisitorDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected VisitorDomainServiceBase()
        {
            LocalizationSourceName = VisitorConsts.LocalizationSourceName;
        }
    }
}

using Abp.AspNetCore.Mvc.ViewComponents;

namespace Visitor.Web.Public.Views
{
    public abstract class VisitorViewComponent : AbpViewComponent
    {
        protected VisitorViewComponent()
        {
            LocalizationSourceName = VisitorConsts.LocalizationSourceName;
        }
    }
}
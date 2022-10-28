using Abp.AspNetCore.Mvc.Views;

namespace Visitor.Web.Views
{
    public abstract class VisitorRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected VisitorRazorPage()
        {
            LocalizationSourceName = VisitorConsts.LocalizationSourceName;
        }
    }
}

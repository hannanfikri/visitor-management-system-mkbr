using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace Visitor.Web.Public.Views
{
    public abstract class VisitorRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected VisitorRazorPage()
        {
            LocalizationSourceName = VisitorConsts.LocalizationSourceName;
        }
    }
}

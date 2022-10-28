using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor
{
    [DependsOn(typeof(VisitorXamarinSharedModule))]
    public class VisitorXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorXamarinIosModule).GetAssembly());
        }
    }
}
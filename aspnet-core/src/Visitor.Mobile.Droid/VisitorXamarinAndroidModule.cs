using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor
{
    [DependsOn(typeof(VisitorXamarinSharedModule))]
    public class VisitorXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorXamarinAndroidModule).GetAssembly());
        }
    }
}
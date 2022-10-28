using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor
{
    [DependsOn(typeof(VisitorCoreSharedModule))]
    public class VisitorApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorApplicationSharedModule).GetAssembly());
        }
    }
}
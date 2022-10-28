using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor
{
    [DependsOn(typeof(VisitorClientModule), typeof(AbpAutoMapperModule))]
    public class VisitorXamarinSharedModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorXamarinSharedModule).GetAssembly());
        }
    }
}
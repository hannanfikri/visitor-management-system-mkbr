using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Visitor.Authorization;

namespace Visitor
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(VisitorApplicationSharedModule),
        typeof(VisitorCoreModule)
        )]
    public class VisitorApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorApplicationModule).GetAssembly());
        }
    }
}
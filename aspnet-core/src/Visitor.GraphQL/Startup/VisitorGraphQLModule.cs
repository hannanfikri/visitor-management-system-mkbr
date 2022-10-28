using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor.Startup
{
    [DependsOn(typeof(VisitorCoreModule))]
    public class VisitorGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Visitor.Configure;
using Visitor.Startup;
using Visitor.Test.Base;

namespace Visitor.GraphQL.Tests
{
    [DependsOn(
        typeof(VisitorGraphQLModule),
        typeof(VisitorTestBaseModule))]
    public class VisitorGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorGraphQLTestModule).GetAssembly());
        }
    }
}
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Visitor
{
    public class VisitorClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(VisitorClientModule).GetAssembly());
        }
    }
}

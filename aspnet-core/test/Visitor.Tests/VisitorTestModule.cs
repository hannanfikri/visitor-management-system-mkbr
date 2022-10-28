using Abp.Modules;
using Visitor.Test.Base;

namespace Visitor.Tests
{
    [DependsOn(typeof(VisitorTestBaseModule))]
    public class VisitorTestModule : AbpModule
    {
       
    }
}

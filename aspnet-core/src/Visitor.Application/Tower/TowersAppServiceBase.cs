using Abp.Authorization;

namespace Visitor.Tower
{
    [AbpAuthorize(new[] { "Pages.Towers" })]
    public class TowersAppServiceBase
    {
    }
}
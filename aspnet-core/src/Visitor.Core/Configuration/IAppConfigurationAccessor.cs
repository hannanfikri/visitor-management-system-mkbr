using Microsoft.Extensions.Configuration;

namespace Visitor.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}

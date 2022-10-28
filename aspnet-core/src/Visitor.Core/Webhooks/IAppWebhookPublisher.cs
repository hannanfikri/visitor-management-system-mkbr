using System.Threading.Tasks;
using Visitor.Authorization.Users;

namespace Visitor.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}

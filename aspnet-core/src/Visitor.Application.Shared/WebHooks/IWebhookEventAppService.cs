using System.Threading.Tasks;
using Abp.Webhooks;

namespace Visitor.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}

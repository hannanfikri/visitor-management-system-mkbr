using System.Threading.Tasks;
using Abp.Application.Services;

namespace Visitor.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}

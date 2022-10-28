using System.Threading.Tasks;
using Abp.Application.Services;
using Visitor.MultiTenancy.Payments.PayPal.Dto;

namespace Visitor.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}

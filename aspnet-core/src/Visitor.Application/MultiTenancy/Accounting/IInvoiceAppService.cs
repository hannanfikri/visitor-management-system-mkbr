using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Visitor.MultiTenancy.Accounting.Dto;

namespace Visitor.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}

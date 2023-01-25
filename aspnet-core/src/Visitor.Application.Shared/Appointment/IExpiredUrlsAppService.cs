using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment.ExpiredUrl;

namespace Visitor.Appointment
{
    public interface IExpiredUrlsAppService : IApplicationService
    {
        Task<PagedResultDto<GetExpiredUrlForViewDto>> GetAll(GetAllExpiredUrlsInput input);

        Task<GetExpiredUrlForEditOutput> GetExpiredUrlForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditExpiredUrlDto input);

        Task Delete(EntityDto<Guid> input);

        Task<PagedResultDto<ExpiredUrlAppointmentLookupTableDto>> GetAllAppointmentForLookupTable(GetAllForLookupTableInput input);
    }
}

using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Visitor.Departments.Dtos;

namespace Visitor.Appointment
{
    public interface IAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input);
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAllToday(GetAllAppointmentsInput input);
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAllTomorrow(GetAllAppointmentsInput input);
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAllYesterday(GetAllAppointmentsInput input);

        Task<GetAppointmentForViewDto> GetAppointmentForView(Guid id);

        Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditAppointmentDto input);

        Task Delete (EntityDto<Guid> input);
        List<GetDepartmentForViewDto> GetDepartmentName();

        GetDateTime GetDateTime(DateTime date);
    }
}

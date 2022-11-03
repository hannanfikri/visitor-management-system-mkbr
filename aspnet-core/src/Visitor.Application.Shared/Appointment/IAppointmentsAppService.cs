using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;
using Visitor.Dto;

namespace Visitor.Appointment
{
    public interface IAppointmentsAppService : IApplicationService
    {
        Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input);
        Task<GetAppointmentForViewDto> GetAppointmentForView(Guid id);

        Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditAppointmentDto input);

        Task Delete (EntityDto<Guid> input);

        //Task<FileDto> GetAppointmentsToExcel(GetAllAppointmentsForExcelInput input);
    }
}

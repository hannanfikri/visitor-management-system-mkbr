using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointment;
using Visitor.Company.Dtos;
using Visitor.Departments.Dtos;
using Visitor.Level.Dtos;
using Visitor.PurposeOfVisit.Dtos;
using Visitor.Title.Dtos;
using Visitor.Tower.Dtos;

namespace Visitor.Portal
{
    public interface IPortalAppServices : IApplicationService
    {
        Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<Guid> input);
        Task<GetAppointmentForViewDto> GetAppointmentForView(Guid id);
        Task<Guid> CreateOrEdit(CreateOrEditAppointmentDto input);
        List<GetPurposeOfVisitForViewDto> GetPurposeOfVisit();
        List<GetTitleForViewDto> GetTitle();
        List<GetTowerForViewDto> GetTower();
        List<GetLevelForViewDto> GetLevel();
        List<GetCompanyForViewDto> GetCompanyName();
        List<GetDepartmentForViewDto> GetDepartmentName();

    }
}

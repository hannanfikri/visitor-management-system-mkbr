using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Visitor.Appointment.AppointmentsAppService;
using Visitor.Authorization;
using Visitor.Dto;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Visitor.Departments;
using Visitor.Departments.Dtos;
using Visitor.Appointments;

namespace Visitor.Appointment
{
    public class AppointmentsAppService :VisitorAppServiceBase, IAppointmentsAppService
    {
        private readonly IRepository<AppointmentEnt, Guid> _appointmentRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;
        //private readonly IAppointmentsExcelExporter _appointmentsExcelExporter;

        public AppointmentsAppService(IRepository<AppointmentEnt, Guid> appointmentRepository, IRepository<Department, Guid> departmentRepository) //IAppointmentsExcelExporter appointmentsExcelExporter
        {
            _appointmentRepository = appointmentRepository;
            _departmentRepository = departmentRepository;
           // _appointmentsExcelExporter = appointmentsExcelExporter;

        }

        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input)
        {
            var getDepartments = _departmentRepository.GetAll();
            var dbListDepartments = getDepartments.ToList();
            var resultDepartment = dbListDepartments.AsEnumerable();

            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter);

            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               select new
                            {
                                Id = o.Id,
                                o.FullName,
                                o.Email,
                                o.PhoneNo,
                                o.IdentityCard,
                                o.Department,
                                o.Status
                            };

            var totalCount = await filteredAppointments.CountAsync();

            var dbListAppointment = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();

            foreach (var o in dbListAppointment)
            {
                foreach (var d in resultDepartment)
                { 
                    var res = new GetAppointmentForViewDto()
                    {
                        Appointment = new AppointmentDto
                        {
                            FullName = o.FullName,
                            Email = o.Email,
                            PhoneNo = o.PhoneNo,
                            IdentityCard = o.IdentityCard,
                            Id = o.Id,
                            DepartmentId = d.Id,
                            Department = new DepartmentDto
                            {
                                Id = d.Id,
                                DepartmentName = d.DepartmentName,
                            },
                            Status = o.Status,
                        }
                    };
                    results.Add(res);
                }
            }

            return new PagedResultDto<GetAppointmentForViewDto>(
                totalCount,
                results
            );
        }

        public async Task<GetAppointmentForViewDto> GetAppointmentForView(Guid id)
        {
            var appointment = await _appointmentRepository.GetAsync(id);

            var output = new GetAppointmentForViewDto { Appointment = ObjectMapper.Map<AppointmentDto>(appointment) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Edit)]
        public async Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<Guid> input)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAppointmentForEditOutput { Appointment = ObjectMapper.Map<CreateOrEditAppointmentDto>(appointment) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditAppointmentDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Create)]
        protected virtual async Task Create(CreateOrEditAppointmentDto input)
        {
            var appointment = ObjectMapper.Map<AppointmentEnt>(input);

            await _appointmentRepository.InsertAsync(appointment);

        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Edit)]
        protected virtual async Task Update(CreateOrEditAppointmentDto input)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, appointment);
        }

        [AbpAuthorize(AppPermissions.Pages_Appointments_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _appointmentRepository.DeleteAsync(input.Id);
        }
        public List<GetDepartmentForViewDto> GetDepartmentName()
        {
            var query = _departmentRepository.GetAll();
            var queryDepartments = from a in query
                                   select new
                                   {
                                       Id = a.Id,
                                       a.DepartmentName,
                                   };
            var dbList = queryDepartments.ToList();
            var results = new List<GetDepartmentForViewDto>();
            foreach (var d in dbList)
            {
                var res = new GetDepartmentForViewDto()
                {
                    Department = new DepartmentDto
                    {
                        Id = d.Id,
                        DepartmentName = d.DepartmentName
                    }
                };
                results.Add(res);
            }
            return new List<GetDepartmentForViewDto>(results);
        }

        /*public async Task<FileDto> GetAppointmentsToExcel(GetAllAppointmentsForExcelInput input)
        {

            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter);

            var query = (from o in filteredAppointments
                         select new GetAppointmentForViewDto()
                         {
                             Appointment = new AppointmentDto
                             {
                                 FullName = o.FullName,
                                 Id = o.Id
                             }
                         });

            var appointmentListDtos = await query.ToListAsync();

            //return _appointmentsExcelExporter.ExportToFile(appointmentListDtos);
        }*/
    }
    
}

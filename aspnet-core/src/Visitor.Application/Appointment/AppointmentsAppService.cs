using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor.Authorization;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Visitor.PurposeOfVisit.Dtos;
using Visitor.PurposeOfVisit;
using Visitor.Title;
using Visitor.Tower;
using Visitor.Company;
using Visitor.Title.Dtos;
using Visitor.Tower.Dtos;
using Visitor.Level;
using Visitor.Level.Dtos;
using Visitor.Company.Dtos;
using Visitor.Departments;
using Visitor.Departments.Dtos;
using Visitor.Appointments;
using Visitor.Storage;
using Visitor.Authorization.Users.Profile;
using Abp.Collections.Extensions;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using UserIdentifier = Abp.UserIdentifier;
using Visitor.Migrations;
using Microsoft.AspNetCore.Mvc;
using Abp.AspNetZeroCore.Net;
using ImageDrawing = System.Drawing.Image;
using System.Reflection.Metadata;
using Visitor.Common;
using System.Threading;
using Stripe;
using Twilio.Types;
using NPOI.SS.Formula.Functions;
using Abp.UI;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
/*using StatusEnum = Visitor.Appointments.StatusEnum;*/

namespace Visitor.Appointment
{
    public class AppointmentsAppService : VisitorAppServiceBase, IAppointmentsAppService
    {
        private readonly IRepository<AppointmentEnt, Guid> _appointmentRepository;
        private readonly IRepository<PurposeOfVisitEnt, Guid> _purposeOfVisitRepository;
        private readonly IRepository<TitleEnt, Guid> _titleRepository;
        private readonly IRepository<TowerEnt, Guid> _towerRepository;
        private readonly IRepository<LevelEnt, Guid> _levelRepository;
        private readonly IRepository<CompanyEnt, Guid> _companyRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;
        private readonly ITempFileCacheManager _tempFileCacheManager;
        private readonly IBinaryObjectManager _binaryObjectManager;
        private readonly ProfileImageServiceFactory _profileImageServiceFactory;
        private const int MaxPictureBytes = 5242880; //5MB
        

        public AppointmentsAppService

            (IRepository<AppointmentEnt, Guid> appointmentRepository,
            IRepository<PurposeOfVisitEnt, Guid> purposeOfVisitRepository,
            IRepository<TitleEnt, Guid> titleRepository,
            IRepository<TowerEnt, Guid> towerRepository,
            IRepository<LevelEnt, Guid> levelRepository,
            IRepository<CompanyEnt, Guid> companyRepository,
            IRepository<Department, Guid> departmentRepository,
            ITempFileCacheManager tempFileCacheManager,
            IBinaryObjectManager binaryObjectManager,
            ProfileImageServiceFactory profileImageServiceFactory
            
            )
        {
            _appointmentRepository = appointmentRepository;
            _purposeOfVisitRepository = purposeOfVisitRepository;
            _titleRepository = titleRepository;
            _towerRepository = towerRepository;
            _levelRepository = levelRepository;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;
            _tempFileCacheManager = tempFileCacheManager;
            _binaryObjectManager = binaryObjectManager;
            _profileImageServiceFactory = profileImageServiceFactory;
            

        }
        protected DateTime GetToday()
        {
            return DateTime.Now.Date;
            /*DateOnly testTimeOnly = DateOnly.FromDateTime(now);
            return testTimeOnly; */
        }
        protected DateTime GetTomorrow()
        {
            return DateTime.Now.AddDays(1).Date;
        }
        protected DateTime GetYesterday()
        {
            return DateTime.Now.AddDays(-1).Date;
        }
        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAll(GetAllAppointmentsInput input)
        {
            DateTime minA = DateTime.Now;
            DateTime maxA = DateTime.Now;
            DateTime minR = DateTime.Now;
            DateTime maxR = DateTime.Now;

            if (input.MinAppDateTimeFilter != null)
            {
                 minA = (DateTime)input.MinAppDateTimeFilter;
            }
            if (input.MaxAppDateTimeFilter != null)
            {
                 maxA = (DateTime)input.MaxAppDateTimeFilter;
            }
            if (input.MinRegDateTimeFilter != null)
            {
                minR = (DateTime)input.MinRegDateTimeFilter;
            }
            if (input.MaxRegDateTimeFilter != null)
            {
                maxR = (DateTime)input.MaxRegDateTimeFilter;
            }


            var todaysDate = DateTime.Today;

            var statusEnumFilter = input.StatusFilter.HasValue
                        ? (StatusType)input.StatusFilter
                        : default;

            var serviceType = 0;

            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityCardFilter), e => e.IdentityCard == input.IdentityCardFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo == input.PhoneNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title == input.TitleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OfficerToMeetFilter), e => e.OfficerToMeet == input.OfficerToMeetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeOfVisitFilter), e => e.PurposeOfVisit == input.PurposeOfVisitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department == input.DepartmentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TowerFilter), e => e.Tower == input.TowerFilter)

                        .WhereIf(input.MinAppDateTimeFilter != null, e => e.AppDateTime.Date >= minA.Date)
                        .WhereIf(input.MaxAppDateTimeFilter != null, e => e.AppDateTime.Date <= maxA.Date)
                        .WhereIf(input.MinRegDateTimeFilter != null, e => e.CreationTime.Date >= minR.Date)
                        .WhereIf(input.MaxRegDateTimeFilter != null, e => e.CreationTime.Date <= maxR.Date)

                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusEnumFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LevelFilter), e => e.Level == input.LevelFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppRefNoFilter), e => e.AppRefNo == input.AppRefNoFilter);
                        //.WhereIf(input.AppDateTimeFilter != null, e => e.AppDateTime == input.AppDateTimeFilter );



            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               select new
                               {
                                   o.Id,
                                   o.FullName,
                                   o.Email,
                                   o.PhoneNo,
                                   o.IdentityCard,
                                   o.PurposeOfVisit,
                                   o.CompanyName,
                                   o.OfficerToMeet,
                                   o.Department,
                                   o.Tower,
                                   o.Level,
                                   o.AppDateTime,
                                   o.CreationTime,
                                   o.Status,
                                   o.Title,
                                   o.ImageId,
                                   o.AppRefNo
                               };

            var totalCount = await filteredAppointments.CountAsync();

            var dbList = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();
            var image = new UploadPictureOutput();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentForViewDto()
                {
                    Appointment = new AppointmentDto
                    {
                        Id = o.Id,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        IdentityCard = o.IdentityCard,
                        PurposeOfVisit = o.PurposeOfVisit,
                        CompanyName = o.CompanyName,
                        OfficerToMeet = o.OfficerToMeet,
                        Department = o.Department,
                        Tower = o.Tower,
                        Level = o.Level,
                        AppDateTime = o.AppDateTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        RegDateTime = o.CreationTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        Status = o.Status,
                        Title = o.Title,
                        ImageId = o.ImageId,
                        //ImageId = image.Id,,
                        AppRefNo = o.AppRefNo
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAppointmentForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAllToday(GetAllAppointmentsInput input)
        {

            DateTime minA = DateTime.Now;
            DateTime maxA = DateTime.Now;
            DateTime minR = DateTime.Now;
            DateTime maxR = DateTime.Now;

            if (input.MinAppDateTimeFilter != null)
            {
                minA = (DateTime)input.MinAppDateTimeFilter;
            }
            if (input.MaxAppDateTimeFilter != null)
            {
                maxA = (DateTime)input.MaxAppDateTimeFilter;
            }
            if (input.MinRegDateTimeFilter != null)
            {
                minR = (DateTime)input.MinRegDateTimeFilter;
            }
            if (input.MaxRegDateTimeFilter != null)
            {
                maxR = (DateTime)input.MaxRegDateTimeFilter;
            }

            DateTime d1 = GetToday();
            var todaysDate = DateTime.Today;
            

            var statusEnumFilter = input.StatusFilter.HasValue
                        ? (StatusType)input.StatusFilter
                        : default;

            var serviceType = 0;


            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter)
                        .Where(e => e.AppDateTime.Date == d1)

                        .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityCardFilter), e => e.IdentityCard == input.IdentityCardFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo == input.PhoneNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title == input.TitleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OfficerToMeetFilter), e => e.OfficerToMeet == input.OfficerToMeetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeOfVisitFilter), e => e.PurposeOfVisit == input.PurposeOfVisitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department == input.DepartmentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TowerFilter), e => e.Tower == input.TowerFilter)

                        .WhereIf(input.MinAppDateTimeFilter != null, e => e.AppDateTime.Date >= minA.Date)
                        .WhereIf(input.MaxAppDateTimeFilter != null, e => e.AppDateTime.Date <= maxA.Date)
                        .WhereIf(input.MinRegDateTimeFilter != null, e => e.CreationTime.Date >= minR.Date)
                        .WhereIf(input.MaxRegDateTimeFilter != null, e => e.CreationTime.Date <= maxR.Date)


                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusEnumFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LevelFilter), e => e.Level == input.LevelFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppRefNoFilter), e => e.AppRefNo == input.AppRefNoFilter);

            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               select new
                               {
                                   o.Id,
                                   o.FullName,
                                   o.Email,
                                   o.PhoneNo,
                                   o.IdentityCard,
                                   o.PurposeOfVisit,
                                   o.CompanyName,
                                   o.OfficerToMeet,
                                   o.Department,
                                   o.Tower,
                                   o.Level,
                                   o.AppDateTime,
                                   o.CreationTime,
                                   o.Status,
                                   o.Title,
                                   o.AppRefNo
                               };

            var totalCount = await filteredAppointments.CountAsync();

            var dbList = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();
            var image = new UploadPictureOutput();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentForViewDto()
                {
                    Appointment = new AppointmentDto
                    {
                        Id = o.Id,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        IdentityCard = o.IdentityCard,
                        PurposeOfVisit = o.PurposeOfVisit,
                        CompanyName = o.CompanyName,
                        OfficerToMeet = o.OfficerToMeet,
                        Department = o.Department,
                        Tower = o.Tower,
                        Level = o.Level,
                        AppDateTime = o.AppDateTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        RegDateTime = o.CreationTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        Status = o.Status,
                        Title = o.Title,
                        AppRefNo = o.AppRefNo
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAppointmentForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAllTomorrow(GetAllAppointmentsInput input)
        {
            DateTime minA = DateTime.Now;
            DateTime maxA = DateTime.Now;
            DateTime minR = DateTime.Now;
            DateTime maxR = DateTime.Now;

            if (input.MinAppDateTimeFilter != null)
            {
                minA = (DateTime)input.MinAppDateTimeFilter;
            }
            if (input.MaxAppDateTimeFilter != null)
            {
                maxA = (DateTime)input.MaxAppDateTimeFilter;
            }
            if (input.MinRegDateTimeFilter != null)
            {
                minR = (DateTime)input.MinRegDateTimeFilter;
            }
            if (input.MaxRegDateTimeFilter != null)
            {
                maxR = (DateTime)input.MaxRegDateTimeFilter;
            }

            DateTime d2 = GetTomorrow();

            var todaysDate = DateTime.Today;

            var statusEnumFilter = input.StatusFilter.HasValue
                        ? (StatusType)input.StatusFilter
                        : default;

            var serviceType = 0;

            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter)
                        .Where(e => e.AppDateTime.Date == d2)

                        .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityCardFilter), e => e.IdentityCard == input.IdentityCardFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo == input.PhoneNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title == input.TitleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OfficerToMeetFilter), e => e.OfficerToMeet == input.OfficerToMeetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeOfVisitFilter), e => e.PurposeOfVisit == input.PurposeOfVisitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department == input.DepartmentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TowerFilter), e => e.Tower == input.TowerFilter)

                        .WhereIf(input.MinAppDateTimeFilter != null, e => e.AppDateTime.Date >= minA.Date)
                        .WhereIf(input.MaxAppDateTimeFilter != null, e => e.AppDateTime.Date <= maxA.Date)
                        .WhereIf(input.MinRegDateTimeFilter != null, e => e.CreationTime.Date >= minR.Date)
                        .WhereIf(input.MaxRegDateTimeFilter != null, e => e.CreationTime.Date <= maxR.Date)

                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusEnumFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LevelFilter), e => e.Level == input.LevelFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppRefNoFilter), e => e.AppRefNo == input.AppRefNoFilter);

            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               select new
                               {
                                   o.Id,
                                   o.FullName,
                                   o.Email,
                                   o.PhoneNo,
                                   o.IdentityCard,
                                   o.PurposeOfVisit,
                                   o.CompanyName,
                                   o.OfficerToMeet,
                                   o.Department,
                                   o.Tower,
                                   o.Level,
                                   o.AppDateTime,
                                   o.CreationTime,
                                   o.Status,
                                   o.Title,
                                   o.AppRefNo
                               };

            var totalCount = await filteredAppointments.CountAsync();

            var dbList = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentForViewDto()
                {
                    Appointment = new AppointmentDto
                    {
                        Id = o.Id,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        IdentityCard = o.IdentityCard,
                        PurposeOfVisit = o.PurposeOfVisit,
                        CompanyName = o.CompanyName,
                        OfficerToMeet = o.OfficerToMeet,
                        Department = o.Department,
                        Tower = o.Tower,
                        Level = o.Level,
                        AppDateTime = o.AppDateTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        RegDateTime = o.CreationTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        Status = o.Status,
                        Title = o.Title,
                        AppRefNo = o.AppRefNo
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAppointmentForViewDto>(
                totalCount,
                results
            );

        }
        public async Task<PagedResultDto<GetAppointmentForViewDto>> GetAllYesterday(GetAllAppointmentsInput input)
        {
            DateTime minA = DateTime.Now;
            DateTime maxA = DateTime.Now;
            DateTime minR = DateTime.Now;
            DateTime maxR = DateTime.Now;

            if (input.MinAppDateTimeFilter != null)
            {
                minA = (DateTime)input.MinAppDateTimeFilter;
            }
            if (input.MaxAppDateTimeFilter != null)
            {
                maxA = (DateTime)input.MaxAppDateTimeFilter;
            }
            if (input.MinRegDateTimeFilter != null)
            {
                minR = (DateTime)input.MinRegDateTimeFilter;
            }
            if (input.MaxRegDateTimeFilter != null)
            {
                maxR = (DateTime)input.MaxRegDateTimeFilter;
            }

            DateTime d3 = GetYesterday();

            var todaysDate = DateTime.Today;

            var statusEnumFilter = input.StatusFilter.HasValue
                        ? (StatusType)input.StatusFilter
                        : default;

            var serviceType = 0;

            var filteredAppointments = _appointmentRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter)
                        .Where(e => e.AppDateTime.Date == d3)

                        .WhereIf(!string.IsNullOrWhiteSpace(input.IdentityCardFilter), e => e.IdentityCard == input.IdentityCardFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PhoneNoFilter), e => e.PhoneNo == input.PhoneNoFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EmailFilter), e => e.Email == input.EmailFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TitleFilter), e => e.Title == input.TitleFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.OfficerToMeetFilter), e => e.OfficerToMeet == input.OfficerToMeetFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurposeOfVisitFilter), e => e.PurposeOfVisit == input.PurposeOfVisitFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DepartmentFilter), e => e.Department == input.DepartmentFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TowerFilter), e => e.Tower == input.TowerFilter)

                        .WhereIf(input.MinAppDateTimeFilter != null, e => e.AppDateTime.Date >= minA.Date)
                        .WhereIf(input.MaxAppDateTimeFilter != null, e => e.AppDateTime.Date <= maxA.Date)
                        .WhereIf(input.MinRegDateTimeFilter != null, e => e.CreationTime.Date >= minR.Date)
                        .WhereIf(input.MaxRegDateTimeFilter != null, e => e.CreationTime.Date <= maxR.Date)

                        .WhereIf(input.StatusFilter.HasValue && input.StatusFilter > -1, e => e.Status == statusEnumFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LevelFilter), e => e.Level == input.LevelFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppRefNoFilter), e => e.AppRefNo == input.AppRefNoFilter);

            var pagedAndFilteredAppointments = filteredAppointments
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var appointments = from o in pagedAndFilteredAppointments
                               select new
                               {
                                   o.Id,
                                   o.FullName,
                                   o.Email,
                                   o.PhoneNo,
                                   o.IdentityCard,
                                   o.PurposeOfVisit,
                                   o.CompanyName,
                                   o.OfficerToMeet,
                                   o.Department,
                                   o.Tower,
                                   o.Level,
                                   o.AppDateTime,
                                   o.CreationTime,
                                   o.Status,
                                   o.Title,
                                   o.AppRefNo
                               };

            var totalCount = await filteredAppointments.CountAsync();

            var dbList = await appointments.ToListAsync();
            var results = new List<GetAppointmentForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAppointmentForViewDto()
                {
                    Appointment = new AppointmentDto
                    {
                        Id = o.Id,
                        FullName = o.FullName,
                        Email = o.Email,
                        PhoneNo = o.PhoneNo,
                        IdentityCard = o.IdentityCard,
                        PurposeOfVisit = o.PurposeOfVisit,
                        CompanyName = o.CompanyName,
                        OfficerToMeet = o.OfficerToMeet,
                        Department = o.Department,
                        Tower = o.Tower,
                        Level = o.Level,
                        AppDateTime = o.AppDateTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        RegDateTime = o.CreationTime.ToString("dddd, dd MMMM yyyy hh:mm tt"),
                        Status = o.Status,
                        Title = o.Title,
                        AppRefNo = o.AppRefNo
                    }
                };

                results.Add(res);
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

            var pn = input.PhoneNo;
            var ic = input.IdentityCard;
            //input.MobileNumber = "+6" + input.MobileNumber;
            var rand = new Random();
            int num = rand.Next(1000);
            var date = input.AppDateTime.Date.ToString("yyMMdd");
            var lang = Thread.CurrentThread.CurrentCulture;
            input.Status = 0;
            string lastFourDigitsPN = pn.Substring(pn.Length - 4, 4);
            string lastFourDigitsIC = ic.Substring(pn.Length - 2, 4);
            //input.AppointmentDate = input.AppointmentDate.ToShortDateString();
            input.AppRefNo = "AR" + date + lastFourDigitsPN + lastFourDigitsIC + num;
            /*var appointment = ObjectMapper.Map<AppointmentEnt>(input);*/

            


            var checker = true;
            while (checker)
            {
                var bookhCheck = _appointmentRepository.FirstOrDefault(e => e.AppRefNo == input.AppRefNo);
                if (bookhCheck?.Id != null)
                {
                    /*string PhoneNo = "";*/

                    //if duplicate booking ref no
                    checker = true;
                    num = rand.Next(1000);
                    /*string lastFourDigits = pn.Substring(pn.Length - 4, 4);*/
                    input.AppRefNo = "AR" + date + lastFourDigitsPN + lastFourDigitsIC + num;

                }
                else
                {
                    checker = false;
                    break;
                }
            };

            var appointment = ObjectMapper.Map<AppointmentEnt>(input);
            await _appointmentRepository.InsertAsync(appointment);
            /*await UpdateSlot(input.BranchSlotSettingId, input.AppointmentSlot);*/
            /*var bookingDetail = await _bookingRepository.InsertAsync(booking);
            var branch = await _lookup_branchRepository.GetAsync(input.BranchId.Value);*/

            /*if (input.Email != null)
            {
                try
                {
                    await _portalEmailer.SendEmailDetailBookingAsync(ObjectMapper.Map<Booking>(bookingDetail), branch, service);
                }
                catch
                {
                    //To do
                }
            }

            try
            {
                await SendSms(input);
            }
            catch
            {

            }*/

            /*if (input.Email != null)
            {
                try
                {
                    await _portalEmailer.SendEmailDetailBookingAsync(ObjectMapper.Map<Booking>(bookingDetail), branch, service);
                }
                catch
                {
                    //To do
                }
            }

            try
            {
                await SendSms(input);
            }
            catch
            {

            }*/

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

        public List<GetPurposeOfVisitForViewDto> GetPurposeOfVisit()
        {
            var query = _purposeOfVisitRepository.GetAll();
            var queryPOV = from a in query
                           select new
                           {
                               Id = a.Id,
                               a.PurposeOfVisitApp
                           };
            var dblist = queryPOV.ToList();
            var results = new List<GetPurposeOfVisitForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetPurposeOfVisitForViewDto()
                {
                    PurposeOfVisit = new PurposeOfVisitDto
                    {
                        Id = db.Id,
                        PurposeOfVisitApp = db.PurposeOfVisitApp,
                    }
                };
                results.Add(res);
            }

            return new List<GetPurposeOfVisitForViewDto>(results);
        }

        public List<GetTitleForViewDto> GetTitle()
        {
            var query = _titleRepository.GetAll();
            var qTitle = from a in query
                         select new
                         {
                             Id = a.Id,
                             a.VisitorTitle
                         };
            var dblist = qTitle.ToList();
            var results = new List<GetTitleForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetTitleForViewDto()
                {
                    Title = new TitleDto
                    {
                        Id = db.Id,
                        VisitorTitle = db.VisitorTitle,
                    }
                };
                results.Add(res);
            }

            return new List<GetTitleForViewDto>(results);
        }
        public List<GetTowerForViewDto> GetTower()
        {
            var query = _towerRepository.GetAll();
            var qTower = from a in query
                         select new
                         {
                             Id = a.Id,
                             a.TowerBankRakyat
                         };
            var dblist = qTower.ToList();
            var results = new List<GetTowerForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetTowerForViewDto()
                {
                    Tower = new TowerDto
                    {
                        Id = db.Id,
                        TowerBankRakyat = db.TowerBankRakyat,
                    }
                };
                results.Add(res);
            }

            return new List<GetTowerForViewDto>(results);
        }
        public List<GetLevelForViewDto> GetLevel()
        {
            var query = _levelRepository.GetAll();
            var qLevel = from a in query
                         select new
                         {
                             Id = a.Id,
                             a.LevelBankRakyat
                         };
            var dblist = qLevel.ToList();
            var results = new List<GetLevelForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetLevelForViewDto()
                {
                    Level = new LevelDto
                    {
                        Id = db.Id,
                        LevelBankRakyat = db.LevelBankRakyat
                    }
                };
                results.Add(res);
            }
            return new List<GetLevelForViewDto>(results);
        }
        public List<GetCompanyForViewDto> GetCompanyName()
        {
            var query = _companyRepository.GetAll();
            var qCompany = from a in query
                           select new
                           {
                               Id = a.Id,
                               a.CompanyName,
                               a.OfficePhoneNumber,
                               a.CompanyEmail,
                               a.CompanyAddress
                           };
            var dblist = qCompany.ToList();
            var results = new List<GetCompanyForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetCompanyForViewDto()
                {
                    Company = new CompanyDto
                    {
                        Id = db.Id,
                        CompanyName = db.CompanyName,
                        OfficePhoneNumber = db.OfficePhoneNumber,
                        CompanyEmail = db.CompanyEmail,
                        CompanyAddress = db.CompanyAddress
                    }
                };
                results.Add(res);
            }

            return new List<GetCompanyForViewDto>(results);
        }
        public List<GetDepartmentForViewDto> GetDepartmentName()
        {
            var query = _departmentRepository.GetAll();
            var qDepartment = from a in query
                              select new
                              {
                                  Id = a.Id,
                                  a.DepartmentName
                              };
            var dblist = qDepartment.ToList();
            var results = new List<GetDepartmentForViewDto>();
            foreach (var db in dblist)
            {
                var res = new GetDepartmentForViewDto()
                {
                    Department = new DepartmentDto
                    {
                        Id = db.Id,
                        DepartmentName = db.DepartmentName,
                    }
                };
                results.Add(res);
            }
            return new List<GetDepartmentForViewDto>(results);
        }


        // Upload image services (referring to profile services)
        public async Task<Guid> UpdatePictureForAppointment(UpdatePictureInput input)
        {

            byte[] byteArray;
            var imageBytes = _tempFileCacheManager.GetFile(input.FileToken);

            if (imageBytes == null)
            {
                throw new UserFriendlyException("There is no such image file with the token: " + input.FileToken);
            }

            using(var image = Image.Load(imageBytes, out IImageFormat format))
            {
                var width = (input.Width == 0 || input.Width > image.Width) ? image.Width : input.Width;
                var height = (input.Height == 0 || input.Height > image.Height) ? image.Height : input.Height;

                var bmCrop = image.Clone(i =>
                    i.Crop(new Rectangle(input.X, input.Y, width, height))
                );

                await using (var stream = new MemoryStream())
                {
                    await bmCrop.SaveAsync(stream, format);
                    byteArray = stream.ToArray();
                }
            }

            if (byteArray.Length > MaxPictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit",
                    AppConsts.ResizedMaxProfilePictureBytesUserFriendlyValue));
            }
            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray, $"Appointment picture at {DateTime.UtcNow}");
            await _binaryObjectManager.SaveAsync(storedFile);

            /*var app = _appointmentRepository.GetAsync(appId).Result;

            app.ImageId = storedFile.Id;*/
            var picId = storedFile.Id;
            return picId;
        }

        public async Task<byte[]> GetPictureByIdOrNull(Guid imageId)
        {
            var file = await _binaryObjectManager.GetOrNullAsync(imageId);
            if ( file == null)
            {
                return null;
            }
            return file.Bytes;
        }

        public async Task<string> GetFilePictureByIdOrNull(Guid imageId)
        {
            var output = await _binaryObjectManager.GetOrNullAsync(imageId);
            //var memoryStream = new MemoryStream(output.Bytes);
            //return ImageDrawing.FromStream(memoryStream);
            //byte[] bytes = output.Bytes;
            //return File(bytes);
            //Blob blob = new Blob(memoryStream.GetBuffer());

            using (MemoryStream ms = new MemoryStream(output.Bytes))
            {
                var image = ImageDrawing.FromStream(ms);
                string base64 = Convert.ToBase64String(output.Bytes);
                return base64;
            }
            
        }

        public async Task<GetPictureOutput> GetPictureByAppointment(Guid appId)
        {
            var output = await _appointmentRepository.GetAsync(appId);

            return new GetPictureOutput(output.ImageId);
        }
    }
    
}

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
using Visitor.Storage;
using Visitor.Authorization.Users.Profile;
using Abp.Collections.Extensions;
using Visitor;
using Visitor.Appointment;
using System.Threading;

namespace CMS.Portal
{
    public class PortalsAppService : VisitorAppServiceBase
    {
        private readonly IRepository<AppointmentEnt, Guid> _appointmentRepository;
        private readonly IRepository<PurposeOfVisitEnt, Guid> _purposeOfVisitRepository;
        private readonly IRepository<TitleEnt, Guid> _titleRepository;
        private readonly IRepository<TowerEnt, Guid> _towerRepository;
        private readonly IRepository<LevelEnt, Guid> _levelRepository;
        private readonly IRepository<CompanyEnt, Guid> _companyRepository;
        private readonly IRepository<Department, Guid> _departmentRepository;

        public PortalsAppService

            (IRepository<AppointmentEnt, Guid> appointmentRepository,
            IRepository<PurposeOfVisitEnt, Guid> purposeOfVisitRepository,
            IRepository<TitleEnt, Guid> titleRepository,
            IRepository<TowerEnt, Guid> towerRepository,
            IRepository<LevelEnt, Guid> levelRepository,
            IRepository<CompanyEnt, Guid> companyRepository,
            IRepository<Department, Guid> departmentRepository
            )
        {
            _appointmentRepository = appointmentRepository;
            _purposeOfVisitRepository = purposeOfVisitRepository;
            _titleRepository = titleRepository;
            _towerRepository = towerRepository;
            _levelRepository = levelRepository;
            _companyRepository = companyRepository;
            _departmentRepository = departmentRepository;

        }
        public async Task<GetAppointmentForEditOutput> GetAppointmentForEdit(EntityDto<Guid> input)
        {
            var appointment = await _appointmentRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAppointmentForEditOutput { Appointment = ObjectMapper.Map<CreateOrEditAppointmentDto>(appointment) };

            return output;
        }
        public async Task<GetAppointmentForViewDto> GetAppointmentForView(Guid id)
        {
            var appointment = await _appointmentRepository.GetAsync(id);

            var output = new GetAppointmentForViewDto { Appointment = ObjectMapper.Map<AppointmentDto>(appointment) };

            return output;
        }
        public async Task CreateOrEdit(CreateOrEditAppointmentDto input)
        {
                await Create(input);
        }

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

    }
}

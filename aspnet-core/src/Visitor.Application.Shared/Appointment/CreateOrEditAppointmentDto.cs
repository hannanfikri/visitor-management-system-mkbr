
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using Visitor.Departments.Dtos;
using System.ComponentModel.DataAnnotations.Schema;
using Visitor.Appointments;

namespace Visitor.Appointment
{
    public class CreateOrEditAppointmentDto : EntityDto<Guid?>
    {
        //[Required]
        //[StringLength(AppointmentEnt.MaxIdentityCardLength)]
        public virtual string IdentityCard { get; set; }

        //[Required]
        //[StringLength(AppointmentEnt.MaxFullNameLength)]
        public virtual string FullName { get; set; }

        //[Required]
        //[StringLength(Appointments.MaxPhoneNoLength)]
        public virtual string PhoneNo { get; set; }

        //[Required]
        //[StringLength(Appointments.MaxOfficerToMeetLength)]
        public virtual string OfficerToMeet { get; set; }


        //[StringLength(Appointments.MaxPurposeOfVisitLength)]
        public virtual string PurposeOfVisit { get; set; }

        //[Required]
        //[StringLength(Appointments.MaxEmailLength)]
        public virtual string Email { get; set; }

        public virtual string Title { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Department { get; set; }
        public virtual Nullable<int> Tower { get; set; }
        public virtual Nullable<int> Level { get; set; }

        public virtual DateTime AppDateTime { get; set; }

        public virtual byte[] FaceVerify { get; set; }

        public virtual DateTime RegDateTime { get; set; }
        public virtual StatusType Status { get; set; }

        //[StringLength(AbpAppointmentBase.MaxConnectionStringLength)]
        //public string ConnectionString { get; set; }

        //public bool IsActive { get; set; }
    }
}

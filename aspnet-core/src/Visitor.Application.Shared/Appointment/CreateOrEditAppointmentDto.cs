
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using Abp.Domain.Entities.Auditing;

namespace Visitor.Appointment
{
    public class CreateOrEditAppointmentDto : FullAuditedEntityDto<Guid?> ,IHasCreationTime
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
        [Required]
        public virtual DateTime AppDateTime { get; set; }
        [Required]
        public virtual byte[] FaceVerify { get; set; }
        public virtual string Status { get; set; }
    }
}

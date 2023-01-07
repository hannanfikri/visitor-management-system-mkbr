
using Abp.Application.Services.Dto;
using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.Application.Services.Dto;
using Abp.MultiTenancy;
using Abp.Domain.Entities.Auditing;
using Visitor.Departments.Dtos;
using System.ComponentModel.DataAnnotations.Schema;
using Visitor.Appointments;
using Visitor.Common;
using Abp.Runtime.Validation;
using Abp.Extensions;

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
        public virtual string Tower { get; set; }
        public virtual string Level { get; set; }
        [Required]
        public virtual DateTime AppDateTime { get; set; }

        [NotMapped]
        public virtual IHasCreationTime RegDateTime { get; set; }
        public virtual StatusType Status { get; set; }
        public virtual string ImageId { get; set; }
        public virtual string PassNumber { get; set; }

        public virtual string AppRefNo { get; set; }
        public string FileToken { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public void AddValidationErrors(CustomValidationContext context)
        {
            if (FileToken.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(FileToken));
            }
        }
    }
}

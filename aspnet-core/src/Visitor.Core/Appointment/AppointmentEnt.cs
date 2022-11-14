using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.PurposeOfVisit;

namespace Visitor.Appointment
{
    public class AppointmentEnt :  FullAuditedEntity<Guid>,IHasCreationTime//derive from class entity
    {
        public const int MaxIdentityCardLength = 12;
        public const int MaxFullNameLength = 255;
        public const int MaxPhoneNoLength = 11;
        public const int MaxEmailLength = 50;
        public const int MaxOfficerToMeetLength = 255;
        public const int MaxPurposeOfVisitLength = 250;//text

        public string IdentityCard { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string OfficerToMeet { get; set; }
        public string PurposeOfVisit { get; set; }
        public string Department { get; set; }
        public Nullable<int> Tower { get; set; }
        public Nullable<int> Level { get; set; }
        public DateTime AppDateTime { get; set; }
        public byte[] FaceVerify { get; set; }//match datatype for file

        [NotMapped]
        public IHasCreationTime RegDateTime { get; set; }
        public string Status { get; set; }

       // [ForeignKey(<PurposeOfVisitEnt> )]


        private AppointmentEnt() { }//empty constructor

        public AppointmentEnt(string identityCard, string fullName, string phoneNo, string email, string title, string companyName, string officerToMeet, string purposeOfVisit, string department, Nullable<int> tower, Nullable<int> level, DateTime appDateTime, byte[] faceVerify, IHasCreationTime regDateTime, string status)
        {
            IdentityCard = identityCard;
            FullName = fullName;
            PhoneNo = phoneNo;
            Email = email;
            Title = title;
            CompanyName = companyName;
            OfficerToMeet = officerToMeet;
            PurposeOfVisit = purposeOfVisit;
            Department = department;
            Tower = tower;
            Level = level;
            AppDateTime = appDateTime;
            FaceVerify = faceVerify;
            RegDateTime = regDateTime;
            Status = status;// constructor
        }
    }
}

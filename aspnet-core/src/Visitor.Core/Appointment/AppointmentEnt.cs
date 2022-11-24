using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Appointments;
using Visitor.Departments;

namespace Visitor.Appointment
{
    public class AppointmentEnt : Entity<Guid>//derive from class entity
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
        public string? PurposeOfVisit { get; set; }
        [ForeignKey("Department")]
        public Guid DepartmentId { get; set; }
        public string Department { get; set; }
        public Nullable<int> Tower { get; set; }
        public Nullable<int> Level { get; set; }
        public DateTime AppDateTime { get; set; }
        public byte[] FaceVerify { get; set; }//match datatype for file
        public DateTime RegDateTime { get; set; }
        public StatusType Status { get; set; }


        private AppointmentEnt() { }//empty constructor

        public AppointmentEnt(string identityCard, string fullName, string phoneNo, string email, string title, string companyName, string officerToMeet, string purposeOfVisit, Guid departmentId, string department, int? tower, int? level, DateTime appDateTime, byte[] faceVerify, DateTime regDateTime, StatusType status)
        {
            IdentityCard = identityCard;
            FullName = fullName;
            PhoneNo = phoneNo;
            Email = email;
            Title = title;
            CompanyName = companyName;
            OfficerToMeet = officerToMeet;
            PurposeOfVisit = purposeOfVisit;
            DepartmentId = departmentId;
            Department = department;
            Tower = tower;
            Level = level;
            AppDateTime = appDateTime;
            FaceVerify = faceVerify;
            RegDateTime = regDateTime;
            Status = status = StatusType.Registered;
        }
    }
}

using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Visitor.Appointment
{
    public class AppointmentDto : EntityDto<Guid>
    {
        //[AutoMapFrom(typeof(Appointments))]
    
        
            public string IdentityCard { get; set; }
            public string FullName { get; set; }
            public string PhoneNo { get; set; }
            public string Email { get; set; }
            public string Title { get; set; }
            public string CompanyName { get; set; }
            public string OfficerToMeet { get; set; }
        [ForeignKey("PurposeOfVisitEnt")]
        public Guid PovId { get; set; }
        public string PurposeOfVisit { get; set; }
            public string Department { get; set; }
            public int? Tower { get; set; }
            public int? Level { get; set; }
            public DateTime AppDateTime { get; set; }
            public byte[] FaceVerify { get; set; }
            public IHasCreationTime RegDateTime { get; set; }
            public string Status { get; set; }

        [ForeignKey("TitleEnt")]
        public Guid TitleId { get; set; }

        [ForeignKey("LevelEnt")]
        public Guid LevelId { get; set; }

        [ForeignKey("TowerEnt")]
        public Guid TowerId { get; set; }
    }
}

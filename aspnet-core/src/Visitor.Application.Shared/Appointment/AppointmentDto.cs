﻿using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Visitor.Departments.Dtos;

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
            public string PurposeOfVisit { get; set; }
            [ForeignKey("Department")]
            public Guid DepartmentId { get; set; }
            public DepartmentDto Department { get; set; }
            public int Tower { get; set; }
            public int Level { get; set; }
            public DateTime AppDateTime { get; set; }
            public byte[] FaceVerify { get; set; }
            public DateTime RegDateTime { get; set; }
            public string Status { get; set; }

        
    }
}

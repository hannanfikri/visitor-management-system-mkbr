using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visitor.Appointment.ExpiredUrl
{
    public class CreateOrEditExpiredUrlDto : EntityDto<Guid?>
    {
        [Required]
        public DateTime UrlCreateDate { get; set; }

        [Required]
        public DateTime UrlExpiredDate { get; set; }

        [Required]
        public string Item { get; set; }

        public string Status { get; set; }

        public Guid? AppointmentId { get; set; }
    }
}

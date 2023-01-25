using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Appointment.ExpiredUrl
{
    [Table("ExpiredUrls")]
    public class ExpiredUrlEnt:FullAuditedEntity<Guid>
    {
        [Required]
        public virtual DateTime UrlCreateDate { get; set; }

        [Required]
        public virtual DateTime UrlExpiredDate { get; set; }

        [Required]
        public virtual string Item { get; set; }

        [Required]
        public virtual string Status { get; set; }

        public virtual Guid? AppointmentId { get; set; }

        [ForeignKey("AppointmentId")]
        public AppointmentEnt AppointmentFk { get; set; }
    }
}

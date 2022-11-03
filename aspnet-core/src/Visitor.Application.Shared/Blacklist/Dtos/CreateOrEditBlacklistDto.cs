using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Visitor.Blacklist.Dtos
{

    public class CreateOrEditBlacklistDto : EntityDto<Guid?>
    {
        [Required]
        public virtual string FullName { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Remarks { get; set; }
    }
}

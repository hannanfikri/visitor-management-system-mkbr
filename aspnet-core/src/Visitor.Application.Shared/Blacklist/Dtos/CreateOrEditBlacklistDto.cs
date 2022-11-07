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
        public string BlacklistFullName { get; set; }
        public string BlacklistIdentityCard { get; set; }
        public string BlacklistPhoneNumber { get; set; }
        public string BlacklistRemarks { get; set; }
    }
}

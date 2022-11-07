using Abp.Application.Services.Dto;
using System;


namespace Visitor.Blacklist.Dtos
{
    public class BlacklistDto : EntityDto<Guid>
    {

        public string BlacklistFullName { get; set; }
        public string BlacklistIdentityCard { get; set; }
        public string BlacklistPhoneNumber { get; set; }
        public string BlacklistRemarks { get; set; }
    }
}

using Abp.Application.Services.Dto;
using System;


namespace Visitor.Blacklist.Dtos
{
    public class BlacklistDto : EntityDto<Guid>
    {

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Remarks { get; set; }
    }
}

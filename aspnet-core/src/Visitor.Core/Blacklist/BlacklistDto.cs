using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;


namespace Visitor.Blacklist
{
    [AutoMapFrom(typeof(BlacklistEnt))]
    public class BlacklistDto : EntityDto<Guid>
    {

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Remarks { get; set; }
    }
}

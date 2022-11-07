using Abp.Domain.Entities;
using System;

namespace Visitor.Blacklist
{
    public class BlacklistEnt : Entity<Guid>
    {

        public string BlacklistFullName { get; set; }
        public string BlacklistIdentityCard { get; set; }
        public string BlacklistPhoneNumber { get; set; }
        public string BlacklistRemarks { get; set; }
    }
}

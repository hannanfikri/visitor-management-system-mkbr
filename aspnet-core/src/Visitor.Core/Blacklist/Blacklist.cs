using Abp.Domain.Entities;
using System;

namespace Visitor.Blacklist
{
    public class BlacklistEnt : Entity<Guid>
    {

        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Remarks { get; set; }

        public BlacklistEnt(string fullName, string phoneNumber, string remarks)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            Remarks = remarks;
        }
    }
}

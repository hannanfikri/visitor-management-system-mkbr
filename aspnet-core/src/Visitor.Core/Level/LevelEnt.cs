using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Level
{
    public class LevelEnt : Entity<Guid>
    {
        public string LevelBankRakyat { get; set; } 
    }
}

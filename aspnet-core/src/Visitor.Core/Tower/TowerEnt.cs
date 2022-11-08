using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Tower
{
    public class TowerEnt:Entity<Guid>
    {
        public string TowerBankRakyat { get; set; }
    }
}

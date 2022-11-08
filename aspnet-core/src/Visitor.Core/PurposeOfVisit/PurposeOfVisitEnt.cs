using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.PurposeOfVisit
{
    public class PurposeOfVisitEnt : Entity<Guid>
    {
        public string PurposeOfVisitApp { get; set; }
    }
}

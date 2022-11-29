using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Title
{
    public class TitleEnt : Entity<Guid>
    {
        public string VisitorTitle { get; set; }
    }
}

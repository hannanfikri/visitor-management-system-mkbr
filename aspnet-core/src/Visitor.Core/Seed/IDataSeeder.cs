using Abp.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor.Seed
{
    public interface IDataSeeder
    {
        public Task SeedAsync();
    }
}

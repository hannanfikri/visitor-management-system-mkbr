using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Tower;

namespace Visitor.Seed
{
    public class SeedDataGenerator
    {
        public static IEnumerable<TowerEnt> GetSeedData()
        {
            return new List<TowerEnt>
            {
                new TowerEnt { TowerBankRakyat = "Tower 1" },
                new TowerEnt { TowerBankRakyat = "Tower 2" },
            };
        }
    }
}

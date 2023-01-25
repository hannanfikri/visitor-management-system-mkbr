using Abp;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Tower;

namespace Visitor.Seed
{
    public class TowerSeeder : IDataSeeder
    {
        private readonly IRepository<TowerEnt, Guid> _towerRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IGuidGenerator _guidGenerator;

        public TowerSeeder(IRepository<TowerEnt, Guid> towerRepository,IUnitOfWorkManager unitOfWorkManager,IGuidGenerator guidGenerator)
        {
            
            {
                _towerRepository = towerRepository;
                _unitOfWorkManager = unitOfWorkManager;
                _guidGenerator = guidGenerator;
            }
        }
        public async Task SeedAsync()
        {
            using (var uow = _unitOfWorkManager.Begin())
            {
                await _towerRepository.InsertAsync(new TowerEnt
                {
                    TowerBankRakyat = "Tower 1"
                });

                await _towerRepository.InsertAsync(new TowerEnt
                {
                    TowerBankRakyat = "Tower 2"
                });

                await uow.CompleteAsync();
            }
        }
    }
}

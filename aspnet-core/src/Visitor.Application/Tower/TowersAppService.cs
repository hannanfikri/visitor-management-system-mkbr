using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visitor.Authorization;
using Visitor.Tower.Dtos;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Visitor.Tower
{
    [AbpAuthorize(AppPermissions.Pages_Towers)]
    public class TowersAppService : VisitorAppServiceBase, ITowerAppService
    {

        private readonly IRepository<TowerEnt, Guid> _towerRepository;


        public TowersAppService(IRepository<TowerEnt, Guid> towerRepository)
        {
            _towerRepository = towerRepository;

        }

        public async Task<PagedResultDto<GetTowerForViewDto>> GetAll(GetAllTowersInput input)
        {

            var filteredTowers = _towerRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.TowerBankRakyat.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TowerFilter), e => e.TowerBankRakyat == input.TowerFilter);

            var pagedAndFilteredTowers = filteredTowers
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var towers = from o in pagedAndFilteredTowers
                             select new
                             {
                                 Id = o.Id,
                                 o.TowerBankRakyat
                             };

            var totalCount = await filteredTowers.CountAsync();

            var dbList = await towers.ToListAsync();
            var results = new List<GetTowerForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTowerForViewDto()
                {
                    Tower = new Dtos.TowerDto()
                    {

                        TowerBankRakyat = o.TowerBankRakyat,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTowerForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetTowerForViewDto> GetTowerForView(Guid id)
        {
            var tower = await _towerRepository.GetAsync(id);

            var output = new GetTowerForViewDto { Tower = ObjectMapper.Map<Dtos.TowerDto>(tower) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Towers_Edit)]
        public async Task<GetTowerForEditOutput> GettowerForEdit(EntityDto<Guid> input)
        {
            var tower = await _towerRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTowerForEditOutput { Tower = ObjectMapper.Map<CreateOrEditTowerDto>(tower) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditTowerDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Towers_Create)]
        protected virtual async Task Create(CreateOrEditTowerDto input)
        {
            var tower = ObjectMapper.Map<TowerEnt>(input);

            await _towerRepository.InsertAsync(tower);

        }

        [AbpAuthorize(AppPermissions.Pages_Towers_Edit)]
        protected virtual async Task Update(CreateOrEditTowerDto input)
        {
            var tower = await _towerRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, tower);

        }

        [AbpAuthorize(AppPermissions.Pages_Towers_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _towerRepository.DeleteAsync(input.Id);
        }
    }
}

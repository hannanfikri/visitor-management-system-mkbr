using Abp.Application.Services.Dto;
using Abp.Application.Services;
using Abp.Authorization;
using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using Visitor.Authorization;
using Abp.ObjectMapping;
using Visitor.Blacklist.Dtos;
using Abp.Collections.Extensions;
using Visitor.Dto;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace Visitor.Blacklist
{
    [AbpAuthorize(AppPermissions.Pages_Blacklists)]
    public class BlacklistsAppService : VisitorAppServiceBase, IBlacklistsAppService
    {
        private readonly IRepository<BlacklistEnt, Guid> _blacklistRepository;
        //private readonly IBlacklistsExcelExporter _BlacklistsExcelExporter;

        public BlacklistsAppService(IRepository<BlacklistEnt, Guid> blacklistRepository)//, IBlacklistsExcelExporter BlacklistsExcelExporter)
        {
            _blacklistRepository = blacklistRepository;
           // _BlacklistsExcelExporter = BlacklistsExcelExporter;

        }

        public async Task<PagedResultDto<GetBlacklistForViewDto>> GetAll(GetAllBlacklistsInput input)
        {

            var filteredBlacklists = _blacklistRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter);

            var pagedAndFilteredBlacklists = filteredBlacklists
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var blacklists = from o in pagedAndFilteredBlacklists
                            select new
                            {
                                Id = o.Id,
                                o.FullName,
                                o.PhoneNumber,
                                o.Remarks
                            };

            var totalCount = await filteredBlacklists.CountAsync();

            var dbList = await blacklists.ToListAsync();
            var results = new List<GetBlacklistForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetBlacklistForViewDto()
                {
                    Blacklist = new Dtos.BlacklistDto()
                    {

                        FullName = o.FullName,
                        PhoneNumber = o.PhoneNumber,
                        Remarks = o.Remarks,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetBlacklistForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetBlacklistForViewDto> GetBlacklistForView(Guid id)
        {
            var blacklist = await _blacklistRepository.GetAsync(id);

            var output = new GetBlacklistForViewDto { Blacklist = ObjectMapper.Map<Dtos.BlacklistDto>(blacklist) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Blacklists_Edit)]
        public async Task<GetBlacklistForEditOutput> GetblacklistForEdit(EntityDto<Guid> input)
        {
            var blacklist = await _blacklistRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetBlacklistForEditOutput { Blacklist = ObjectMapper.Map<CreateOrEditBlacklistDto>(blacklist) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditBlacklistDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Blacklists_Create)]
        protected virtual async Task Create(CreateOrEditBlacklistDto input)
        {
            var blacklist = ObjectMapper.Map<BlacklistEnt>(input);

            await _blacklistRepository.InsertAsync(blacklist);

        }

        [AbpAuthorize(AppPermissions.Pages_Blacklists_Edit)]
        protected virtual async Task Update(CreateOrEditBlacklistDto input)
        {
            var blacklist = await _blacklistRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, blacklist);

        }

        [AbpAuthorize(AppPermissions.Pages_Blacklists_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _blacklistRepository.DeleteAsync(input.Id);
        }

        /*public async Task<FileDto> GetBlacklistsToExcel(GetAllBlacklistsForExcelInput input)
        {

            var filteredBlacklists = _blacklistRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.FullName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FullNameFilter), e => e.FullName == input.FullNameFilter);

            var query = (from o in filteredBlacklists
                         select new GetBlacklistForViewDto()
                         {
                             Blacklist = new Dtos.BlacklistDto  
                             {
                                 FullName = o.FullName,
                                 Id = o.Id
                             }
                         });

            var blacklistListDtos = await query.ToListAsync();

            return _BlacklistsExcelExporter.ExportToFile(blacklistListDtos);
        }*/

    }
}

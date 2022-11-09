using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using Abp.Authorization;
using Visitor.Authorization;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using AutoMapper;
using Visitor.Status.Dtos;

namespace Visitor.Status
{
    [AbpAuthorize(AppPermissions.Pages_Statuses)]
    public class StatusAppService : VisitorAppServiceBase 
    {
        private readonly IRepository<StatusEnt, Guid> _statusRepository;


        public StatusAppService(IRepository<StatusEnt, Guid> statusRepository)
        {
            _statusRepository = statusRepository;

        }
        public async Task<PagedResultDto<GetStatusForViewDto>> GetAll(GetAllStatusesInput input)
    {

        var filteredStatuss = _statusRepository.GetAll()
                    .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.StatusApp.Contains(input.Filter))
                    .WhereIf(!string.IsNullOrWhiteSpace(input.StatusFilter), e => e.StatusApp == input.StatusFilter);

        var pagedAndFilteredStatuss = filteredStatuss
            .OrderBy(input.Sorting ?? "id asc")
            .PageBy(input);

        var statuses = from o in pagedAndFilteredStatuss
                     select new
                     {
                         Id = o.Id,
                         o.StatusApp
                     };

        var totalCount = await filteredStatuss.CountAsync();

        var dbList = await statuses.ToListAsync();
        var results = new List<GetStatusForViewDto>();

        foreach (var o in dbList)
        {
            var res = new GetStatusForViewDto()
            {
                Status = new Dtos.StatusDto()
                {

                    StatusApp = o.StatusApp,
                    Id = o.Id,
                }
            };

            results.Add(res);
        }

        return new PagedResultDto<GetStatusForViewDto>(
            totalCount,
            results
        );

    }

    public async Task<GetStatusForViewDto> GetStatusForView(Guid id)
    {
        var status = await _statusRepository.GetAsync(id);

        var output = new GetStatusForViewDto { Status = ObjectMapper.Map<Dtos.StatusDto>(status) };

        return output;
    }

    [AbpAuthorize(AppPermissions.Pages_Statuses_Edit)]
    public async Task<GetStatusForEditOutput> GetStatusForEdit(EntityDto<Guid> input)
    {
        var status = await _statusRepository.FirstOrDefaultAsync(input.Id);

        var output = new GetStatusForEditOutput { Status = ObjectMapper.Map<CreateOrEditStatusDto>(status) };

        return output;
    }

    public async Task CreateOrEdit(CreateOrEditStatusDto input)
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

    [AbpAuthorize(AppPermissions.Pages_Statuses_Create)]
    protected virtual async Task Create(CreateOrEditStatusDto input)
    {
        var status = ObjectMapper.Map<StatusEnt>(input);

        await _statusRepository.InsertAsync(status);

    }

    [AbpAuthorize(AppPermissions.Pages_Statuses_Edit)]
    protected virtual async Task Update(CreateOrEditStatusDto input)
    {
        var status = await _statusRepository.FirstOrDefaultAsync((Guid)input.Id);
        ObjectMapper.Map(input, status);

    }

    [AbpAuthorize(AppPermissions.Pages_Statuses_Delete)]
    public async Task Delete(EntityDto<Guid> input)
    {
        await _statusRepository.DeleteAsync(input.Id);
    }
}
}

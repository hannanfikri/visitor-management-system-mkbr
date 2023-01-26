using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Visitor.Authorization;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace Visitor.Appointment.ExpiredUrl
{
    public class ExpiredUrlsAppService : VisitorAppServiceBase, IExpiredUrlsAppService
    {
        private readonly IRepository<ExpiredUrlEnt, Guid> _expiredUrlRepository;
        private readonly IRepository<AppointmentEnt, Guid> _lookup_appointmentRepository;

        public ExpiredUrlsAppService(IRepository<ExpiredUrlEnt, Guid> expiredUrlRepository, IRepository<AppointmentEnt, Guid> lookup_appointmentRepository)
        {
            _expiredUrlRepository = expiredUrlRepository;
            _lookup_appointmentRepository = lookup_appointmentRepository;

        }

        public async Task<PagedResultDto<GetExpiredUrlForViewDto>> GetAll(GetAllExpiredUrlsInput input)
        {

            var filteredExpiredUrls = _expiredUrlRepository.GetAll()
                        .Include(e => e.AppointmentFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.Item.Contains(input.Filter))
                        .WhereIf(input.MinUrlCreateDateFilter != null, e => e.UrlCreateDate >= input.MinUrlCreateDateFilter)
                        .WhereIf(input.MaxUrlCreateDateFilter != null, e => e.UrlCreateDate <= input.MaxUrlCreateDateFilter)
                        .WhereIf(input.MinUrlExpiredDateFilter != null, e => e.UrlExpiredDate >= input.MinUrlExpiredDateFilter)
                        .WhereIf(input.MaxUrlExpiredDateFilter != null, e => e.UrlExpiredDate <= input.MaxUrlExpiredDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ItemFilter), e => e.Item == input.ItemFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AppointmentFullNameFilter), e => e.AppointmentFk != null && e.AppointmentFk.FullName == input.AppointmentFullNameFilter);

            var pagedAndFilteredExpiredUrls = filteredExpiredUrls
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var expiredUrls = from o in pagedAndFilteredExpiredUrls
                              join o1 in _lookup_appointmentRepository.GetAll() on o.AppointmentId equals o1.Id into j1
                              from s1 in j1.DefaultIfEmpty()

                              select new
                              {

                                  o.UrlCreateDate,
                                  o.UrlExpiredDate,
                                  o.Item,
                                  Id = o.Id,
                                  AppointmentFullName = s1 == null || s1.FullName == null ? "" : s1.FullName.ToString()
                              };

            var totalCount = await filteredExpiredUrls.CountAsync();

            var dbList = await expiredUrls.ToListAsync();
            var results = new List<GetExpiredUrlForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetExpiredUrlForViewDto()
                {
                    ExpiredUrl = new ExpiredUrlDto
                    {

                        UrlCreateDate = o.UrlCreateDate,
                        UrlExpiredDate = o.UrlExpiredDate.ToString("MM/dd/yyyy hh:mm:ss tt"),
                        Item = o.Item,
                        Id = o.Id,
                    },
                    AppointmentFullName = o.AppointmentFullName
                };

                results.Add(res);
            }

            return new PagedResultDto<GetExpiredUrlForViewDto>(
                totalCount,
                results
            );

        }

        [AbpAuthorize(AppPermissions.Pages_ExpiredUrls_Edit)]
        public async Task<GetExpiredUrlForEditOutput> GetExpiredUrlForEdit(EntityDto<Guid> input)
        {
            var expiredUrl = await _expiredUrlRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetExpiredUrlForEditOutput { ExpiredUrl = ObjectMapper.Map<CreateOrEditExpiredUrlDto>(expiredUrl) };

            if (output.ExpiredUrl.AppointmentId != null)
            {
                var _lookupAppointment = await _lookup_appointmentRepository.FirstOrDefaultAsync((Guid)output.ExpiredUrl.AppointmentId);
                output.AppointmentFullName = _lookupAppointment?.FullName?.ToString();
            }

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditExpiredUrlDto input)
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

        //[AbpAuthorize(AppPermissions.Pages_ExpiredUrls_Create)]
        protected virtual async Task Create(CreateOrEditExpiredUrlDto input)
        {
            var expiredUrl = ObjectMapper.Map<ExpiredUrlEnt>(input);

            await _expiredUrlRepository.InsertAsync(expiredUrl);

        }

        //[AbpAuthorize(AppPermissions.Pages_ExpiredUrls_Edit)]
        protected virtual async Task Update(CreateOrEditExpiredUrlDto input)
        {
            var expiredUrl = await _expiredUrlRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, expiredUrl);

        }

        [AbpAuthorize(AppPermissions.Pages_ExpiredUrls_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _expiredUrlRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_ExpiredUrls)]
        public async Task<PagedResultDto<ExpiredUrlAppointmentLookupTableDto>> GetAllAppointmentForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_appointmentRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.FullName != null && e.FullName.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var appointmentList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<ExpiredUrlAppointmentLookupTableDto>();
            foreach (var appointment in appointmentList)
            {
                lookupTableDtoList.Add(new ExpiredUrlAppointmentLookupTableDto
                {
                    Id = appointment.Id.ToString(),
                    DisplayName = appointment.FullName?.ToString()
                });
            }

            return new PagedResultDto<ExpiredUrlAppointmentLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Visitor.Company.Exporting;
using Visitor.Company.Dtos;
using Visitor.Dto;
using Abp.Application.Services.Dto;
using Visitor.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Visitor.Storage;

namespace Visitor.Company
{
    [AbpAuthorize(AppPermissions.Pages_Companies)]
    public class CompaniesAppService : VisitorAppServiceBase, ICompaniesAppService
    {
        private readonly IRepository<CompanyEnt, Guid> _companyRepository;
        private readonly ICompaniesExcelExporter _companiesExcelExporter;

        public CompaniesAppService(IRepository<CompanyEnt, Guid> companyRepository, ICompaniesExcelExporter companiesExcelExporter)
        {
            _companyRepository = companyRepository;
            _companiesExcelExporter = companiesExcelExporter;

        }

        public async Task<PagedResultDto<GetCompanyForViewDto>> GetAll(GetAllCompaniesInput input)
        {

            var filteredCompanies = _companyRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CompanyName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter);

            var pagedAndFilteredCompanies = filteredCompanies
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var companies = from o in pagedAndFilteredCompanies
                            select new
                            {
                                Id = o.Id,
                                o.CompanyName,
                                o.CompanyEmail,
                                o.OfficePhoneNumber,
                                o.CompanyAddress
                            };

            var totalCount = await filteredCompanies.CountAsync();

            var dbList = await companies.ToListAsync();
            var results = new List<GetCompanyForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetCompanyForViewDto()
                {
                    Company = new CompanyDto
                    {

                        CompanyName = o.CompanyName,
                        CompanyEmail = o.CompanyEmail,
                        OfficePhoneNumber = o.OfficePhoneNumber,
                        CompanyAddress = o.CompanyAddress,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetCompanyForViewDto>(
                totalCount,
                results
            );

        }

        public async Task<GetCompanyForViewDto> GetCompanyForView(Guid id)
        {
            var company = await _companyRepository.GetAsync(id);

            var output = new GetCompanyForViewDto { Company = ObjectMapper.Map<CompanyDto>(company) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Companies_Edit)]
        public async Task<GetCompanyForEditOutput> GetCompanyForEdit(EntityDto<Guid> input)
        {
            var company = await _companyRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetCompanyForEditOutput { Company = ObjectMapper.Map<CreateOrEditCompanyDto>(company) };

            return output;
        }

        public async Task CreateOrEdit(CreateOrEditCompanyDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Companies_Create)]
        protected virtual async Task Create(CreateOrEditCompanyDto input)
        {
            var company = ObjectMapper.Map<CompanyEnt>(input);

            await _companyRepository.InsertAsync(company);

        }

        [AbpAuthorize(AppPermissions.Pages_Companies_Edit)]
        protected virtual async Task Update(CreateOrEditCompanyDto input)
        {
            var company = await _companyRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, company);

        }

        [AbpAuthorize(AppPermissions.Pages_Companies_Delete)]
        public async Task Delete(EntityDto<Guid> input)
        {
            await _companyRepository.DeleteAsync(input.Id);
        }

        public async Task<FileDto> GetCompaniesToExcel(GetAllCompaniesForExcelInput input)
        {

            var filteredCompanies = _companyRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.CompanyName.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CompanyNameFilter), e => e.CompanyName == input.CompanyNameFilter);

            var query = (from o in filteredCompanies
                         select new GetCompanyForViewDto()
                         {
                             Company = new CompanyDto
                             {
                                 CompanyName = o.CompanyName,
                                 Id = o.Id
                             }
                         });

            var companyListDtos = await query.ToListAsync();

            return _companiesExcelExporter.ExportToFile(companyListDtos);
        }

    }
}
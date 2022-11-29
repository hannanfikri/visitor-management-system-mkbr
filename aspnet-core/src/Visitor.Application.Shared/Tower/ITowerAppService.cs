using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Visitor.Tower.Dtos;

namespace Visitor.Tower
{
    public interface ITowerAppService : IApplicationService
    {
        Task<PagedResultDto<GetTowerForViewDto>> GetAll(GetAllTowersInput Input);
        Task<GetTowerForViewDto> GetTowerForView(Guid id);
        Task<GetTowerForEditOutput> GettowerForEdit(EntityDto<Guid> id);
        Task CreateOrEdit(CreateOrEditTowerDto input);
        Task Delete(EntityDto<Guid> id);
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Visitor.Dto;

namespace Visitor.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}

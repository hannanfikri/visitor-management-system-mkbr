using System.Threading.Tasks;
using Visitor.Sessions.Dto;

namespace Visitor.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}

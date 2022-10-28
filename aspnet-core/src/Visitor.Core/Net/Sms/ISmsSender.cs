using System.Threading.Tasks;

namespace Visitor.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}
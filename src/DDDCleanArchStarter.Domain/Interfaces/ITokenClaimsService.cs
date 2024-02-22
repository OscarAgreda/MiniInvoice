using System.Threading.Tasks;
namespace DDDCleanArchStarter.Domain.Interfaces
{
    public interface ITokenClaimsService
    {
        Task<string> GetTokenAsync(string userName);
    }
}
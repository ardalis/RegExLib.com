using System.Threading.Tasks;

namespace RegExLib.Core.Interfaces
{
  public interface ITokenClaimsService
  {
    Task<string> GetTokenAsync(string userName);
  }
}

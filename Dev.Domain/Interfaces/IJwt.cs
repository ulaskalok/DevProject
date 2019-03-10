using System.Security.Claims;

namespace Dev.Domain.Interfaces
{
    public interface IJwt
    {
        string GetJwt(ClaimsIdentity identity);
    }
}

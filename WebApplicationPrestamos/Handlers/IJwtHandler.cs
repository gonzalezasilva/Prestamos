using WebApplicationPrestamos.Models;

namespace WebApplicationPrestamos.Handlers
{
    public interface IJwtHandler
    {
        string GenerateToken(UserInfo user, IEnumerable<string> roles);
    }
}
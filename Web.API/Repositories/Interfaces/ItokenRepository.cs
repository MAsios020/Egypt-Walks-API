using Microsoft.AspNetCore.Identity;

namespace Web.API.Repositories.Interfaces
{
    public  interface ItokenRepository
    {

        string GenerateTokenAsync(IdentityUser User, string[] roles);

    }
}

using Microsoft.AspNetCore.Identity;

namespace Library.Models
{
    /// <summary>
    /// User model (extend default identity user)
    /// </summary>
    public class User: IdentityUser<int>
    {
        
    }
}
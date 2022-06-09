using System.Security.Claims;
using TodoApi.Entities;

namespace TodoApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static IEnumerable<Claim> GetClaims(this User user) 
        {
            var result = new List<Claim>
            {
                new(ClaimTypes.Name, user.Email)
            };
            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));
            return result;
        }
    }
}

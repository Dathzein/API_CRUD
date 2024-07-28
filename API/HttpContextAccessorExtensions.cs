using System.Security.Claims;

namespace API
{

    public static class HttpContextAccessorExtensions
    {
        public static string GetContainerRoleFromClaims(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            Claim? claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role);

            if (claim is null || string.IsNullOrWhiteSpace(claim.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(httpContextAccessor), "usuario no tiene la definicion de 'Role'.");
            }
            return claim.Value;
        }

        public static string GetUserIdFromClaims(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                throw new ArgumentNullException(nameof(httpContextAccessor));
            }

            Claim? claim = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name);

            if (claim is null || string.IsNullOrWhiteSpace(claim.Value))
            {
                throw new ArgumentOutOfRangeException(nameof(httpContextAccessor), "usuario no tiene la definicion de 'UserId'.");
            }

            return claim.Value;
        }
    }
}

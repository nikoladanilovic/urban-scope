using System.Security.Claims;

namespace UrbanScope.Helpers
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var idStr =
                user.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? user.FindFirstValue("sub");

            if (string.IsNullOrWhiteSpace(idStr))
                throw new InvalidOperationException("User id claim is missing.");

            return Guid.Parse(idStr);
        }
    }
}

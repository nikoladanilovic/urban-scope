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

            if (!Guid.TryParse(idStr, out var id))
                throw new InvalidOperationException("User id claim is not a valid GUID.");

            return id;
        }
    }
}

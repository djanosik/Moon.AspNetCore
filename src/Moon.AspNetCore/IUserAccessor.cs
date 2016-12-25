using System.Security.Claims;

namespace Moon.AspNetCore
{
    /// <summary>
    /// Provides access to the current application user.
    /// </summary>
    public interface IUserAccessor
    {
        /// <summary>
        /// Gets the current application user.
        /// </summary>
        ClaimsPrincipal User { get; }
    }
}
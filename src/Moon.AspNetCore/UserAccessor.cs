using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moon.Security;

namespace Moon.AspNetCore
{
    /// <summary>
    /// Uses <see cref="IHttpContextAccessor" /> to provide the current application user.
    /// </summary>
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor httpAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessor" /> class.
        /// </summary>
        /// <param name="httpAccessor">The HTTP context accessor.</param>
        public UserAccessor(IHttpContextAccessor httpAccessor)
        {
            Requires.NotNull(httpAccessor, nameof(httpAccessor));

            this.httpAccessor = httpAccessor;
        }

        /// <summary>
        /// Gets the current application user.
        /// </summary>
        public ClaimsPrincipal User => httpAccessor.HttpContext.User;
    }
}
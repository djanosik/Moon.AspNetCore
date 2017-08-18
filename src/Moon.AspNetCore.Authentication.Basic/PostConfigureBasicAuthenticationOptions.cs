using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// Used to setup defaults for all <see cref="BasicAuthenticationOptions" />.
    /// </summary>
    public class PostConfigureBasicAuthenticationOptions : IPostConfigureOptions<BasicAuthenticationOptions>
    {
        /// <summary>
        /// Invoked to post configure a <see cref="BasicAuthenticationOptions"/> instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configure.</param>
        public void PostConfigure(string name, BasicAuthenticationOptions options)
        {
            if (string.IsNullOrEmpty(options.Realm))
            {
                options.Realm = BasicAuthenticationDefaults.Realm;
            }
        }
    }
}
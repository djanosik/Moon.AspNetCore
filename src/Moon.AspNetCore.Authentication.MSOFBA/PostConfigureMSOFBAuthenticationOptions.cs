using System.Drawing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Moon.AspNetCore.Authentication.MSOFBA
{
    /// <summary>
    /// Used to setup defaults for all <see cref="MSOFBAuthenticationOptions" />.
    /// </summary>
    public class PostConfigureMSOFBAuthenticationOptions : IPostConfigureOptions<MSOFBAuthenticationOptions>
    {
        /// <summary>
        /// Invoked to post configure a <see cref="MSOFBAuthenticationOptions" /> instance.
        /// </summary>
        /// <param name="name">The name of the options instance being configured.</param>
        /// <param name="options">The options instance to configure.</param>
        public void PostConfigure(string name, MSOFBAuthenticationOptions options)
        {
            if (!options.LoginPath.HasValue)
            {
                options.LoginPath = CookieAuthenticationDefaults.LoginPath;
            }

            if (!options.LoginSuccessPath.HasValue)
            {
                options.LoginSuccessPath = new PathString("/");
            }

            if (string.IsNullOrEmpty(options.ReturnUrlParameter))
            {
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            }

            if (options.DialogSize.IsEmpty)
            {
                options.DialogSize = new Size(800, 600);
            }
        }
    }
}
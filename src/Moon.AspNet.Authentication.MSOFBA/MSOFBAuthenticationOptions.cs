using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Http;

namespace Moon.AspNet.Authentication.MSOFBA
{
    /// <summary>
    /// Contains the options used by the <see cref="MSOFBAuthenticationMiddleware" />.
    /// </summary>
    public class MSOFBAuthenticationOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MSOFBAuthenticationOptions" /> class.
        /// </summary>
        public MSOFBAuthenticationOptions()
        {
            LoginPath = CookieAuthenticationDefaults.LoginPath;
            ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        }

        /// <summary>
        /// Gets or sets the path to login page used by MS-OFBA.
        /// </summary>
        /// <remarks>
        /// This page will be displayed in dialog box presented by Microsoft Office or other
        /// OFBA-enabled application.
        /// </remarks>
        public PathString LoginPath { get; set; }

        /// <summary>
        /// Gets or sets the parameter name used to pass the return URL.
        /// </summary>
        public string ReturnUrlParameter { get; set; }
    }
}
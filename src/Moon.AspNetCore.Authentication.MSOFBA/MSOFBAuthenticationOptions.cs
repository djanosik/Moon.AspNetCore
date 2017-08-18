using System.Drawing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Moon.AspNetCore.Authentication.MSOFBA
{
    /// <summary>
    /// Contains the options used by the <see cref="MSOFBAuthenticationMiddleware" />.
    /// </summary>
    public class MSOFBAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// Gets or sets the path to login page used by MS-OFBA.
        /// </summary>
        /// <remarks>
        /// This page will be displayed in dialog box presented by Microsoft Office or other
        /// OFBA-enabled application.
        /// </remarks>
        public PathString LoginPath { get; set; }

        /// <summary>
        /// Specifies the path to log-in redirect URL which is used to indicate successful login
        /// </summary>
        /// <remarks>
        /// On seeing the redirect, the client determines that this URI matches that returned in response to the 
        /// Protocol Discovery request. In case the URIs match, the client assumes success, follows the 
        /// redirect, and closes the form.
        /// </remarks>
        public PathString LoginSuccessPath { get; set; }

        /// <summary>
        /// Gets or sets the parameter name used to pass the return URL.
        /// </summary>
        public string ReturnUrlParameter { get; set; }

        /// <summary>
        /// Gets or sets the size of the OFBA log-in dialog.
        /// </summary>
        public Size DialogSize { get; set; }
    }
}
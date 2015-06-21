using System.Text.RegularExpressions;

namespace Moon.AspNet
{
    /// <summary>
    /// Options for <see cref="OneHostNameMiddleware" />.
    /// </summary>
    public class OneHostNameOptions
    {
        /// <summary>
        /// Gets or sets the preferred host name (eg. "onedomain.com").
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// Gets or sets the pattern used to match the preferred host name (eg. "^onedomain\.com$").
        /// </summary>
        public Regex Pattern { get; set; }
    }
}
using System;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace

namespace Moon.AspNetCore.Authentication.Basic
{
    /// <summary>
    /// Defines Basic authentication events. This may be used as a base class or may be instantiated directly.
    /// </summary>
    public class BasicAuthenticationEvents
    {
        /// <summary>
        /// A delegate assigned to this property will be invoked when the related method is called.
        /// </summary>
        public Func<BasicSignInContext, Task> OnSignIn { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Implements the interface method by invoking the related delegate method.
        /// </summary>
        /// <param name="context">Contains information about the event.</param>
        public virtual Task SignInAsync(BasicSignInContext context)
            => OnSignIn(context);
    }
}
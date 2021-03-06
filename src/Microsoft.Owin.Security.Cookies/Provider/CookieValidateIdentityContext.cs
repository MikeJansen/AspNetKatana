// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.Owin.Security.Provider;

namespace Microsoft.Owin.Security.Cookies
{
    /// <summary>
    /// Context object passed to the ICookieAuthenticationProvider method ValidateIdentity.
    /// </summary>
    public class CookieValidateIdentityContext : BaseContext<CookieAuthenticationOptions>
    {
        /// <summary>
        /// Creates a new instance of the context object.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ticket">Contains the initial values for identity and extra data</param>
        /// <param name="options"></param>
        public CookieValidateIdentityContext(IOwinContext context, AuthenticationTicket ticket, CookieAuthenticationOptions options)
            : base(context, options)
        {
            if (ticket == null)
            {
                throw new ArgumentNullException("ticket");
            }

            Identity = ticket.Identity;
            Properties = ticket.Properties;
        }

        /// <summary>
        /// Contains the claims identity arriving with the request. May be altered to change the 
        /// details of the authenticated user.
        /// </summary>
        public ClaimsIdentity Identity { get; private set; }

        /// <summary>
        /// Contains the extra meta-data arriving with the request ticket. May be altered.
        /// </summary>
        public AuthenticationProperties Properties { get; private set; }

        /// <summary>
        /// Called to replace the claims identity. The supplied identity will replace the value of the 
        /// Identity property, which determines the identity of the authenticated request.
        /// </summary>
        /// <param name="identity">The identity used as the replacement</param>
        public void ReplaceIdentity(IIdentity identity)
        {
            Identity = identity as ClaimsIdentity ?? new ClaimsIdentity(identity);
        }

        /// <summary>
        /// Called to reject the incoming identity. This may be done if the application has determined the
        /// account is no longer active, and the request should be treated as if it was anonymous.
        /// </summary>
        public void RejectIdentity()
        {
            Identity = null;
        }
    }
}

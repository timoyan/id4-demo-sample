// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.




using System;
using System.Threading.Tasks;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace id4_server
{
    public class CustomProfileService<ApplicationUser> : IProfileService
        where ApplicationUser : class
    {
        /// <summary>
        /// The claims factory.
        /// </summary>
        protected readonly IUserClaimsPrincipalFactory<ApplicationUser> ClaimsFactory;
        
        /// <summary>
        /// The logger
        /// </summary>
        protected readonly ILogger<CustomProfileService<ApplicationUser>> Logger;

        /// <summary>
        /// The user manager.
        /// </summary>
        protected readonly UserManager<ApplicationUser> UserManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService{TUser}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="claimsFactory">The claims factory.</param>
        public CustomProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            UserManager = userManager;
            ClaimsFactory = claimsFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService{TUser}"/> class.
        /// </summary>
        /// <param name="userManager">The user manager.</param>
        /// <param name="claimsFactory">The claims factory.</param>
        /// <param name="logger">The logger.</param>
        public CustomProfileService(UserManager<ApplicationUser> userManager,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            ILogger<CustomProfileService<ApplicationUser>> logger)
        {
            UserManager = userManager;
            ClaimsFactory = claimsFactory;
            Logger = logger;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No sub claim present");

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }
            else
            {
                var principal = await ClaimsFactory.CreateAsync(user);
                if (principal == null) throw new Exception("ClaimsFactory failed to create a principal");

                context.AddRequestedClaims(principal.Claims);
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject?.GetSubjectId();
            if (sub == null) throw new Exception("No subject Id claim present");

            var user = await UserManager.FindByIdAsync(sub);
            if (user == null)
            {
                Logger?.LogWarning("No user found matching subject Id: {0}", sub);
            }

            context.IsActive = user != null;
        }
    }
}
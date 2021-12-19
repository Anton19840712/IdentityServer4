using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Authorization.IdentityServer.Infrastructure
{
    /// <summary>
    /// Enables to create different claims.
    /// </summary>
    public class ProfileService : IProfileService //from Identity4
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context) //can inject directly from is4
        {
            var claims = new List<Claim> //we create and configure claims here:
            {
                new Claim(ClaimTypes.DateOfBirth, "01.01.2010"),
                new Claim(ClaimTypes.Country, "Belarus"),
                new Claim(ClaimTypes.Gender, "male")
            };
            context.IssuedClaims.AddRange(claims);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context) //can inject directly from is4
        {
            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Authorization.IdentityServer.Infrastructure
{
    /// <summary>
    /// Class allows to change default process of creation of claims.
    /// </summary>
    ///
    /// See nimble framework - there are used not claims, but permissions.
    public class ProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}

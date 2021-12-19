using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Client.Mvc
{
    public class OlderThanRequirementHandler : AuthorizationHandler<OlderThanRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OlderThanRequirement requirement)
        {
            var hasClaim = context.User.HasClaim(x => x.Type == ClaimTypes.DateOfBirth);
            if (!hasClaim)
            {
                return Task.CompletedTask;
            }

            var dateOfBirth = context.User.FindFirst(x => x.Type == ClaimTypes.DateOfBirth).Value;
            var date = DateTime.Parse(dateOfBirth, new CultureInfo("ru-RU"));
            var canEnterDiff = DateTime.Now.Year - date.Year;
            if (canEnterDiff>=requirement.Years)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
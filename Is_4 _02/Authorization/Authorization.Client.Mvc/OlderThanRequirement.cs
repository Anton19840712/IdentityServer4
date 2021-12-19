using Microsoft.AspNetCore.Authorization;

namespace Authorization.Client.Mvc
{
    public class OlderThanRequirement : IAuthorizationRequirement
    {
        public OlderThanRequirement(int years)
        {
            Years = years;
        }

        public int Years { get; set; }
    }
}
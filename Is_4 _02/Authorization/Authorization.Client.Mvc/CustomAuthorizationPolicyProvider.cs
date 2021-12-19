using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Authorization.Client.Mvc
{
    //Instead of AddAuthorization in Startup.cs
    //config.AddPolicy("OlderThan", builder =>
    //{
    //    builder.AddRequirements(new OlderThanRequirement(10));
    //});


    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            this._options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policyExists = await base.GetPolicyAsync(policyName);
            if (policyExists == null)
            {
                policyExists = new AuthorizationPolicyBuilder().AddRequirements(new OlderThanRequirement(10)).Build();//более гибко добавляешь требования к клиенту
                _options.AddPolicy(policyName, policyExists);
            }

            return policyExists;
        }
    }
    }
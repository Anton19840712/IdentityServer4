using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Authorization.Client.Mvc.ViewModels
{
    public class ClaimViewer
    {
        public ClaimViewer(string name, IEnumerable<Claim> claims)
        {
            if (claims == null) throw new ArgumentNullException(nameof(claims));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Claims = claims.ToList();
            Token = "N/A";
        }

        public ClaimViewer(string name, string tokenJson)
        {
            Claims = ((JwtSecurityToken)new JwtSecurityTokenHandler().ReadToken(tokenJson)).Claims?.ToList();

            Token = tokenJson;

            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public List<Claim> Claims { get; }

        public string Name { get; }

        public string Token { get; }
    }
}
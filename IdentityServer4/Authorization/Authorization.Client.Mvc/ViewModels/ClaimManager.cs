using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Authorization.Client.Mvc.ViewModels
{
    public class ClaimManager
    {
        public ClaimManager(HttpContext context, ClaimsPrincipal user)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            Items = new List<ClaimViewer>();

            var claims = user.Claims.ToList();

            var idTokenJson = context.GetTokenAsync("id_token").GetAwaiter().GetResult();
            
            var accessTokenJson = context.GetTokenAsync("access_token").GetAwaiter().GetResult();

            var refreshTokenJson = context.GetTokenAsync("refresh_token").GetAwaiter().GetResult();

            if (idTokenJson != null)
            {
                AddTokenInfo("Identity Token", idTokenJson);
            }

            if (accessTokenJson != null)
            {
                AddTokenInfo("Access Token", accessTokenJson);
            }

            if (refreshTokenJson != null)
            {
                AddTokenInfo("Refresh Token", refreshTokenJson);
            }

            AddTokenInfo("User Claims", claims);
        }

        public List<ClaimViewer> Items { get; }

        public string AccessToken
        {
            get
            {
                if (Items == null || Items.Count == 0)
                {
                    throw new InvalidOperationException("Not tokens found");
                }
                var token = Items.SingleOrDefault(x => x.Name == "Access Token");
                if (token == null)
                {
                    throw new InvalidOperationException("Not tokens found");
                }

                return token.Token;
            }
        }

        public string RefreshToken
        {
            get
            {
                if (Items == null || Items.Count == 0)
                {
                    throw new InvalidOperationException("Not tokens found");
                }
                var token = Items.SingleOrDefault(x => x.Name == "Refresh Token");
                if (token == null)
                {
                    throw new InvalidOperationException("Not tokens found");
                }

                return token.Token;
            }
        }

        private void AddTokenInfo(string nameToken, string idTokenJson)
        {
            Items.Add(new ClaimViewer(nameToken, idTokenJson));
        }

        private void AddTokenInfo(string nameToken, IEnumerable<Claim> claims)
        {
            Items.Add(new ClaimViewer(nameToken, claims));
        }
    }
}
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System.Security.Claims;
using System.Text.Json;

namespace HadisIelts.Client.Services.Authorization
{
    public class CustomUserFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
        public CustomUserFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
        {
        }
        public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            var user = await base.CreateUserAsync(account, options);
            var claimsIdentity = (ClaimsIdentity)user.Identity!;
            if (account is not null)
            {
                MapArrayClaimsToMultipleSeprateClaims(account, claimsIdentity);
            }
            return user;
        }
        private void MapArrayClaimsToMultipleSeprateClaims(RemoteUserAccount account, ClaimsIdentity claimsIdentity)
        {
            foreach (var prop in account.AdditionalProperties)
            {
                var key = prop.Key;
                var value = prop.Value;
                if (value is not null
                    && value is JsonElement element
                    && element.ValueKind == JsonValueKind.Array)
                {
                    claimsIdentity.RemoveClaim(claimsIdentity.FindFirst(prop.Key));
                    var claims = element.EnumerateArray().Select(x => new Claim(key, x.ToString()));
                    claimsIdentity.AddClaims(claims);
                }
            }
        }
    }
}

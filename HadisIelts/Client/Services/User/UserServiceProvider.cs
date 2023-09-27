using Microsoft.AspNetCore.Components.Authorization;

namespace HadisIelts.Client.Services.User
{
    public class UserServiceProvider : IUserServices
    {
        private readonly AuthenticationStateProvider _authenticationProvider;
        public UserServiceProvider(AuthenticationStateProvider authenticationProvider)
        {
            _authenticationProvider = authenticationProvider;
        }
        public async Task<string> GetUserIdAsync()
        {
            var claims = await _authenticationProvider.GetAuthenticationStateAsync();
            var claim = claims.User.Claims.FirstOrDefault(claim => claim.Type == "sub");
            return claim?.Value;
        }
    }
}

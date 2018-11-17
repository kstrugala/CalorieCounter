using System;
using System.Linq;
using System.Threading.Tasks;
using CalorieCounter.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;

namespace CalorieCounter.Infrastructure.Services
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSettings _jwtSettings;

        public TokenManager(IDistributedCache cache, IHttpContextAccessor httpContextAccessor, JwtSettings jwtSettings)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = jwtSettings;
        }      

        public async Task<bool> IsCurrentActiveToken()
            => await IsActiveAsync(GetCurrentAsync());


        public async Task DeactivateCurrentToken()
            => await DeactivateAsync(GetCurrentAsync());


        public async Task DeactivateAsync(string token)
           => await _cache.SetStringAsync(GetKey(token), " ", new DistributedCacheEntryOptions{
               AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_jwtSettings.ExpiryMinutes)
           });
        
        public async Task<bool> IsActiveAsync(string token)
            => await _cache.GetStringAsync(GetKey(token)) == null;


        private string GetCurrentAsync()
        {
            var authorizationHeader = _httpContextAccessor
                .HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }

        private static string GetKey(string token)
            => $"tokens:{token}:deactivated";      
    }
}
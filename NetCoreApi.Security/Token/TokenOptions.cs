using System;
using Microsoft.IdentityModel.Tokens;

namespace NetCoreApi.Security.Token
{
    public static class TokenOptions
    {
        public static string Audience { get; } = "NetCoreApiAudience";
        public static string Issuer { get; } = "NetCoreApiIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
        public static string TokenType { get; } = "Bearer";
    }
}

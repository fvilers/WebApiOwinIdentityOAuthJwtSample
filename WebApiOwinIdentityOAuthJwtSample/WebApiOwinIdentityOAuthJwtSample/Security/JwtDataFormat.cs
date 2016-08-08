using Microsoft.Owin.Security;
using System;
using System.IdentityModel.Tokens;

namespace WebApiOwinIdentityOAuthJwtSample.Security
{
    internal class JwtDataFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly SigningCredentials _signingCredentials;
        private readonly TimeSpan _expiration;

        public JwtDataFormat(string issuer, string audience, byte[] key, TimeSpan expiration)
        {
            if (issuer == null) throw new ArgumentNullException(nameof(issuer));
            if (audience == null) throw new ArgumentNullException(nameof(audience));
            if (key == null) throw new ArgumentNullException(nameof(key));
            _issuer = issuer;
            _audience = audience;
            _signingCredentials = new SigningCredentials(
                new InMemorySymmetricSecurityKey(key),
                "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256",
                "http://www.w3.org/2001/04/xmlenc#sha256"
            );
            _expiration = expiration;
        }

        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_expiration.TotalMinutes);
            var token = new JwtSecurityToken(_issuer, _audience, data.Identity.Claims, now, expires, _signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
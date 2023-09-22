using System.IdentityModel.Tokens.Jwt;

namespace Platform.Certificate.API.Common.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetCurrentUserIdFromToken(this HttpContext context)
        {
            var auth = context.Request.Headers["Authorization"].ToString();
            var token = auth.Split(' ')[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var readToken = tokenHandler.ReadJwtToken(token);
            return Convert.ToInt32(readToken.Payload["sid"].ToString());
        }
    }
}
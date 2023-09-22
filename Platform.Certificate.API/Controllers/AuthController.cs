using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Platform.Certificate.API.Common.Helpers;
using Platform.Certificate.API.Models.Dtos.User;
using Platform.Certificate.API.Models.Forms.User;
using Platform.Certificate.API.Services.Interfaces;

namespace Platform.Certificate.API.Controllers
{
    [ApiController]
    [EnableCors(Constants.AllowOrigin)]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _service;

        public AuthController(IUserService service)
        {
            _service = service;
        }

        [Route(template: "[controller]/[action]"),
         AllowAnonymous, HttpPost,
         ProducesResponseType(type: typeof(ClientResponse<LoginResponseDto>), statusCode: 200),
         ProducesResponseType(type: typeof(ClientResponse<string>), statusCode: 400),
         ProducesResponseType(type: typeof(ClientResponse<string>), statusCode: 401)]
        public async Task<IActionResult> Token([FromBody] LoginForm form)
        {
            var serviceResponse = await _service.Login(form);
            if (serviceResponse.Failed)
            {
                return Unauthorized(new ClientResponse<string>(true, serviceResponse.MessageWithErrors));
            }

            var user = serviceResponse.Data;

            var claims = new List<Claim>
            {
                new(type: JwtRegisteredClaimNames.Sid, value: user.Id.ToString()),
                new(type: JwtRegisteredClaimNames.NameId, value: user.Username),
                new(type: JwtRegisteredClaimNames.GivenName, value: user.Fullname),
                new(ClaimTypes.Role, user.Role.ToString()),
                new(type: JwtRegisteredClaimNames.Jti, value: Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.UtcNow.AddDays(value: 3),
                notBefore: DateTime.UtcNow,
                audience: "Audience",
                issuer: "Issuer",
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding
                        .UTF8
                        .GetBytes("wwBI_nancf3UoOYQ_jDOoSQRrFL1SR8fbQS-Rsdu1")),
                    SecurityAlgorithms.HmacSha256)
            );
            var loginResponseDto = new LoginResponseDto()
            {
                Id = user.Id,
                Token = new JwtSecurityTokenHandler().WriteToken(token: token),
                Username = user.Username,
                Fullname = user.Fullname,
                Role = user.Role
            };
            return Ok(loginResponseDto);
        }
    }
}
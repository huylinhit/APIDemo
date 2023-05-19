using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace APIDemo.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public class AuthenticationRequestBody
        {
            public string? UserName { get; set; }
            public string? Password { get; set; }
        }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }


        [HttpPost("authenticate")]
        public ActionResult<string> Authentication(
            AuthenticationRequestBody authenticationRequestBody)
        {
            var user = ValidateUserCredential(
                authenticationRequestBody.UserName,
                authenticationRequestBody.Password
                );

            if (user == null)
            {
                return Unauthorized();
            }

            //create token
            var securityToken = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );

            var signingCredential = new SigningCredentials( 
                securityToken,
                SecurityAlgorithms.HmacSha256);

            var claimsList = new List<Claim>();
            claimsList.Add(new Claim("sub", user.UserId.ToString()));
            claimsList.Add(new Claim("given_name", user.UserName));
            claimsList.Add(new Claim("last_name", user.LastName));
            claimsList.Add(new Claim("city", user.City));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsList,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredential
                );

            var tokenToReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        private class CityInfo
        {
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string City { get; set; }

            public CityInfo(int userId, string userName, string firstName, string lastName, string city)
            {
                UserId = userId;
                UserName = userName;
                FirstName = firstName;
                LastName = lastName;
                City = city;
            }

        }

        private CityInfo ValidateUserCredential(string? userName, string? password)
        {
            return new CityInfo(
                1,
                userName ?? "",
                "Kevin",
                "Dockx",
                "Ha Noi"
                );
        }
    }
}

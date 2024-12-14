using Microsoft.IdentityModel.Tokens;
using SSOAuthentication.Model;
using SSOAuthentication.Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace SSOAuthentication.Services
{
    public class UserServices
    {
        IConfiguration _configuration = new ConfigurationBuilder()
          .SetBasePath(Directory.GetCurrentDirectory()) // Base path for config file
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
          .Build();
        UserRepository userRepository = new UserRepository();
        LoginConfirmationRepository loginConfirmationRepository = new LoginConfirmationRepository();


        public UserModel Login(UserLogin userLogin)
        {
            var user = userRepository.GetByUsername(userLogin.Username);
            if (user == null) throw new Exception("Invalid");
            if (user.Password != userLogin.Password) throw new Exception("Invalid");

            return user;

        }

        public string GenerateToken(UserModel user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };



            var token = GetToken(authClaims);



            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SSOAuthentication.Model;
using SSOAuthentication.Model.DTOs;
using SSOAuthentication.Repository;
using SSOAuthentication.Services;

namespace SSOApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userService = new UserServices();
        public LoginConfirmationService _loginConfirmationService = new LoginConfirmationService();

        [HttpPost]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            try
            {
                var user = _userService.Login(userLogin);
                var token = _userService.GenerateToken(user);


                var loginData = new LoginConfirmationModel()
                {
                    UserId = user.Id,
                    UniqueToken = Guid.NewGuid(),
                    JwtToken = token,

                };
                var loginconfirmationdata = _loginConfirmationService.Add(loginData);
                return Ok(new
                {
                    Token = token,
                    LoginConfirmationToken = loginconfirmationdata.UniqueToken,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("login-confirmation")]      
         public IActionResult LoginConfirmation([FromBody] LoginConfirmationParamDTO data)
        {
            var user = _loginConfirmationService.ConfirmLogin(data.UniqueToken);
            var token = _userService.GenerateToken(user);

            return Ok(new {Token = token});
        }

        [HttpPost("login-confirmation/user-data")]
        public IActionResult LoginConfirmationUserData([FromBody    ] LoginConfirmationParamDTO data)
        {
            var user = _loginConfirmationService.GetUserData(data.UniqueToken);
            return Ok(new { Username = user.Username });
        }
    }
}

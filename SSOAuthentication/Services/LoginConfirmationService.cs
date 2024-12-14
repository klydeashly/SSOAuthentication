using SSOAuthentication.Model;
using SSOAuthentication.Repository;

namespace SSOAuthentication.Services
{
    public class LoginConfirmationService
    {
        private readonly LoginConfirmationRepository loginConfirmationRepository = new LoginConfirmationRepository();
        private readonly UserRepository userRepository = new UserRepository();
        public LoginConfirmationModel Add(LoginConfirmationModel data)
        {
            return loginConfirmationRepository.Add(data);
            
        }
        public UserModel ConfirmLogin(string uniqueToken)
        {
           var data = loginConfirmationRepository.GetByUniqueToken(uniqueToken);
            data.IsUsed = true;
            loginConfirmationRepository.Update(data);
            var user = userRepository.GetById(data.UserId);

            return user;
        }

        public UserModel GetUserData(string uniqueToken)
        {
            var data = loginConfirmationRepository.GetByUniqueToken(uniqueToken);
            var user = userRepository.GetById(data.UserId);

            return user;
        }
    }
}

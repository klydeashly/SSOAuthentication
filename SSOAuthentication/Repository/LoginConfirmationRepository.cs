using Dapper;
using SSOAuthentication.Model;

namespace SSOAuthentication.Repository
{
    public class LoginConfirmationRepository : GenericRepository<LoginConfirmationModel>
    {
        public LoginConfirmationModel? GetByUniqueToken(string uniqueToken)
        {
            var query = $"SELECT * FROM tblLoginConfirmation WHERE UniqueToken = @UniqueToken";

            return _connection.QueryFirstOrDefault<LoginConfirmationModel>(query, new {UniqueToken = uniqueToken});
        }
    }
}

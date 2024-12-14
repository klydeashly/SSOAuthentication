using Dapper;
using SSOAuthentication.Model;

namespace SSOAuthentication.Repository
{
    public class UserRepository : GenericRepository<UserModel>
    {
        public UserModel GetByUsername(string username)
        {
            var query = "SELECT * FROM tblUser WHERE Username = @Username";
            return _connection.QueryFirstOrDefault<UserModel>(query, new { Username = username });
        }
    }
}

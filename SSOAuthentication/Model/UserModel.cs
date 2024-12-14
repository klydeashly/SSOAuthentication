using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSOAuthentication.Model
{
        [Table("tblUser")]
        public class UserModel
        {
            [Key]
            [Column("Id")]
            public int Id { get; set; }
            [Column("Username")]
            public string Username { get; set; }
            [Column("Password")]
            public string Password { get; set; }
        }

        public class UserLogin
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
}

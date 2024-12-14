using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SSOAuthentication.Model
{
    [Table("tblLoginConfirmation")]
    public class LoginConfirmationModel
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        [Column("UniqueToken")]
        public Guid UniqueToken { get; set; }
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("IsUsed")]
        public bool IsUsed {get; set;}
        [Column("JwtToken")]
        public string JwtToken { get; set; }
    }  
}

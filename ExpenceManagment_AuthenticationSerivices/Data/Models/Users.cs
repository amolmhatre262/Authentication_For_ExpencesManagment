using ExpenceManagment_AuthenticationSerivices.Data.Models.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ExpenceManagment_AuthenticationSerivices.Data.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [NotAdminAttributes(ErrorMessage = "Username cannot be 'admin'")]
        public string UserName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [MinLength(10)]
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        

    }
}

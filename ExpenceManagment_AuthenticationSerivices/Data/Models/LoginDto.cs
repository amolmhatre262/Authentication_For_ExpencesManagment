namespace ExpenceManagment_AuthenticationSerivices.Data.Models
{
    public class LoginDto
    {
        public string UserName { get; set; }

        public string PasswordHash { get; set; }
    }
}

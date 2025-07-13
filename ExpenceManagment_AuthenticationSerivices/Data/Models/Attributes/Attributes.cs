using System.ComponentModel.DataAnnotations;

namespace ExpenceManagment_AuthenticationSerivices.Data.Models.Attributes
{
    public class NotAdminAttributes : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return value?.ToString().ToLower() != "admin";
        }
    }
}

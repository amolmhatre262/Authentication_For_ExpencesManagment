using ExpenceManagment_AuthenticationSerivices.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit_Expencemanagment_Authentication.test.TestData
{
    public class UserTestData
    {
        public static List<Users> GetAllUsers() => new List<Users>
        {
            new Users { UserID = 1, UserName = "Alice", PasswordHash = "123", Email = "alice@example.com" },
            new Users { UserID = 2, UserName = "Bob", PasswordHash = "456", Email = "bob@example.com" }
        };
    }
}

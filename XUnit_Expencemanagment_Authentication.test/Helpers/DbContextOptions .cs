using ExpenceManagment_AuthenticationSerivices.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit_Expencemanagment_Authentication.test.Helpers
{
    public class DbContextOptions
    {
        public static DbContextOptions<ApplicationDBContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }
    }
}

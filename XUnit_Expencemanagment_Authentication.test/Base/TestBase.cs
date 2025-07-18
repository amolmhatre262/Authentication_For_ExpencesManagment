using ExpenceManagment_AuthenticationSerivices.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit_Expencemanagment_Authentication.test.Base
{
    public abstract class TestBase
    {
        protected DbContextOptions<ApplicationDBContext> ContextOptions { get; }

        protected TestBase()
        {
            //ContextOptions = DbContextOptions.GetInMemoryOptions();
        }


    }
}

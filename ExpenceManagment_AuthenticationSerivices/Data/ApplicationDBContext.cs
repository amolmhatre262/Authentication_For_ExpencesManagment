using ExpenceManagment_AuthenticationSerivices.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace ExpenceManagment_AuthenticationSerivices.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        public DbSet<Users> Users { get; set; } // DbSet represents a table

    }
}

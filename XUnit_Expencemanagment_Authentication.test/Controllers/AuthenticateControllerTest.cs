using ExpenceManagment_AuthenticationSerivices.Controllers;
using ExpenceManagment_AuthenticationSerivices.Data;
using ExpenceManagment_AuthenticationSerivices.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XUnit_Expencemanagment_Authentication.test.Controllers
{
    public class AuthenticateControllerTest
    {
        [Fact]
        public void AuthenticateController_SampleUnitTest_Validresult()
        {
            //AAA

            //Arrange
            AuthenticateController controller = new AuthenticateController(null);
            string ExpectedResult = "This is a sample unit test method.";

            // Act
            string actualResult = controller.SampleUnitTest();

            // Assert
            Assert.Equal(ExpectedResult, actualResult);
        }

        private DbContextOptions<ApplicationDBContext> GetInMemoryOptions()
        {
            return new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_" + System.Guid.NewGuid())
                .Options;
        }

        [Fact]
        public async Task GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var options = GetInMemoryOptions();

            var testUsers = new List<Users>
            {
                new Users { UserID = 1, UserName = "Alice", PasswordHash = "pass1", Email = "alice@example.com" },
                new Users { UserID = 2, UserName = "Bob", PasswordHash = "pass2", Email = "bob@example.com" }
            };

            using (var context = new ApplicationDBContext(options))
            {
                context.Users.AddRange(testUsers);
                context.SaveChanges();
            }

            using (var context = new ApplicationDBContext(options))
            {
                var controller = new AuthenticateController(context);

                // Act
                var result = await controller.GetAllUsers();

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result.Result);
                var returnedUsers = Assert.IsAssignableFrom<IEnumerable<Users>>(okResult.Value);

                Assert.Equal(2, ((List<Users>)returnedUsers).Count);
            }
        }
    }
}

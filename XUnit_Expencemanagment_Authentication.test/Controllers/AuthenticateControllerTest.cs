using ExpenceManagment_AuthenticationSerivices.Controllers;
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
    }
}

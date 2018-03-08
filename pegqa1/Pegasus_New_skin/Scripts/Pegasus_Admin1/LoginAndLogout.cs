using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.LoginTests
{
    [TestClass]
    public class LoginAndLogout : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void LoginWithoutCredentials()
        {
            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());

            // Login with blank username and password
            Login("", "");
            Console.WriteLine("Logged in as: " + "" + " / " + "");

            // Verify Page title
            VerifyTitle("Login");

            // Verify validation message
            loginHelper.verifyErrorMessages("This field is required.");

            // Capture screenshot fot the screen
            TakeScreenshot("LoginWithoutCredentials");
        }

        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void loginWithInvalidPassword()
        {
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            var username = oXMLData.getData("settings/Credentials", "username_office");

            // Initializing the objects
            var loginHelper = new LoginHelper(GetWebDriver());

            // Login with blank username and password
            Login(username[0], "123456");
            Console.WriteLine("Logged in as: " + username[0] + " / " + "123456");

            // Verify Page title
            VerifyTitle("Login");

            // Verify validation messages
            loginHelper.verifyErrorMessages("This field is required.");

            // Capture screenshot for the screen
            TakeScreenshot("loginWithInvalidPassword");
        }

        [TestMethod]
        [TestCategory("All")]
        public void loginWithValidCredentials()
        {
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            var username = oXMLData.getData("settings/Credentials", "username_office");
            var password = oXMLData.getData("settings/Credentials", "password");

            // Login with blank username and password
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            // Verify Page title
            VerifyTitle("Dashboard");

            // Capture screenshot fot the screen
            TakeScreenshot("loginWithValidCredentials");
        }
    }
}
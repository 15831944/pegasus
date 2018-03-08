using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class GlobalSearchBar : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTest")]

        public void globalSearchBar()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_main_helper = new OfficeMainHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            //try
            //{

                executionLog.Log("GlobalSearchBar", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("GlobalSearchBar", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("GlobalSearchBar", "Enter any existing opportunity name");
                office_main_helper.TypeText("GlobalSearchBar", "Test");
                Console.WriteLine("Searched in Global Search Bar.");

                office_main_helper.WaitForWorkAround(6000);
            

            //}
            //catch (Exception e){
               
            //}
        }
    }
}

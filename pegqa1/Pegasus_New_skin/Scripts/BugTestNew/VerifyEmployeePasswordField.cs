using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyEmployeePasswordField : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyEmployeePasswordField()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("VerifyEmployeePasswordField", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyEmployeePasswordField", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyEmployeePasswordField", "Go to Employee page");
                VisitOffice("employees");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Click on Create page");
                agents_EmployeesHelper.ClickElement("Create");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAdvanceFilterIssue", "Deselect Auto Generate Password Check Box");
                agents_EmployeesHelper.ClickElement("AutoGenPswdChkBox");
                agents_EmployeesHelper.WaitForWorkAround(1000);

                executionLog.Log("EmployeeAdvanceFilterIssue", "Deselect Auto Generate Password Check Box");
                agents_EmployeesHelper.TypeText("Password", "123456789");
                Console.WriteLine("User is able to enter in Password field when Auto Gen Password is deselected");
               

            //}
            //catch (Exception e)
            //{
               
            //}
            
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeStateIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void employeeStateIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());
            String Status = "Pass";
            String JIRA = "";

            try
            {

                executionLog.Log("EmployeeStateIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("EmployeeStateIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EmployeeStateIssue", "Go to Create Employee page");
                VisitCorp("employees/create");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeStateIssue", "Verify title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeStateIssue", "Select country");
                corp_EmployeeHelper.SelectByText("Country", "Canada");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("EmployeeStateIssue", "Verify State available");
                Assert.IsTrue(corp_EmployeeHelper.IsElementPresent("//*[@id='CorporateUserAddress0State']/option[2]"));

                executionLog.Log("EmployeeStateIssue", "Select country");
                corp_EmployeeHelper.SelectByText("Country", "United States");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("EmployeeStateIssue", "Verify State available");
                Assert.IsTrue(corp_EmployeeHelper.IsElementPresent("//*[@id='CorporateUserAddress0State']/option[2]"));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeStateIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee State Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee State Issue", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee State Issue");
                        TakeScreenshot("EmployeeStateIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeStateIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeStateIssue");
                        string id = loginHelper.getIssueID("Employee State Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeStateIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee State Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee State Issue");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeStateIssue");
                executionLog.WriteInExcel("Employee State Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}

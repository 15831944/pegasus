using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyResultsPerPageUsers : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyResultsPerPageUsers()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_UserHelper = new Office_UserHelper(GetWebDriver());

            try
            {
                executionLog.Log("VerifyResultsPerPageUsers", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyResultsPerPageUsers", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyResultsPerPageUsers", "Go to User page");
                VisitOffice("users");
                office_UserHelper.WaitForWorkAround(6000);

                executionLog.Log("VerifyResultsPerPageUsers", "Verify title");
                VerifyTitle("'s Users");

                executionLog.Log("VerifyResultsPerPageUsers", "Click on Advance filter");
                office_UserHelper.ClickElement("AdvanceFilter");
                office_UserHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyResultsPerPageUsers", "Select results per page 50");
                office_UserHelper.Select("ResultsPerPage", "50");

                executionLog.Log("VerifyResultsPerPageUsers", "Click on apply button.");
                office_UserHelper.ClickElement("Apply");
                office_UserHelper.WaitForWorkAround(20000);

                executionLog.Log("VerifyResultsPerPageUsers", "Verify no. of results 50 on page");
                office_UserHelper.verifyCount(50, 50);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyResultsPerPageUsers");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Results Per Page Users");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Results Per Page Users", "Bug", "Medium", "Office Users page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Results Per Page Users");
                        TakeScreenshot("VerifyResultsPerPageUsers");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyResultsPerPageUsers.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyResultsPerPageUsers");
                        string id = loginHelper.getIssueID("Verify Results Per Page Users");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyResultsPerPageUsers.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Results Per Page Users"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Results Per Page Users");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyResultsPerPageUsers");
                executionLog.WriteInExcel("Verify Results Per Page Users", Status, JIRA, "Corp Offices");
            }
        }
    }
}
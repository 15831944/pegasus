using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TaskResultsPerPageIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void taskResultsPerPageIssue()
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
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            //try
            //{
                executionLog.Log("TaskResultsPerPageIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TaskResultsPerPageIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TaskResultsPerPageIssue", "Redirect To URL");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Verify page title.");
                VerifyTitle("Tasks");

                executionLog.Log("TaskResultsPerPageIssue", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Select number of records to 10.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "10");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Wait for locator to be present.");
                officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TaskResultsPerPageIssue", "Verify number of records displayed.");
                officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Select number of records to 20.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "20");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Wait for locator to be present.");
                officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TaskResultsPerPageIssue", "Verify number of records displayed.");
                //officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                officeActivities_TasksHelper.ShowResult(10);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Select number of records to 50.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "50");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Wait for locator to be present.");
                officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TaskResultsPerPageIssue", "Verify number of records displayed.");
                // officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                officeActivities_TasksHelper.ShowResult(50);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Select number of records to 100.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "100");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskResultsPerPageIssue", "Wait for locator to be present.");
                officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TaskResultsPerPageIssue", "Verify number of records displayed.");
                //officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                officeActivities_TasksHelper.ShowResult(100);
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskResultsPerPageIssue", "Logout from the application.");
                VisitOffice("logout");

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("TaskResultsPerPageIssue");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Task Results Per Page Issue");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Task Results Per Page Issue", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Task Results Per Page Issue");
            //            TakeScreenshot("TaskResultsPerPageIssue");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\TaskResultsPerPageIssue.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("TaskResultsPerPageIssue");
            //            string id = loginHelper.getIssueID("Task Results Per Page Issue");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\TaskResultsPerPageIssue.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Task Results Per Page Issue"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Task Results Per Page Issue");
            //  //  executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("TaskResultsPerPageIssue");
            //    executionLog.WriteInExcel("Task Results Per Page Issue", Status, JIRA, "Opportunities Management");
            //}
        }
    }
}
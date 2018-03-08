using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyModifiedMovementIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyModifiedMovementIssue()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyModifiedMovementIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyModifiedMovementIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyModifiedMovementIssue", "Redirect To URL");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyModifiedMovementIssue", "Verify page title.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyModifiedMovementIssue", "Click on advance filter.");
                office_LeadsHelper.ClickElement("AdvanceFilter");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedMovementIssue", "Select 'Modified' in displayed columns.");
                office_LeadsHelper.SelectByText("DisplayedCols", "Modified");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedMovementIssue", "Move column to available columns");
                office_LeadsHelper.ClickElement("RemoveCols");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedMovementIssue", "Click on apply button.");
                office_LeadsHelper.ClickElement("Apply");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedMovementIssue", "Verify unexpected error message not present on the page.");
                office_LeadsHelper.VerifyTextNot("OOPS you are trying to access a non existing page on the website.");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedMovementIssue", "Logout from the application.");
                VisitOffice("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyModifiedMovementIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Modified Movement Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Modified Movement Issue", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Modified Movement Issue");
                        TakeScreenshot("VerifyModifiedMovementIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedMovementIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyModifiedMovementIssue");
                        string id = loginHelper.getIssueID("Verify Modified Movement Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedMovementIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Modified Movement Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Modified Movement Issue");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyModifiedMovementIssue");
                executionLog.WriteInExcel("Verify Modified Movement Issue", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
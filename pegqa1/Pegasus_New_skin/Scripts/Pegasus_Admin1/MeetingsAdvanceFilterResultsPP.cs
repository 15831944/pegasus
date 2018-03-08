using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MeetingsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void meetingsAdvanceFilterResultsPP()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Select number of records to 10.");
                officeActivities_MeetingHelper.SelectByText("ResultsPerPage", "10");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_MeetingHelper.ShowResult(10);
                //officeActivities_MeetingHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Select number of records to 20.");
                officeActivities_MeetingHelper.SelectByText("ResultsPerPage", "20");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_MeetingHelper.ShowResult(20);
                // officeActivities_MeetingHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Select number of records to 50.");
                officeActivities_MeetingHelper.SelectByText("ResultsPerPage", "50");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_MeetingHelper.ShowResult(50);
                // officeActivities_MeetingHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Select number of records to 100.");
                officeActivities_MeetingHelper.SelectByText("ResultsPerPage", "100");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_MeetingHelper.ShowResult(100);
                //officeActivities_MeetingHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Meetings Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Meetings Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meetings Advance Filter ResultsPP");
                        TakeScreenshot("MeetingsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Meetings Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meetings Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meetings Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Meetings Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
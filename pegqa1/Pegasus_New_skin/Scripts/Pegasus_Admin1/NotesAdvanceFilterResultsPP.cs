using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class NotesAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void notesAdvanceFilterResultsPP()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("NotesAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("NotesAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Select number of records to 10.");
                officeActivities_NotesHelper.SelectByText("ResultsPerPage", "10");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);
                //officeActivities_NotesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_NotesHelper.ShowResult(10);
                // officeActivities_NotesHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Select number of records to 20.");
                officeActivities_NotesHelper.SelectByText("ResultsPerPage", "20");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);
                //officeActivities_NotesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_NotesHelper.ShowResult(20);
                // officeActivities_NotesHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Select number of records to 50.");
                officeActivities_NotesHelper.SelectByText("ResultsPerPage", "50");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);
                //officeActivities_NotesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_NotesHelper.ShowResult(50);
                //  officeActivities_NotesHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Select number of records to 100.");
                officeActivities_NotesHelper.SelectByText("ResultsPerPage", "100");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);
                //officeActivities_NotesHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_NotesHelper.ShowResult(100);
                //officeActivities_NotesHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NotesAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("NotesAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Notes Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Notes Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Notes Advance Filter ResultsPP");
                        TakeScreenshot("NotesAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NotesAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("NotesAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Notes Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NotesAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Notes Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Notes Advance Filter ResultsPP");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("NotesAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Notes Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
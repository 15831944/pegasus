using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyNotesAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyNotesAdvanceFilerColumnOrder()
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

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify subject column is visible on the page..");
                officeActivities_NotesHelper.IsElementPresent("HeadSubject");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify related to column is visible on the page.");
                officeActivities_NotesHelper.IsElementPresent("HeadRelatedTo");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify created column is visible on the page.");
                officeActivities_NotesHelper.IsElementPresent("HeadCreated");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Select subject in displayed columns.");
                officeActivities_NotesHelper.SelectByText("DisplayedCols", "Subject");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeActivities_NotesHelper.ClickElement("RemoveCols");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Select related to in displayed columns.");
                officeActivities_NotesHelper.SelectByText("DisplayedCols", "Related To");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_NotesHelper.ClickElement("RemoveCols");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Select created in displayed columns.");
                officeActivities_NotesHelper.SelectByText("DisplayedCols", "Created");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_NotesHelper.ClickElement("RemoveCols");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify subject not present on page.");
                officeActivities_NotesHelper.IsElementNotPresent("HeadSubject");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify related to not present on page.");
                officeActivities_NotesHelper.IsElementNotPresent("HeadRelatedTo");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify created not present on page.");
                officeActivities_NotesHelper.IsElementNotPresent("HeadCreated");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Redirect at tasks page.");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify page title as notes");
                VerifyTitle("Notes");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify default position of related to column.");
                officeActivities_NotesHelper.IsElementPresent("HeadRelatedTO2");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify default position of created by column.");
                officeActivities_NotesHelper.IsElementPresent("HeadCreatedBy3");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Redirect at tasks page.");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_NotesHelper.ClickElement("AdvanceFilter");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Select related to in displayed column.");
                officeActivities_NotesHelper.SelectByText("DisplayedCols", "Related To");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Move related to 1 step up.");
                officeActivities_NotesHelper.ClickElement("MoveUp");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Select created by in displayed column.");
                officeActivities_NotesHelper.SelectByText("DisplayedCols", "Created By");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Move created by 1 step down.");
                officeActivities_NotesHelper.ClickElement("MoveDown");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_NotesHelper.ClickElement("Apply");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify changed position of related to column.");
                officeActivities_NotesHelper.IsElementPresent("HeadRelatedTO1");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Verify changed position of created by column.");
                officeActivities_NotesHelper.IsElementPresent("HeadCreatedBy4");
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNotesAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tasks Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tasks Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
                        TakeScreenshot("VerifyNotesAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNotesAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tasks Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyNotesAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Tasks Advance Filer Column Order", Status, JIRA, "Notes Management");
            }
        }
    }
}
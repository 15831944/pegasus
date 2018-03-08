using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteMeetingFromRecycleBin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void deleteMeetingFromRecycleBin()
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

            //Random Variables
            var name = "Meeting" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DeleteMeetingFromRecycleBin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Click to open client info");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Verify page title");
                VerifyTitle("Create a Meeting");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Enter Subject meeting");
                officeActivities_MeetingHelper.TypeText("Subject", name);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Enter location of rhe meeting.");
                officeActivities_MeetingHelper.TypeText("Location", "Location");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Enter start date meeting.");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2018");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Enter end date meeting.");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2018");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Click On Assigned To");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Clcik on Client You Want To select");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Wait for element to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("DeleteMeetingFromRecycleBin", "verify page text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Redirect To Meeting");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Search Meeting");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Click On Meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Cancel Meeting");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("DeleteMeetingFromRecycleBin", "Wait for success message.");
                officeActivities_MeetingHelper.WaitForText("Meeting successfully deleted.", 10);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Go to Recycle bin");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Search Meeting By Subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DeleteMeetingFromRecycleBin", "Click On Delete Icon");
                officeActivities_MeetingHelper.ClickElement("ClickOnDeleteIcon");

                executionLog.Log("DeleteMeetingFromRecycleBin", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("DeleteMeetingFromRecycleBin", "Wait for success message.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteMeetingFromRecycleBin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Meeting From Recycle Bin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("DeleteMeetingFromRecycleBin", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Meeting From Recycle Bin");
                        TakeScreenshot("DeleteMeetingFromRecycleBin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteMeetingFromRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteMeetingFromRecycleBin");
                        string id = loginHelper.getIssueID("Delete Meeting From Recycle Bin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteMeetingFromRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Meeting From Recycle Bin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Meeting From Recycle Bin");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteMeetingFromRecycleBin");
                executionLog.WriteInExcel("Delete Meeting From Recycle Bin", Status, JIRA, "Office Activities");
            }
        }
    }
}
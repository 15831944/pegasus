using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateMeetingActivities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void createMeetingActivities()
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

            // Randon Variables.
            var Subject = "Meeting" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateMeetingActivities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateMeetingActivities", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateMeetingActivities", "Go to create meetings page");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMeetingActivities", "Verify page title.");
                VerifyTitle("Create a Meeting");

                executionLog.Log("CreateMeetingActivities", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("CreateMeetingActivities", "Enter location of meeting.");
                officeActivities_MeetingHelper.TypeText("Location", "TESTING MEETING LOCATION");

                executionLog.Log("CreateMeetingActivities", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "07/07/2017");

                executionLog.Log("CreateMeetingActivities", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2017");

                executionLog.Log("CreateMeetingActivities", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("CreateMeetingActivities", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMeetingActivities", "Click on client for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateMeetingActivities", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("CreateMeetingActivities", "verify page text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("CreateMeetingActivities", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMeetingActivities", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMeetingActivities", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMeetingActivities", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

                executionLog.Log("CreateMeetingActivities", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("CreateMeetingActivities", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateMeetingActivities", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("CreateMeetingActivities", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateMeetingActivities", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                //executionLog.Log("CreateMeetingActivities", "Wait for delete icon to be present.");
                //officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("CreateMeetingActivities", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("CreateMeetingActivities", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("CreateMeetingActivities", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateMeetingActivities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Meeting Activities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Meeting Activities", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Meeting Activities");
                        TakeScreenshot("CreateMeetingActivities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMeetingActivities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateMeetingActivities");
                        string id = loginHelper.getIssueID("Create Meeting Activities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateMeetingActivities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Meeting Activities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Meeting Activities");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateMeetingActivities");
                executionLog.WriteInExcel("Create Meeting Activities", Status, JIRA, "Office Activities");
            }
        }
    }
}
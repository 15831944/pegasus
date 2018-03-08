using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesBulkUpdateMeetings : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Test1")]
        public void activitiesBulkUpdateMeetings()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Random Variables.
            var Subject1 = "Meeting2" + RandomNumber(1, 99);
            var Subject = "Meeting" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

         try
           {
            executionLog.Log("ActivitiesBulkUpdateMeetings", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to create meetings page");
            VisitOffice("meetings/create");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Create a Meeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("NameError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("StartDateError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("ParentError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter Subject for the meeting");
            officeActivities_MeetingHelper.TypeText("Subject", Subject);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter location of meeting.");
            officeActivities_MeetingHelper.TypeText("Location", "Test Location");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter start date.");
            officeActivities_MeetingHelper.TypeText("StartDate", "2015-03-28");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter End Date.");
            officeActivities_MeetingHelper.TypeText("EndDate", "2015-03-26");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select Related To");
            officeActivities_MeetingHelper.Select("RelatedTo", "20");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On find list icon");
            officeActivities_MeetingHelper.ClickElement("FindListIcon");
            officeActivities_MeetingHelper.WaitForWorkAround(2000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on client for which meeting is created.");
            officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
            officeActivities_MeetingHelper.WaitForWorkAround(1000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select meeting status");
            officeActivities_MeetingHelper.Select("Status", "Planned");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select meeting priority.");
            officeActivities_MeetingHelper.Select("Priority", "Low");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select assigned owner for meeting.");
            officeActivities_MeetingHelper.SelectByText("AssignedOwner", "Howard Tang");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select assigned user group for meeting.");
            officeActivities_MeetingHelper.Select("AssignedUserGroup", "81");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for dates.");
            officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert by clicking ok.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter start date");
            officeActivities_MeetingHelper.TypeText("StartDate", "2015-03-24");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter End Date.");
            officeActivities_MeetingHelper.TypeText("EndDate", "2015-03-26");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "verify page text");
            officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to create meetings page");
            VisitOffice("meetings/create");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Create a Meeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("NameError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("StartDateError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify validation text for mandatoryness.");
            officeActivities_MeetingHelper.VerifyText("ParentError", "This field is required.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter Subject for the meeting");
            officeActivities_MeetingHelper.TypeText("Subject", Subject1);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter location of meeting.");
            officeActivities_MeetingHelper.TypeText("Location", "Test Location");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter start date.");
            officeActivities_MeetingHelper.TypeText("StartDate", "2015-03-28");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter End Date.");
            officeActivities_MeetingHelper.TypeText("EndDate", "2015-03-26");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select Related To");
            officeActivities_MeetingHelper.Select("RelatedTo", "20");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On find list icon");
            officeActivities_MeetingHelper.ClickElement("FindListIcon");
            officeActivities_MeetingHelper.WaitForWorkAround(2000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on client for which meeting is created.");
            officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
            officeActivities_MeetingHelper.WaitForWorkAround(1000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select status as planned.");
            officeActivities_MeetingHelper.Select("Status", "Planned");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select meeting priority.");
            officeActivities_MeetingHelper.Select("Priority", "Low");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select assigned owner for meeting.");
            officeActivities_MeetingHelper.SelectByText("AssignedOwner", "Howard Tang");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select assigned user group for meeting.");
            officeActivities_MeetingHelper.Select("AssignedUserGroup", "81");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify alert text for dates.");
            officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert by clicking ok.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter start date");
            officeActivities_MeetingHelper.TypeText("StartDate", "2015-03-24");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Enter End Date.");
            officeActivities_MeetingHelper.TypeText("EndDate", "2015-03-26");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Save button");
            officeActivities_MeetingHelper.ClickElement("Save");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for success text");
            officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to meetings page");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Bulk Update.");
            officeActivities_MeetingHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On status");

            officeActivities_MeetingHelper.ClickElement("UpDateStatus");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify alert for selecting records.");
            officeActivities_MeetingHelper.VerifyAlertText("Please select atleast one record to proceed");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select first meeting.");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select second meeting.");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox2");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on bulk update.");
            officeActivities_MeetingHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on status.");
            officeActivities_MeetingHelper.ClickElement("UpDateStatus");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select status to be updated.");
            officeActivities_MeetingHelper.Select("ChangeStatus", "Held");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on update button.");
            officeActivities_MeetingHelper.ClickElement("UpdateButton");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for success text.");
            officeActivities_MeetingHelper.WaitForText("Meeting status updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to meetings page");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select first meeting");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select second meeting.");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox2");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on bulk update.");
            officeActivities_MeetingHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on Owner.");
            officeActivities_MeetingHelper.ClickElement("ChangeOwner");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select Owner to be updated.");
            officeActivities_MeetingHelper.SelectByText("ChangeResponsibility", "Brian Sales Agent");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on update button.");
            officeActivities_MeetingHelper.ClickElement("UpdateOwner");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for success text.");
            officeActivities_MeetingHelper.WaitForText("Meeting owner updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to meetings page");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select first meeting");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select second meeting.");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox2");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on bulk update.");
            officeActivities_MeetingHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on User group.");
            officeActivities_MeetingHelper.ClickElement("ChangeUserGroup");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select user group to be updated.");
            officeActivities_MeetingHelper.Select("SelectUserGroup", "169");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on update button.");
            officeActivities_MeetingHelper.ClickElement("UpdateGroup");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for success text.");
            officeActivities_MeetingHelper.WaitForText("Meeting user group updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Go to meetings page");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title.");
            VerifyTitle("Meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select first meeting");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select second meeting.");
            officeActivities_MeetingHelper.ClickElement("SelectCheckbox2");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on bulk update.");
            officeActivities_MeetingHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on priority.");
            officeActivities_MeetingHelper.ClickElement("ChangePriority");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Select priority to be updated.");
            officeActivities_MeetingHelper.Select("SelectPriority", "High");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on update button.");
            officeActivities_MeetingHelper.ClickElement("UpdatePriority");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for success text.");
            officeActivities_MeetingHelper.WaitForText("Meeting priority updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Redirect at meetings page.");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Search meeting by subject");
            officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
            officeActivities_MeetingHelper.WaitForWorkAround(3000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on the meeting");
            officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Cancel meeting.");
            officeActivities_MeetingHelper.ClickElement("CancelMeeting");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Redirect at recycle bin.");
            VisitOffice("meetings/recyclebin");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title");
            VerifyTitle("Recycled Meeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Search meeting by name.");
            officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for delete icon to be present.");
            officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On delete icon");
            officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify text.");
            officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Redirect at meetings page.");
            VisitOffice("meetings");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Search meeting by subject");
            officeActivities_MeetingHelper.TypeText("SearchSubject", Subject1);
            officeActivities_MeetingHelper.WaitForWorkAround(3000);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click on the meeting");
            officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On Cancel meeting.");
            officeActivities_MeetingHelper.ClickElement("CancelMeeting");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Redirect at recycle bin.");
            VisitOffice("meetings/recyclebin");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify page title");
            VerifyTitle("Recycled Meeting");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Search meeting by name.");
            officeActivities_MeetingHelper.TypeText("SearchSubject", Subject1);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Wait for delete icon to be present.");
            officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Click On delete icon");
            officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Accept alert message.");
            officeActivities_MeetingHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateMeetings", "Verify text.");
            officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);
        }
     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesBulkUpdateMeetings");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Bulk Update Meetings");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Bulk Update Meetings", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Bulk Update Meetings");
                        TakeScreenshot("ActivitiesBulkUpdateMeetings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateMeetings.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesBulkUpdateMeetings");
                        string id = loginHelper.getIssueID("Activities Bulk Update Meetings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateMeetings.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Bulk Update Meetings"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Bulk Update Meetings");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ActivitiesBulkUpdateMeetings");
                executionLog.WriteInExcel("Activities Bulk Update Meetings", Status, JIRA, "Office Activities");
            }
        }
    }
}

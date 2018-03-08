using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesMeetingsManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesMeetingsManagement()
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
            var Subject = "Meeting" + GetRandomNumber();
            var file = GetPathToFile() + "2.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ActivitiesMeetingsManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesMeetingsManagement", "Go to create meetings page");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify page title.");
                VerifyTitle("Create a Meeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("ActivitiesMeetingsManagement", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("StartDateError", "This field is required.");

                executionLog.Log("ActivitiesMeetingsManagement", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("ParentError", "This field is required.");

                executionLog.Log("ActivitiesMeetingsManagement", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("ActivitiesMeetingsManagement", "Enter location of meeting.");
                officeActivities_MeetingHelper.TypeText("Location", "Test Location");

                executionLog.Log("ActivitiesMeetingsManagement", "Enter start date.");
                officeActivities_MeetingHelper.TypeText("StartDate", "12/30/2016");

                executionLog.Log("ActivitiesMeetingsManagement", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "12/22/2016");

                executionLog.Log("ActivitiesMeetingsManagement", "Select Related To");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on client for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("ActivitiesMeetingsManagement", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "12/22/2016");

                executionLog.Log("ActivitiesMeetingsManagement", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "12/30/2016");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "verify page text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on any client.");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Select actitivity type as meetings.");
                office_ClientsHelper.Select("SelectActivityType", "Meetings");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Enter ticket name to be search.");
                office_ClientsHelper.TypeText("ActivitySubject", Subject);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify created meeting present on client page.");
                office_ClientsHelper.IsElementPresent("OpenFirstActivity");

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at meetings page.");
                VisitOffice("meetings");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "select all in  owner fiedld");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify page title.");
                VerifyTitle("Edit Meeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Select meeting parent as lead");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Lead");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on lead for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Edit end Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "1/1/2017");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "Wait for updation success.");
                officeActivities_MeetingHelper.WaitForText("Meeting updated successfully.", 10);

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On any lead.");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Select actitivity type as meetings.");
                office_LeadsHelper.Select("SelectActivityType", "Meetings");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Enter meeting name to be search.");
                office_LeadsHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify created meeting present on leads page.");
                office_LeadsHelper.IsElementPresent("ClickNotes1");

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "select all in  owner fiedld");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify page title.");
                VerifyTitle("Edit Meeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Select parent type to opportunity");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Opportunity");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on opportunity for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "Wait for updation success message.");
                officeActivities_MeetingHelper.WaitForText("Meeting updated successfully.", 10);

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On any opportunity.");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Select actitivity type as meetings");
                office_LeadsHelper.Select("SelectActivityType", "Meetings");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Enter meeting name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", Subject);
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify created opportunity present on opportunity page");
                office_OpportunitiesHelper.IsElementPresent("OpenOpportunity");

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "select all in  owner fiedld");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Verify page title.");
                VerifyTitle("Edit Meeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Select parent type for meeting.");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Ticket");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on ticket for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("ActivitiesMeetingsManagement", "Wait for success message.");
                officeActivities_MeetingHelper.WaitForText("Meeting updated successfully.", 10);

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at tickets page.");
                VisitOffice("tickets");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On any ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("ActivitiesMeetingsManagement", "Select actitivity type as meetings");
                office_LeadsHelper.Select("SelectActivityType", "Meetings");

                executionLog.Log("ActivitiesMeetingsManagement", "Enter ticket name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on ticket to view details");
                ticket_CreateATicketHelper.IsElementPresent("OpenTicket");

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at meetings page.");
                VisitOffice("meetings");

                executionLog.Log("ActivitiesMeetingsManagement", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "select all in  owner fiedld");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesMeetingsManagement", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("ActivitiesMeetingsManagement", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");

                executionLog.Log("ActivitiesMeetingsManagement", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("ActivitiesMeetingsManagement", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "select all in  owner fiedld");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesMeetingsManagement", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("ActivitiesMeetingsManagement", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("ActivitiesMeetingsManagement", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("ActivitiesMeetingsManagement", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesMeetingsManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Meetings Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Meetings Management", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Meetings Management");
                        TakeScreenshot("ActivitiesMeetingsManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesMeetingsManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesMeetingsManagement");
                        string id = loginHelper.getIssueID("Activities Meetings Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesMeetingsManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Meetings Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Meetings Management");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ActivitiesMeetingsManagement");
                executionLog.WriteInExcel("Activities Meetings Management", Status, JIRA, "Office Activities");
            }
        }
    }
}
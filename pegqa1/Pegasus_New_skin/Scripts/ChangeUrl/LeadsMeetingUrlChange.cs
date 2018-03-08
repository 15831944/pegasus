using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsMeetingUrlChange : DriverTestCase
    {
        [TestMethod]

        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void leadsMeetingUrlChange()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable
            var Subject = "Meeting" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsMeetingUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsMeetingUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadsMeetingUrlChange", "Go to all Leads");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Click On Any Lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsMeetingUrlChange", "Click On New Meeting");
                office_LeadsHelper.ClickElement("AddMeeting");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Enter Meeting Subject");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("LeadsMeetingUrlChange", "Enter Start Date");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2018");

                executionLog.Log("LeadsMeetingUrlChange", "Enter Start Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2018");

                executionLog.Log("LeadsMeetingUrlChange", "Click Save");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("LeadsMeetingUrlChange", "Wait for success text.");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully. ", 10);

                executionLog.Log("LeadsMeetingUrlChange", "Select Activity >> Meetings");
                officeActivities_MeetingHelper.Select("SelectActivityType", "Meetings");

                executionLog.Log("LeadsMeetingUrlChange", "Click On Document ");
                officeActivities_MeetingHelper.PressEnter("ClickMeeting1");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Change the url with the url number of another office");
                VisitOffice("meetings/view/1");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Verify Validation");
                officeActivities_MeetingHelper.WaitForText("You don't have privileges to view this Meeting.", 10);

                executionLog.Log("LeadsMeetingUrlChange", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsMeetingUrlChange", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsMeetingUrlChange", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("LeadsMeetingUrlChange", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Verify page title");
                VerifyTitle("Recycled Meeting");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsMeetingUrlChange", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsMeetingUrlChange", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsMeetingUrlChange", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("LeadsMeetingUrlChange", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("LeadsMeetingUrlChange", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("LeadsMeetingUrlChange", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsMeetingUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Meeting Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Meeting Url Change", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Meeting Url Change");
                        TakeScreenshot("LeadsMeetingUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsMeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsMeetingUrlChange");
                        string id = loginHelper.getIssueID("Leads Meeting Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsMeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Meeting Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Meeting Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsMeetingUrlChange");
                executionLog.WriteInExcel("Leads Meeting Url Change", Status, JIRA, "Leads Meeting");
            }
        }
    }
}

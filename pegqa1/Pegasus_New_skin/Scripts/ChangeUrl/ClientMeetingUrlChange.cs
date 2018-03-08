using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientMeetingUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void clientMeetingUrlChange()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());


            // Variable
            var Subject = "Meeting" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientMeetingUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientMeetingUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientMeetingUrlChange", "Go to All Clients");
                VisitOffice("clients");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientMeetingUrlChange", "Click On Any Client");
                office_ClientsHelper.ClickElement("ClickOnAnyClient");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Click On New Meeting");
                office_ClientsHelper.ClickElement("AddMeeting");

                executionLog.Log("ClientMeetingUrlChange", "Enter Meeting Subject");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("ClientMeetingUrlChange", "Enter Start Date");
                officeActivities_MeetingHelper.TypeText("StartDate", "06/06/2019");

                executionLog.Log("ClientMeetingUrlChange", "Enter Start Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "07/07/2019");

                executionLog.Log("ClientMeetingUrlChange", "Click Save");
                officeActivities_MeetingHelper.ClickDisplayed("//button[@title='Save']");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientMeetingUrlChange", "Select Activity >> Meeting");
                officeActivities_MeetingHelper.Select("SelectActivityType", "Meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Select All in created by");
                officeActivities_MeetingHelper.SelectByText("CreatedByfield", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Click on Meeting to open");
                officeActivities_MeetingHelper.PressEnter("OpenMeetingClient");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Change the url with the url number of another office");
                VisitOffice("meetings/view/1");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientMeetingUrlChange", "Verify Validation");
                officeActivities_MeetingHelper.VerifyPageText("You don't have privileges to view this Meeting.");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientMeetingUrlChange", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "select All in owner");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientMeetingUrlChange", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientMeetingUrlChange", "Verify page title");
                VerifyTitle("Recycled Meetings");

                executionLog.Log("ClientMeetingUrlChange", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "select All in owner");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMeetingUrlChange", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("ClientMeetingUrlChange", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("ClientMeetingUrlChange", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("ClientMeetingUrlChange", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientMeetingUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Meeting Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Meeting Url Change", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Meeting Url Change");
                        TakeScreenshot("ClientMeetingUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientMeetingUrlChange");
                        string id = loginHelper.getIssueID("Client Meeting Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Meeting Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Meeting Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientMeetingUrlChange");
                executionLog.WriteInExcel("Client Meeting Url Change", Status, JIRA, "Client Management");
            }
        }
    }
}
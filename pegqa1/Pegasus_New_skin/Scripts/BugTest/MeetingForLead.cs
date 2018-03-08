using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MeetingForLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void meetingForLead()
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

            // Random Variables
            var Subject = "Meeting" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingForLead", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingForLead", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MeetingForLead", "Redirect to Meeting");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingDateValidation", "Verify page title.");
                VerifyTitle("Create a Meeting");

                executionLog.Log("MeetingForLead", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("MeetingForLead", "Enter Subject for the meeeting");
                officeActivities_MeetingHelper.TypeText("Location", "TESTING MEETING LOCATION");

                executionLog.Log("MeetingForLead", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "05/05/2018");

                executionLog.Log("MeetingForLead", "Enter End Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "06/06/2018");

                executionLog.Log("MeetingForLead", "Select Related ");
                officeActivities_MeetingHelper.SelectDropDownByText("//*[@id='MeetingParentType']", "Lead");

                executionLog.Log("MeetingForLead", "Click On Assigned To");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "SelectLeadForMeeting");
                officeActivities_MeetingHelper.ClickElement("SelectLedForMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingForLead", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingForLead", "verify page text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Select all in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("MeetingForLead", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("MeetingForLead", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingForLead", "Select all in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                //executionLog.Log("MeetingForLead", "Wait for delete icon to be present.");
                //officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("MeetingForLead", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("MeetingForLead", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("MeetingForLead", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingForLead");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Meeting For Lead");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Meeting For Lead", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meeting For Lead");
                        TakeScreenshot("MeetingForLead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingForLead.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingForLead");
                        string id = loginHelper.getIssueID("Meeting For Lead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingForLead.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meeting For Lead"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meeting For Lead");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingForLead");
                executionLog.WriteInExcel("Meeting For Lead", Status, JIRA, "Office Actitvities");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyMeetingCreatedModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyMeetingCreatedModifiedByCredits()
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

            // Random Variables.
            var Subject = "Meeting" + GetRandomNumber();
            var file = GetPathToFile() + "2.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Go to create meetings page");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify page title.");
                VerifyTitle("Create a Meeting");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("StartDateError", "This field is required.");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("ParentError", "This field is required.");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", Subject);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter location of meeting.");
                officeActivities_MeetingHelper.TypeText("Location", "Test Location");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter start date.");
                officeActivities_MeetingHelper.TypeText("StartDate", "09/09/2016");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2016");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click on client for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2016");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2016");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "verify page text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 5);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("meetings");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Wait for locator to present.");
                officeActivities_MeetingHelper.WaitForElementPresent("SearchSubject", 5);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select ALL in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify created By credits");
                officeActivities_MeetingHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify modified By credits");
                officeActivities_MeetingHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Wait for locator to present.");
                officeActivities_MeetingHelper.WaitForElementPresent("SearchSubject", 5);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select ALL in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify page title.");
                VerifyTitle("Edit Meeting");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select meeting parent as lead");
                officeActivities_MeetingHelper.Select("RelatedTo", "14");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click on lead for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Edit end Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "12/22/2017");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Wait for updation success.");
                officeActivities_MeetingHelper.WaitForText("Meeting updated successfully.", 10);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select ALL in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify created By credits");
                officeActivities_MeetingHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify modified By credits");
                officeActivities_MeetingHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Subject);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Select ALL in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("VerifyMeetingCreatedModifiedByCredits", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMeetingCreatedModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Meeting Created Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Meeting Created Modified By Credits", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Meeting Created Modified By Credits");
                        TakeScreenshot("VerifyMeetingCreatedModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingCreatedModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMeetingCreatedModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Meeting Created Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingCreatedModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Meeting Created Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Meeting Created Modified By Credits");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMeetingCreatedModifiedByCredits");
                executionLog.WriteInExcel("Verify Meeting Created Modified By Credits", Status, JIRA, "Office Activities");
            }
        }
    }
}
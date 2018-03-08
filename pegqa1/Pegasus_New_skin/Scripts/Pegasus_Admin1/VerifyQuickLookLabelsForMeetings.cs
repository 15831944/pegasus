using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForMeetings : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForMeetings()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Meeting" + RandomNumber(99, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify Page title");
                VerifyTitle("Dashboard");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Go to create meetings page");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify page title.");
                VerifyTitle("Create a Meeting");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("StartDateError", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify validation text for mandatoryness.");
                officeActivities_MeetingHelper.VerifyText("ParentError", "This field is required.");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", name);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter location of meeting.");
                officeActivities_MeetingHelper.TypeText("Location", "Test Location");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter start date.");
                officeActivities_MeetingHelper.TypeText("StartDate", "09/09/2016");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2016");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On find list icon");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click on client for which meeting is created.");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Save button");
                officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Save button");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "07/07/2016");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter End Date.");
                officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2016");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Wait for success text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter meeting name to search.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Select 'All' in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click on any meeting .");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting status.");
                officeActivities_MeetingHelper.VerifyText("VerifyStatus", "Held");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting  priority.");
                officeActivities_MeetingHelper.VerifyText("VerifyPriority", "Medium");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting category.");
                officeActivities_MeetingHelper.VerifyText("VerifyCategory", "Select Category");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting responsibility.");
                officeActivities_MeetingHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click on edit button.");
                officeActivities_MeetingHelper.ClickElement("EditLink");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for contact type.");
                officeActivities_MeetingHelper.SelectByText("Status", "New");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for contact Status.");
                officeActivities_MeetingHelper.SelectByText("Priority", "High");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for contact source.");
                officeActivities_MeetingHelper.SelectByText("SelectCategory", "Personal");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for contact category.");
                officeActivities_MeetingHelper.SelectByText("AssignedOwner", "Howard Tang");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click on save button.");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Wait for success text.");
                officeActivities_MeetingHelper.WaitForText("Meeting updated successfully.", 10);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Enter meeting name to search.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Select 'All' in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click on any meeting .");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting status.");
                officeActivities_MeetingHelper.VerifyText("VerifyStatus", "New");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting  priority.");
                officeActivities_MeetingHelper.VerifyText("VerifyPriority", "High");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting category.");
                officeActivities_MeetingHelper.VerifyText("VerifyCategory", "Personal");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify label for meeting responsibility.");
                officeActivities_MeetingHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify page title");
                VerifyTitle("Recycled Meeting");
                //officeActivities_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Select 'All' in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                //executionLog.Log("VerifyQuickLookLabelsForMeetings", "Wait for delete icon to be present.");
                //officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();
               // officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForMeetings", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForMeetings");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForMeetings");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyQuickLookLabelsForMeetings", "Bug", "Medium", "Meetings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForMeetings");
                        TakeScreenshot("VerifyQuickLookLabelsForMeetings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForMeetings");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForMeetings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForMeetings"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForMeetings");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForMeetings");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForMeetings", Status, JIRA, "Activities Management");
            }
        }
    }
}


using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyMeetingsCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyMeetingsCreditsIssue()
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

            var Name = "Meeting" + RandomNumber(1, 500);
            // Variable
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyMeetingsCreditsIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyMeetingsCreditsIssue", "Goto User Activities >> Meeting");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Click on Create button");
                officeActivities_MeetingHelper.ClickElement("CreateMeetingBtn");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Enter the subjecr Name");
                officeActivities_MeetingHelper.TypeText("Subject", Name);
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Enter the Start Date");
                officeActivities_MeetingHelper.TypeText("StartDate", "06/06/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Enter the End Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "07/07/2018");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Select related to");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Click on Select button");
                officeActivities_MeetingHelper.ClickElement("SelectBtn");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Select the client");
                officeActivities_MeetingHelper.ClickElement("SelectClient");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Click on Save button");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Search the meeting");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Name);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Click On any Meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Verify meeting created by credits");
                officeActivities_MeetingHelper.VerifyText("CreatedBy", "Howard Tang");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Verify Meeting Modified by credits");
                officeActivities_MeetingHelper.VerifyText("ModifiedBy", "Howard Tang");
                //officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsCreditsIssue", "Click on Cancel button");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMeetingsCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Meetings Credits Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Meetings Credits Issue", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Meetings Credits Issue");
                        TakeScreenshot("VerifyMeetingsCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingsCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMeetingsCreditsIssue");
                        string id = loginHelper.getIssueID("Verify Meetings Credits Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingsCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Meetings Credits Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Meetings Credits Issue");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMeetingsCreditsIssue");
                executionLog.WriteInExcel("Verify Meetings Credits Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}

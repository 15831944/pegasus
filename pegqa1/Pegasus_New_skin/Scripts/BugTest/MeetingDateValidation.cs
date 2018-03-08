using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MeetingDateValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void meetingDateValidation()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingDateValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingDateValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MeetingDateValidation", "Click to open client info");
                VisitOffice("meetings/create");

                executionLog.Log("MeetingDateValidation", "Verify page title.");
                VerifyTitle("Create a Meeting");

                executionLog.Log("MeetingDateValidation", "Enter Subject for the meeting");
                officeActivities_MeetingHelper.TypeText("Subject", "TESTING MEETING SUBJECT");

                executionLog.Log("MeetingDateValidation", "Enter location for the meeting");
                officeActivities_MeetingHelper.TypeText("Location", "TESTING MEETING LOCATION");

                executionLog.Log("MeetingDateValidation", "Enter start date in the date field");
                officeActivities_MeetingHelper.TypeText("StartDate", "2015-03-28");

                executionLog.Log("MeetingDateValidation", "Enter End Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "2015-03-10");

                executionLog.Log("MeetingDateValidation", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("MeetingDateValidation", "Click On Assigned To");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingDateValidation", "Click on Client You Want To Invite");
                officeActivities_MeetingHelper.ClickElement("ClickOnClientMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingDateValidation", "Click On Save button");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("MeetingDateValidation", "verify validation message.");
                officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                 executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingDateValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Meeting Date Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("MeetingDateValidation", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meeting Date Validation");
                        TakeScreenshot("MeetingDateValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingDateValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingDateValidation");
                        string id = loginHelper.getIssueID("Meeting Date Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingDateValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meeting Date Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meeting Date Validation");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingDateValidation");
                executionLog.WriteInExcel("Meeting Date Validation", Status, JIRA, "Office Activities");
            }
        }
    }
}
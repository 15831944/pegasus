using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminCorpMeetingURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminCorpMeetingURLChange()
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

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminCorpMeetingURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminCorpMeetingURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminCorpMeetingURLChange", "Goto User Admin >> Corporate ");
                VisitOffice("mycorp");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpMeetingURLChange", "Select Activity >> Tasks");
                officeActivities_MeetingHelper.Select("SelectActivityType", "Meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpMeetingURLChange", "Click On Document ");
                officeActivities_MeetingHelper.ClickElement("OpenMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminCorpMeetingURLChange", "Change the url with the url number of another office");
                VisitOffice("viewactivity/meeting/1");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminCorpMeetingURLChange", "Verify Validation");
                officeActivities_MeetingHelper.WaitForText("You don't have privileges to view this office activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminCorpMeetingURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Corp Meeting URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Corp Meeting URL Change", "Bug", "Medium", "Corporate page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Corp Meeting URL Change");
                        TakeScreenshot("AdminCorpMeetingURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpMeetingURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminCorpMeetingURLChange");
                        string id = loginHelper.getIssueID("Admin Corp Meeting URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminCorpMeetingURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Corp Meeting URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Corp Meeting URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminCorpMeetingURLChange");
                executionLog.WriteInExcel("Admin Corp Meeting URL Change", Status, JIRA, "My Corp");
            }
        }
    }
}

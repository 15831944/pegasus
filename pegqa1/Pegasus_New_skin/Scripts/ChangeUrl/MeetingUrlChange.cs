using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MeetingUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void  meetingUrlChange()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MeetingUrlChange", "Goto User Activities >> Meeting");
                VisitOffice("meetings");
               
                executionLog.Log("MeetingUrlChange", "Click On Meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingUrlChange", "Change the url with the url number of another office");
                VisitOffice("meetings/view/21438");
                
                executionLog.Log("MeetingUrlChange", "Verify Validation");
                officeActivities_MeetingHelper.WaitForText("You don't have privileges to view this Meeting." ,10);
                
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Meeting Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Meeting Url Change", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meeting Url Change");
                        TakeScreenshot("MeetingUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingUrlChange");
                        string id = loginHelper.getIssueID("Meeting Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meeting Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meeting Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingUrlChange");
                executionLog.WriteInExcel("Meeting Url Change", Status, JIRA, "Office Activities");
            }
        }
    }
}

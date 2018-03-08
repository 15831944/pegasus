using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ShowMeetingOnCalender : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void showMeetingOnCalender()
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

            // Variable
            var Path = GetPathToFile() + "1.pdf";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ShowMeetingOnCalender", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ShowMeetingOnCalender", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ShowMeetingOnCalender", "Click on Clients in Topmenu");
                VisitOffice("leads");

                executionLog.Log("ShowMeetingOnCalender", "Click On Opp Check Box");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ShowMeetingOnCalender", "Click on Email");
                office_LeadsHelper.ClickElement("ClickOnAddMeeting");

                executionLog.Log("SendEmailNoteMultipleCC", "Wait for element to present.");
                office_LeadsHelper.WaitForElementPresent("EnterSubjectMeeting", 20);

                executionLog.Log("ShowMeetingOnCalender", "Enter meeting subject");
                office_LeadsHelper.TypeText("EnterSubjectMeeting", "Test Meeting");

                executionLog.Log("ShowMeetingOnCalender", "Click On Start Date");
                office_LeadsHelper.ClickElement("ClickOnStartDate");

                executionLog.Log("ShowMeetingOnCalender", "Click On Start Date");
                office_LeadsHelper.ClickElement("SelectDate");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("ShowMeetingOnCalender", "Upload file");
                office_LeadsHelper.UploadFile("//*[@id='DocumentFile']", Path);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ShowMeetingOnCalender", "Click on Send Email button");
                office_LeadsHelper.ClickElement("ClickOnSaveMeeting");

                executionLog.Log("ShowMeetingOnCalender", "Wait for success message.");
                office_LeadsHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("ShowMeetingOnCalender", "Select Activity type as meeting");
                office_LeadsHelper.Select("SelectActivityType", "Meetings");

                executionLog.Log("ShowMeetingOnCalender", "Click on meeting");
                office_LeadsHelper.PressEnter("ClickNotes1");

                executionLog.Log("ShowMeetingOnCalender", "Click on cancel meeting button");
                office_LeadsHelper.ClickElement("CancelMeeting");

                executionLog.Log("ShowMeetingOnCalender", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("ShowMeetingOnCalender", "Wait for success message.");
                office_LeadsHelper.WaitForText("Meeting successfully deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ShowMeetingOnCalender");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ShowMeetingOnCalender");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ShowMeetingOnCalender", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ShowMeetingOnCalender");
                        TakeScreenshot("ShowMeetingOnCalender");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShowMeetingOnCalender.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ShowMeetingOnCalender");
                        string id = loginHelper.getIssueID("ShowMeetingOnCalender");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ShowMeetingOnCalender.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("ShowMeetingOnCalender"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("ShowMeetingOnCalender");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ShowMeetingOnCalender");
                executionLog.WriteInExcel("ShowMeetingOnCalender", Status, JIRA, "Office Activities");
            }
        }
    }
}
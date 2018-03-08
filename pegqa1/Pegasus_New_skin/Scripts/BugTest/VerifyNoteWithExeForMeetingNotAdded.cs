using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyNoteWithExeForMeetingNotAdded : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTest")]
        public void verifyNoteWithExeForMeetingNotAdded()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Random Variables
            var DocName = "Notes Meeting Exe" + GetRandomNumber();
            var ExeFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EquipmentShippingValidationDublicate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EquipmentShippingValidationDublicate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Click to open client inf");
                VisitOffice("meetings");

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Click On Meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Click On Add Document Btn");
                officeActivities_MeetingHelper.ClickElement("ClickOnNoteMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Enter Doc Name");
                officeActivities_MeetingHelper.TypeText("EnterSubNote", DocName);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Uplaod exe file");
                officeActivities_MeetingHelper.Upload("DocumentFileNotesMeet", ExeFile);
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Click Save Of Doc Pop Up");
                officeActivities_MeetingHelper.ClickElement("SaveNoteMeeting");

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Verify Confirmation");
                officeActivities_MeetingHelper.WaitForText("Note successfully Created.", 10);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Redirect To  Notes Activities");
                VisitOffice("notes");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Search Document");
                officeActivities_NotesHelper.TypeText("EnterSubject", DocName);
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                officeActivities_NotesHelper.ClickElement("ClickOnNote");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNoteWithExeForMeetingNotAdded", "Verify Added Document not Present");
                officeActivities_NotesHelper.VerifyText("VerifyNoAttachment", "No Data Available.");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNoteWithExeForMeetingNotAdded");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Note With Exe For Meeting Not Added");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyNoteWithExeForMeetingNotAdded", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Note With Exe For Meeting Not Added");
                        TakeScreenshot("VerifyNoteWithExeForMeetingNotAdded");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNoteWithExeForMeetingNotAdded.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNoteWithExeForMeetingNotAdded");
                        string id = loginHelper.getIssueID("Verify Note With Exe For Meeting Not Added");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNoteWithExeForMeetingNotAdded.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Note With Exe For Meeting Not Added"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Note With Exe For Meeting Not Added");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyNoteWithExeForMeetingNotAdded");
                executionLog.WriteInExcel("Verify Note With Exe For Meeting Not Added", Status, JIRA, "Office Activities");
            }
        }
    }
}
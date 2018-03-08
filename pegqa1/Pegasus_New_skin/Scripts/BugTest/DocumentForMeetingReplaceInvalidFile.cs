using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DocumentForMeetingReplaceInvalidFile : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void documentForMeetingReplaceInvalidFile()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DocumentUpdateVersion", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentUpdateVersion", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Go to meetings page");
                VisitOffice("meetings");

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Click On Meeting In Activity");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Click On Add Document Btn");
                officeActivities_MeetingHelper.ClickElement("ClickOnAddDocumentBtn");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Enter Doc Name");
                var DocName = "Test Valid Doc" + GetRandomNumber();
                officeActivities_MeetingHelper.TypeText("EnterDocumentName", DocName);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Upload Invalid file(exe)");
                var InvalidFile = GetPathToFile() + "chrome.exe";
                officeActivities_MeetingHelper.Upload("BrowseDoc", InvalidFile);
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Replace with valid file(jpg)");
                var ValidFile = GetPathToFile() + "Up.jpg";
                officeActivities_MeetingHelper.Upload("BrowserDoc1", ValidFile);
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Click Save Of Doc Pop Up");
                officeActivities_MeetingHelper.ClickElement("ClickSaveOfDocPopUp");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Verify Confirmation");
                officeActivities_MeetingHelper.WaitForText("Documents successfully Added.", 10);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Redirect To Document Section Activities");
                VisitOffice("documents");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Search Document");
                officeActivities_MeetingHelper.TypeText("SearchSubject", DocName);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Select All in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Verify Added Document Present");
                officeActivities_MeetingHelper.VerifyText("DoucumentFirstInList", DocName);
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Search Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", DocName);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Select document");
                officeActivities_DocumentHelper.ClickElement("CheckDocToDel");

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Click on delete.");
                officeActivities_DocumentHelper.ClickElement("ClickOndelete");

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Accept alert message.");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentForMeetingReplaceInvalidFile", "Wait for success message.");
                officeActivities_DocumentHelper.WaitForText("Document deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentForMeetingReplaceInvalidFile");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Document For Meeting Replace InvalidFile");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("DocumentForMeetingReplaceInvalidFile", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Document For Meeting Replace InvalidFile");
                        TakeScreenshot("DocumentForMeetingReplaceInvalidFile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentForMeetingReplaceInvalidFile.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentForMeetingReplaceInvalidFile");
                        string id = loginHelper.getIssueID("Document For Meeting Replace InvalidFile");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentForMeetingReplaceInvalidFile.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Document For Meeting Replace InvalidFile"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Document For Meeting Replace InvalidFile");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DocumentForMeetingReplaceInvalidFile");
                executionLog.WriteInExcel("Document For Meeting Replace InvalidFile", Status, JIRA, "Office Activities");
            }
        }
    }
}
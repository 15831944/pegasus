using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmailNoteWithRelatedTo : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void emailNoteWithRelatedTo()
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


            // Variable random
            var Subject = "Subject" + RandomNumber(1, 999);
            String Status = "Pass";
            String JIRA = "";
            try
            {

                executionLog.Log("EmailNoteWithRelatedTo", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmailNoteWithRelatedTo", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmailNoteWithRelatedTo", "Redirect To notes");
                VisitOffice("notes");

                executionLog.Log("EmailNoteWithRelatedTo", "Click on Create ");
                officeActivities_NotesHelper.ClickElement("Create");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailNoteWithRelatedTo", "Enter Note Subject");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("EmailNoteWithRelatedTo", "Select Client");
                officeActivities_NotesHelper.SelectDropDownByText("//*[@id='NoteParentType']", "Client");

                executionLog.Log("EmailNoteWithRelatedTo", "ClickIconToSelectClient");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailNoteWithRelatedTo", "Click On Client");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteWithRelatedTo", "Click on Save button");
                officeActivities_NotesHelper.ClickElement("Save");
                officeActivities_NotesHelper.WaitForWorkAround(6000);

                executionLog.Log("EmailNoteWithRelatedTo", "Click On Notes");
                officeActivities_NotesHelper.ClickElement("ClickOnNotes");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteWithRelatedTo", "Click on Email Note");
                officeActivities_NotesHelper.ClickElement("EmailNote");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteWithRelatedTo", "Verify selected client.");
                officeActivities_NotesHelper.VerifyText("VerifySelectedClient", "Client");

                executionLog.Log("AddNotesAdmin", "Redirect at notes page.");
                VisitOffice("notes");

                executionLog.Log("AddNotesAdmin", "Redirect at notes page.");
                officeActivities_NotesHelper.WaitForElementPresent("EnterSubject", 10);

                executionLog.Log("AddNotesAdmin", "Search subject by note");
                officeActivities_NotesHelper.TypeText("EnterSubject", Subject);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddNotesAdmin", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddNotesAdmin", "Search subject by note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddNotesAdmin", "Click on delete note.");
                officeActivities_NotesHelper.ClickElement("DeleteNote");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddNotesAdmin", "Accept alert message.");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("AddNotesAdmin", "Wait for delete text");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmailNoteWithRelatedTo");
                String Error = executionLog.GetAllTextFile("Error");

                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Email Note With Related To");
                Console.WriteLine("Stepp 2 after existing");
                if (!result)
                {
                    if (Int16.Parse(counter) < 15)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssueTest("Email Note With Related To", "Bug", "Medium", "Email note page", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Email Note With Related To");
                        TakeScreenshot("EmailNoteWithRelatedTo");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNoteWithRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 15)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmailNoteWithRelatedTo");
                        string id = loginHelper.getIssueID("Email Note With Related To");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNoteWithRelatedTo.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Email Note With Related To"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Email Note With Related To");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmailNoteWithRelatedTo");
                executionLog.WriteInExcel("Email Note With Related To", Status, JIRA, "Email Actitvities");
            }
        }
    }
}

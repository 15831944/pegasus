using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class NoteAttachmentInvlidFileValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void noteAttachmentInvlidFileValidation()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Random Variables
            var FilerExe = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("NoteAttachmentInvlidFileValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Click on Activities >> Notes");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Click on any Note");
                officeActivities_NotesHelper.ClickElement("ClickOnNotes");

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Click on add attachment");
                officeActivities_NotesHelper.ClickElement("AddAttachment");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Upload a invalid file");
                officeActivities_NotesHelper.UploadFile("//*[@id='DocumentViewForm']/div[2]//input[@id='DocumentFile']", FilerExe);
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("NoteAttachmentInvlidFileValidation", "Verify alert for invalid file.");
                officeActivities_NotesHelper.VerifyAlertText("Please select a valid file");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("NoteAttachmentInvlidFileValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Note Attachment Invlid File Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Note Attachment Invlid File Validation", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Note Attachment Invlid File Validation");
                        TakeScreenshot("NoteAttachmentInvlidFileValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NoteAttachmentInvlidFileValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("NoteAttachmentInvlidFileValidation");
                        string id = loginHelper.getIssueID("Note Attachment Invlid File Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NoteAttachmentInvlidFileValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Note Attachment Invlid File Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Note Attachment Invlid File Validation");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("NoteAttachmentInvlidFileValidation");
                executionLog.WriteInExcel("Note Attachment Invlid File Validation", Status, JIRA, "Office Activities");
            }
        }
    }
}
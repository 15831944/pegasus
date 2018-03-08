using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyNotesCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyNotesCreatedAndModifiedByCredits()
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

            // Variable
            var name = "Note" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Go to notes page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", " Click On Create");
                officeActivities_NotesHelper.ClickElement("Create");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Create a New Note");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on Save  ");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify validation text for subject.");
                officeActivities_NotesHelper.VerifyText("SubjectError", "This field is required.");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Enter note subject.");
                officeActivities_NotesHelper.TypeText("Subject", name);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on save.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Wait for success text");
                officeActivities_NotesHelper.WaitForText("Note saved successfully. ", 10);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Redirect at notes page.");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Select All create by field ");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on searched notes.");
                officeActivities_NotesHelper.ClickElement("ClickOnNotes");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify notes created by credits");
                officeActivities_NotesHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify notes Modified by credits");
                officeActivities_NotesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("EditLink");
                VerifyTitle("Edit Note");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Select note parent");
                officeActivities_NotesHelper.SelectByText("NoteParent", "Client");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", " Click On any client.");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on save button.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify notes created by credits");
                officeActivities_NotesHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify notes Modified by credits");
                officeActivities_NotesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Select All create by field ");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Select First Note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click Delete btn  ");
                officeActivities_NotesHelper.ClickElement("DeleteNote");

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Accept alert message. ");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Wait for delete message. ");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully", 10);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Redirect to recycle bin");
                VisitOffice("notes/recyclebin");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Select All create by field ");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Click on delete icon.");
                officeActivities_NotesHelper.ClickElement("DeleteNoteRBin");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Accept alert message.");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("VerifyNotesCreatedAndModifiedByCredits", "Wait for success message.");
                officeActivities_NotesHelper.WaitForText("Note Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNotesCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Notes Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Notes Created And Modified By Credits", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Notes Created And Modified By Credits");
                        TakeScreenshot("VerifyNotesCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNotesCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Notes Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Notes Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Notes Created And Modified By Credits");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyNotesCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Notes Created And Modified By Credits", Status, JIRA, "Office Activities");
            }
        }
    }
}
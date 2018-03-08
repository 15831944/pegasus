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
    public class VerifyNotesModifiedUpdated : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyNotesModifiedUpdated()
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
                executionLog.Log("VerifyNotesModifiedUpdated", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyNotesModifiedUpdated", "Go to notes page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesModifiedUpdated", " Click On Create");
                officeActivities_NotesHelper.ClickElement("Create");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify page title");
                VerifyTitle("Create a New Note");

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on Save  ");
                officeActivities_NotesHelper.ClickElement("Save");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify validation text for subject.");
                officeActivities_NotesHelper.VerifyText("SubjectError", "This field is required.");

                executionLog.Log("VerifyNotesModifiedUpdated", "Enter note subject.");
                officeActivities_NotesHelper.TypeText("Subject", name);

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on save.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("VerifyNotesModifiedUpdated", "Wait for success text");
                officeActivities_NotesHelper.WaitForText("Note saved successfully. ", 10);

                executionLog.Log("VerifyNotesModifiedUpdated", "Redirect at notes page.");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesModifiedUpdated", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on searched notes.");
                officeActivities_NotesHelper.ClickElement("ClickOnNotes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify notes created by credits");
                officeActivities_NotesHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify notes Modified by credits");
                officeActivities_NotesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("EditLink");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Select note parent");
                officeActivities_NotesHelper.Select("NoteParent", "20");

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesModifiedUpdated", " Click On any client.");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Click on save button.");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify notes created by credits");
                officeActivities_NotesHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify notes Modified by credits");
                officeActivities_NotesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyNotesModifiedUpdated", "Go to note page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Verify page title.");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesModifiedUpdated", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Select First Note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNotesModifiedUpdated", "Click Delete btn  ");
                officeActivities_NotesHelper.ClickElement("DeleteNote");

                executionLog.Log("VerifyNotesModifiedUpdated", "Accept alert message. ");
                officeActivities_NotesHelper.AcceptAlert();

                executionLog.Log("VerifyNotesModifiedUpdated", "Wait for delete message. ");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNotesModifiedUpdated");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Notes Modified Updated");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Notes Modified Updated", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Notes Modified Updated");
                        TakeScreenshot("VerifyNotesModifiedUpdated");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesModifiedUpdated.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNotesModifiedUpdated");
                        string id = loginHelper.getIssueID("Verify Notes Modified Updated");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesModifiedUpdated.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Notes Modified Updated"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Notes Modified Updated");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyNotesModifiedUpdated");
                executionLog.WriteInExcel("Verify Notes Modified Updated", Status, JIRA, "Office Activities");
            }
        }
    }
}

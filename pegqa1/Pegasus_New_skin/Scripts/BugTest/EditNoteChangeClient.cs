using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditNoteChangeClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editNoteChangeClient()
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

            // Random Variables
            var editname = "Note" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditNoteChangeClient", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditNoteChangeClient", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditNoteChangeClient", "Redirect to URL Notes");
                VisitOffice("notes");

                executionLog.Log("EditNoteChangeClient", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("EditNoteChangeClient", " Click On Edit Icon");
                officeActivities_NotesHelper.ClickElement("Edit");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNoteChangeClient", "Verify page title");
                VerifyTitle("Edit Note");

                executionLog.Log("EditNoteChangeClient", "Enter note subj new");
                officeActivities_NotesHelper.TypeText("Subject", editname);
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditNoteChangeClient", "Select Module");
                officeActivities_NotesHelper.SelectDropDownByText("//*[@id='NoteParentType']", "Client");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditNoteChangeClient", "Click on ClientSelectIcon");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNoteChangeClient", " Click On Client2");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNoteChangeClient", "Edit Note Save Btn");
                officeActivities_NotesHelper.ClickElement("Save");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditNoteChangeClient", "Verify note updated successfully");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 20);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditNoteChangeClient");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Note Change Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Note Change Client", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Note Change Client");
                        TakeScreenshot("EditNoteChangeClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNoteChangeClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditNoteChangeClient");
                        string id = loginHelper.getIssueID("Edit Note Change Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNoteChangeClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Note Change Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Note Change Client");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditNoteChangeClient");
                executionLog.WriteInExcel("Edit Note Change Client", Status, JIRA, "Office Activities");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditNoteChangeHistory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editNoteChangeHistory()
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

            // Random Variable
            var editname = "Note" + RandomNumber(1, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditNoteChangeHistory", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditNoteChangeHistory", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditNoteChangeHistory", "Redirect to URL");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditNoteChangeHistory", "Verify title");
                VerifyTitle("Notes");

                executionLog.Log("EditNoteChangeHistory", "Wait for element to present");
                officeActivities_NotesHelper.WaitForElementPresent("Edit", 10);

                executionLog.Log("EditNoteChangeClient", " Click On Edit Icon");
                officeActivities_NotesHelper.ClickElement("Edit");

                executionLog.Log("EditNoteChangeHistory", "Verify title");
                VerifyTitle("Edit Note");

                executionLog.Log("EditNoteChangeClient", "Enter note subj new");
                officeActivities_NotesHelper.TypeText("Subject", editname);

                executionLog.Log("EditNoteChangeClient", "Select Module");
                officeActivities_NotesHelper.SelectDropDownByText("//*[@id='NoteParentType']", "Client");

                executionLog.Log("EditNoteChangeClient", "Click on find list icon.");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                //executionLog.Log("EditNoteChangeHistory", "Wait for element to present");
                //officeActivities_NotesHelper.WaitForElementPresent("ClickOnClient2", 10);

                executionLog.Log("EditNoteChangeClient", " Click client to select.");
                officeActivities_NotesHelper.ClickElement("ClickONClientNS");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNoteChangeHistory", "Click on save button.");
                officeActivities_NotesHelper.ClickElement("Save");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                //executionLog.Log("EditNoteChangeHistory", "Wait for success message.");
                //officeActivities_NotesHelper.WaitForText("Note Updated Success.", 20);

                //executionLog.Log("EditNoteChangeHistory", "Wait for element to present");
                //officeActivities_NotesHelper.WaitForElementPresent("EnterSubject", 03);

                executionLog.Log("EditNoteChangeHistory", "Search note by Subject");
                officeActivities_NotesHelper.TypeText("EnterSubject", editname);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNoteChangeHistory", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                ////executionLog.Log("EditNoteChangeHistory", "Wait for element to present");
                ////officeActivities_NotesHelper.WaitForElementPresent("ClickOnNote", 08);

                //executionLog.Log("EditNoteChangeHistory", "Verify edited name");
                //officeActivities_NotesHelper.VerifyText("ClickOnNote", editname);
                //Console.WriteLine("Note edited");

                executionLog.Log("EditNoteChangeHistory", "Click on edited note");
                officeActivities_NotesHelper.ClickElement("ClickOnNote");

                executionLog.Log("EditNoteChangeHistory", "Wait for element to present");
                officeActivities_NotesHelper.WaitForElementPresent(editname, 04);

                executionLog.Log("EditNoteChangeHistory", "verify ");
                officeActivities_NotesHelper.VerifyNoteEdited(editname);
                //officeActivities_NotesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditNoteChangeHistory");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Note Change History");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Note Change History", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Note Change History");
                        TakeScreenshot("EditNoteChangeHistory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNoteChangeHistory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditNoteChangeHistory");
                        string id = loginHelper.getIssueID("Edit Note Change History");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNoteChangeHistory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Note Change History"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Note Change History");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditNoteChangeHistory");
                executionLog.WriteInExcel("Edit Note Change History", Status, JIRA, "Office Activities");
            }
        }
    }
}
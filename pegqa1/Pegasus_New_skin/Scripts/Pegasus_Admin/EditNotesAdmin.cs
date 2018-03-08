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
    public class EditNotesAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editNotesAdmin()
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
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditNotesAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditNotesAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditNotesAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditNotesAdmin", "Go to note page");
                VisitOffice("notes");

                executionLog.Log("EditNotesAdmin", "Verify title");
                VerifyTitle("Notes");

                executionLog.Log("EditNotesAdmin", " Click On Create");
                officeActivities_NotesHelper.ClickElement("Create");

                executionLog.Log("EditNotesAdmin", "Verify title");
                VerifyTitle("Create a New Note");

                executionLog.Log("EditNotesAdmin", "Enter Name");
                officeActivities_NotesHelper.TypeText("Subject", name);

                executionLog.Log("EditNotesAdmin", "cLICK on Save  ");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("EditNotesAdmin", "Wait for text");
                officeActivities_NotesHelper.WaitForText("Note saved successfully. ", 10);

                executionLog.Log("EditNotesAdmin", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", name);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNotesAdmin", "Select All in Created by fields");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNotesAdmin", "Click on Edit");
                officeActivities_NotesHelper.ClickElement("Edit");
                VerifyTitle("Edit Note");

                executionLog.Log("EditNotesAdmin", "Edit Subject");
                officeActivities_NotesHelper.TypeText("Subject", "Test");

                executionLog.Log("EditNotesAdmin", "Click On Save Btn");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("EditNotesAdmin", "Wait for text");
                officeActivities_NotesHelper.WaitForText("Note Updated Success.", 10);

                executionLog.Log("EditNotesAdmin", "Go to note page");
                VisitOffice("notes");

                executionLog.Log("EditNotesAdmin", "Verify title");
                VerifyTitle("Notes");

                executionLog.Log("EditNotesAdmin", "Enter Subject in Search field");
                officeActivities_NotesHelper.TypeText("EnterSubject", "Test");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNotesAdmin", "Select All in Created by fields");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNotesAdmin", "Select First Note");
                officeActivities_NotesHelper.ClickElement("SelectNote1");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("EditNotesAdmin", "Click Delete btn  ");
                officeActivities_NotesHelper.ClickElement("DeleteNote");

                executionLog.Log("EditNotesAdmin", "Accept alert message. ");
                officeActivities_NotesHelper.AcceptAlert();
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNotesAdmin", "Wait for delete message. ");
                officeActivities_NotesHelper.WaitForText("Note deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditNotesAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Notes Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Notes Admin", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Notes Admin");
                        TakeScreenshot("EditNotesAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNotesAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditNotesAdmin");
                        string id = loginHelper.getIssueID("Edit Notes Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNotesAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Notes Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Notes Admin");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditNotesAdmin");
                executionLog.WriteInExcel("Edit Notes Admin", Status, JIRA, "Office Activities");
            }
        }
    }
}

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
    public class AddNotesAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void addNotesAdmin()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var Subject = "Subject" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("AddNotesAdmin", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AddNotesAdmin", " Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AddNotesAdmin", " Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("AddNotesAdmin", " Goto create notes page.");
                VisitOffice("notes/create");

                executionLog.Log("AddNotesAdmin", " verify title");
                VerifyTitle("Create a New Note");

                executionLog.Log("AddNotesAdmin", " Enter Subject");
                officeActivities_NotesHelper.TypeText("Subject", Subject);

                executionLog.Log("AddNotesAdmin", "Click status");
                officeActivities_NotesHelper.ClickElement("Status");

                executionLog.Log("AddNotesAdmin", " Click on Save  ");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("AddNotesAdmin", " Verify text Note saved successfully ");
                officeActivities_NotesHelper.WaitForText("Note saved successfully.", 10);

                executionLog.Log("AddNotesAdmin", "Redirect at notes page.");
                VisitOffice("notes");

                executionLog.Log("AddNotesAdmin", "Redirect at notes page.");
                officeActivities_NotesHelper.WaitForElementPresent("EnterSubject", 10);

                executionLog.Log("AddNotesAdmin", "Search subject by note");
                officeActivities_NotesHelper.TypeText("EnterSubject", Subject);
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("AddNotesAdmin", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

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
                String Description = executionLog.GetAllTextFile("AddNotesAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Notes Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Notes Admin", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Notes Admin");
                        TakeScreenshot("AddNotesAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddNotesAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddNotesAdmin");
                        string id = loginHelper.getIssueID("Add Notes Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddNotesAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Notes Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Add Notes Admin");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AddNotesAdmin");
                executionLog.WriteInExcel("Add Notes Admin", Status, JIRA, "Office Activities");
            }
        }
    }
}
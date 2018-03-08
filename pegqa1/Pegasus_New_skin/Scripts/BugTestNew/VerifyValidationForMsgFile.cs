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
    public class VerifyValidationForMsgFile : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void verifyValidationForMsgFile()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());

            // Variable
            var file = GetPathToFile() + "test.msg";
            var Subject = "Subject" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

          try
            {
            executionLog.Log("VerifyValidationForMsgFile", " Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VerifyValidationForMsgFile", " Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("VerifyValidationForMsgFile", " Click On  Admin");
            VisitOffice("admin");

            executionLog.Log("VerifyValidationForMsgFile", " Goto create notes page.");
            VisitOffice("notes/create");

            executionLog.Log("VerifyValidationForMsgFile", " verify title");
            VerifyTitle("Create a New Note");

            executionLog.Log("VerifyValidationForMsgFile", " Enter Subject");
            officeActivities_NotesHelper.TypeText("Subject", Subject);

            executionLog.Log("VerifyValidationForMsgFile", "Click status");
            officeActivities_NotesHelper.Select("Status", "Active");

            executionLog.Log("VerifyValidationForMsgFile", " Click on Save  ");
            officeActivities_NotesHelper.ClickElement("Save");

            executionLog.Log("VerifyValidationForMsgFile", " Click on Save  ");
            officeActivities_NotesHelper.WaitForText("Note saved successfully.", 10);

            executionLog.Log("VerifyValidationForMsgFile", "Redirect at notes page.");
            VisitOffice("notes");

            executionLog.Log("VerifyValidationForMsgFile", "Enter Subject in Search field");
            officeActivities_NotesHelper.TypeText("EnterSubject", Subject);
            officeActivities_NotesHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyValidationForMsgFile", "Click on Edit");
            officeActivities_NotesHelper.ClickElement("Edit");
            VerifyTitle("Edit Note");

            executionLog.Log("VerifyValidationForMsgFile", "Click on add attachment.");
            officeActivities_NotesHelper.ClickElement("AddAttEdit");

            executionLog.Log("VerifyValidationForMsgFile", "Wait for locator to be present.");
            officeActivities_NotesHelper.WaitForElementPresent("SubjectAttachment", 10);

            executionLog.Log("VerifyValidationForMsgFile", "Enter attachment subject.");
            officeActivities_NotesHelper.TypeText("SubjectAttachment", "test");

            executionLog.Log("VerifyValidationForMsgFile", "Upload a file with extension .msg");
            officeActivities_NotesHelper.Upload("BrowseAttachment", file);

            executionLog.Log("VerifyValidationForMsgFile", "Click on save button.");
            officeActivities_NotesHelper.ClickElement("SaveAttach");

            executionLog.Log("VerifyValidationForMsgFile", "Wait for invalid file text.");
            officeActivities_NotesHelper.WaitForText("Please upload a valid file.", 10);

        }
       catch (Exception e)
        {
            executionLog.Log("Error", e.StackTrace);
            Status = "Fail";

            String counter = executionLog.readLastLine("counter");
            String Description = executionLog.GetAllTextFile("VerifyValidationForMsgFile");
            String Error = executionLog.GetAllTextFile("Error");
            Console.WriteLine(Error);
            if (counter == "")
            {
                counter = "0";
            }
            bool result = loginHelper.CheckExstingIssue("Verify Validation For Msg File");
            if (!result)
            {
                if (Int16.Parse(counter) < 5)
                {
                    executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                    loginHelper.CreateIssue("Verify Validation For Msg File", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                    string id = loginHelper.getIssueID("Verify Validation For Msg File");
                    TakeScreenshot("VerifyValidationForMsgFile");
                    string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                    var location = directoryName + "\\VerifyValidationForMsgFile.png";
                    loginHelper.AddAttachment(location, id);
                }
            }
            else
            {
                if (Int16.Parse(counter) < 5)
                {
                    executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                    TakeScreenshot("VerifyValidationForMsgFile");
                    string id = loginHelper.getIssueID("Verify Validation For Msg File");
                    string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                    var location = directoryName + "\\VerifyValidationForMsgFile.png";
                    loginHelper.AddAttachment(location, id);
                    loginHelper.AddComment(loginHelper.getIssueID("Verify Validation For Msg File"), "This issue is still occurring");
                }
            }
            JIRA = loginHelper.getIssueID("Verify Validation For Msg File");
            executionLog.DeleteFile("Error");
            throw;
        }
        finally
        {
            executionLog.DeleteFile("VerifyValidationForMsgFile");
            executionLog.WriteInExcel("Verify Validation For Msg File", Status, JIRA, "Office Activities");
        }
    }
}
}
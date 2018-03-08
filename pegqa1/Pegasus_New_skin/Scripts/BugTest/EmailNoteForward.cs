using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmailNoteForward : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void emailNoteForward()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EmailNoteForward", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmailNoteForward", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EmailNoteForward", "Redirect to Notes create page");
                VisitOffice("notes/create");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailNoteForward", "Verify page title");
                VerifyTitle("Create a New Note");

                executionLog.Log("EmailNoteForward", "Enter Subject ");
                officeActivities_NotesHelper.TypeText("Subject", "Email This Note");

                executionLog.Log("EmailNoteForward", "Click on Save");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("EmailNoteForward", "Verify Confirmation");
                officeActivities_NotesHelper.WaitForText("Note saved successfully.", 10);

                executionLog.Log("EmailNoteForward", "Search Note");
                officeActivities_NotesHelper.TypeText("EnterSubject", "Email This Note");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailNoteForward", "Select All in created by field");
                officeActivities_NotesHelper.SelectByText("CreatedbyField", "All");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteForward", "Click on Note");
                officeActivities_NotesHelper.ClickElement("ClickOnNote");

                executionLog.Log("EmailNoteForward", "Wait for locator to be present.");
                officeActivities_NotesHelper.WaitForElementPresent("ClickOnEmail", 10);

                executionLog.Log("EmailNoteForward", "Click On Email This NOte");
                officeActivities_NotesHelper.ClickElement("ClickOnEmail");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmailNoteForward", "Verify Page title.");
                VerifyTitle("Compose");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteForward", "Enter Sender name");
                officeActivities_NotesHelper.TypeText("EnterToFiled", "Test@yopmail.com");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteForward", "Click send Button");
                officeActivities_NotesHelper.ClickElement("ClickSendBtn");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("EmailNoteForward", "Verify Email sent successfully");
                officeActivities_NotesHelper.WaitForText("E-Mail Sent Successfully.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmailNoteForward");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Email Note Forward");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Email Note Forward", "Bug", "Medium", "Note page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Email Note Forward");
                        TakeScreenshot("EmailNoteForward");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNoteForward.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmailNoteForward");
                        string id = loginHelper.getIssueID("Email Note Forward");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmailNoteForward.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Email Note Forward"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Email Note Forward");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmailNoteForward");
                executionLog.WriteInExcel("Email Note Forward", Status, JIRA, "Office Activities.");
            }
        }
    }
}
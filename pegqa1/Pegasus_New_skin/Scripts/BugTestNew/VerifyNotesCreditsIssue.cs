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
    public class VerifyNotesCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyNotesCreditsIssue()
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
                executionLog.Log("VerifyNotesCreditsIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyNotesCreditsIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyNotesCreditsIssue", "Redirect at admin page.");
                VisitOffice("admin");

                executionLog.Log("VerifyNotesCreditsIssue", "Go to notes page");
                VisitOffice("notes");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNotesCreditsIssue", "Verify page title");
                VerifyTitle("Notes");

                executionLog.Log("VerifyNotesCreditsIssue", "Click on any notes.");
                officeActivities_NotesHelper.ClickElement("ClickOnNotes");

                executionLog.Log("VerifyNotesCreditsIssue", "Wait for locator to be present.");
                officeActivities_NotesHelper.WaitForElementPresent("CreatedBy", 10);

                executionLog.Log("VerifyNotesCreditsIssue", "Verify notes created by credits");
                officeActivities_NotesHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreditsIssue", "Verify notes Modified by credits");
                officeActivities_NotesHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyNotesCreditsIssue", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNotesCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Notes Credits Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Notes Credits Issue", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Notes Credits Issue");
                        TakeScreenshot("VerifyNotesCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNotesCreditsIssue");
                        string id = loginHelper.getIssueID("Verify Notes Credits Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyNotesCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Notes Credits Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Notes Credits Issue");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyNotesCreditsIssue");
                executionLog.WriteInExcel("Verify Notes Credits Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}

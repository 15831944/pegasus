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
    public class VerifyRelatedToTicketsPopupInNotes : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyRelatedToTicketsPopupInNotes()
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
                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Redirect at Create Note page");
                VisitOffice("notes/create");
                officeActivities_NotesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Select Related To >> Tickets");
                officeActivities_NotesHelper.SelectByText("NoteParent", "Ticket");
                officeActivities_NotesHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Click 'Select' button");
                officeActivities_NotesHelper.ClickElement("SelectClient");
                officeActivities_NotesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRelatedToTicketsPopupInNotes", "Verify Ticket popup opened");
                officeActivities_NotesHelper.VerifyTextAvailable("Select Ticket");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyRelatedToTicketsPopupInNotes");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Related To Tickets Popup In Notes");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Related To Tickets Popup In Notes", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Related To Tickets Popup In Notes");
                        TakeScreenshot("VerifyRelatedToTicketsPopupInNotes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRelatedToTicketsPopupInNotes.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyRelatedToTicketsPopupInNotes");
                        string id = loginHelper.getIssueID("Verify Related To Tickets Popup In Notes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRelatedToTicketsPopupInNotes.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Related To Tickets Popup In Notes"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Related To Tickets Popup In Notes");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyRelatedToTicketsPopupInNotes");
                executionLog.WriteInExcel("Verify Related To Tickets Popup In Notes", Status, JIRA, "Office Activities");
            }
        }
    }
}
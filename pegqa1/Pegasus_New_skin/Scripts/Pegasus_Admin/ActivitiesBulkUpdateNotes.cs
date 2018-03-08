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
    public class ActivitiesBulkUpdateNotes : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Test1")]
        public void activitiesBulkUpdateNotes()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
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
            executionLog.Log("ActivitiesBulkUpdateNotes", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ActivitiesBulkUpdateNotes", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Redirect at admin page.");
            VisitOffice("admin");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Go to notes page");
            VisitOffice("notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Verify page title");
            VerifyTitle("Notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on Bulk Update");
            officeActivities_NotesHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on Change Status");
            officeActivities_NotesHelper.ClickElement("ChangeStatus");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Verify alert text for selecting note.");
            officeActivities_NotesHelper.VerifyAlertText("Please select atleast one record to proceed.");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Accept alert message by clickin ok.");
            officeActivities_NotesHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on first note.");
            officeActivities_NotesHelper.ClickElement("SelectNote1");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on second note.");
            officeActivities_NotesHelper.ClickElement("SelectNote2");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on bulk update.");
            officeActivities_NotesHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on change status.");
            officeActivities_NotesHelper.ClickElement("ChangeStatus");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Select status to be updated.");
            officeActivities_NotesHelper.SelectByText("SelectStatus", "Inactive");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on update button.");
            officeActivities_NotesHelper.ClickElement("UpdateStatus");
            officeActivities_NotesHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateNotes", "Wait for success text.");
            officeActivities_NotesHelper.WaitForText("Note status updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateNotes", "Redirect at notes page.");
            VisitOffice("notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Verify Page title as notes.");
            VerifyTitle("Notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on first notes.");
            officeActivities_NotesHelper.ClickElement("SelectNote1");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on second notes.");
            officeActivities_NotesHelper.ClickElement("SelectNote2");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on bulk update.");
            officeActivities_NotesHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on change owner.");
            officeActivities_NotesHelper.ClickElement("ChangeOwner");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Select owner to be updated.");
            officeActivities_NotesHelper.SelectByText("SelectOwner", "Ikuta Toma");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on update button.");
            officeActivities_NotesHelper.ClickElement("UpdateOwner");
            officeActivities_NotesHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateNotes", "Wait for success text.");
            officeActivities_NotesHelper.WaitForText("Note owner updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateNotes", "Redirect at notes page.");
            VisitOffice("notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Verify Page title as notes.");
            VerifyTitle("Notes");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on first note.");
            officeActivities_NotesHelper.ClickElement("SelectNote1");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on second note.");
            officeActivities_NotesHelper.ClickElement("SelectNote2");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on bulk update.");
            officeActivities_NotesHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on change user group.");
            officeActivities_NotesHelper.ClickElement("ChangeGroup");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Select user group to be updated.");
            officeActivities_NotesHelper.Select("SelectGroup", "169");

            executionLog.Log("ActivitiesBulkUpdateNotes", "Click on update button.");
            officeActivities_NotesHelper.ClickElement("UpdateGroup");
            officeActivities_NotesHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateNotes", "Wait for success text.");
            officeActivities_NotesHelper.WaitForText("Note user group updated successfully.", 10);

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesBulkUpdateNotes");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Bulk Update Notes");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Bulk Update Notes", "Bug", "Medium", "Notes page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Bulk Update Notes");
                        TakeScreenshot("ActivitiesBulkUpdateNotes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateNotes.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesBulkUpdateNotes");
                        string id = loginHelper.getIssueID("Activities Bulk Update Notes");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateNotes.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Bulk Update Notes"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Bulk Update Notes");
                executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesBulkUpdateNotes");
                executionLog.WriteInExcel("Activities Bulk Update Notes", Status, JIRA, "Office Activities");
            }
        }
    }
}
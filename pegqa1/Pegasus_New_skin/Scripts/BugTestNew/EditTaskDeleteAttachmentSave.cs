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
    public class EditTaskDeleteAttachmentSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editTaskDeleteAttachmentSave()
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
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            // Random Variable
            var File = GetPathToFile() + "2.pdf";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EditTaskDeleteAttachmentSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditTaskDeleteAttachmentSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditTaskDeleteAttachmentSave", "Click on Activities >> task");
                VisitOffice("tasks");

                executionLog.Log("EditTaskDeleteAttachmentSave", "Click on Edit Task");
                officeActivities_TasksHelper.ClickElement("EditTaskIcon");

                executionLog.Log("EditTaskDeleteAttachmentSave", "Click on Add Attachment");
                officeActivities_TasksHelper.ClickElement("AddAttachment");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTaskDeleteAttachmentSave", "Enter attachment name");
                officeActivities_TasksHelper.TypeText("NameAttachment", "Test");

                executionLog.Log("EditTaskDeleteAttachmentSave", "Uplaod an invalid file");
                officeActivities_TasksHelper.Upload("BrowseFile", File);

                executionLog.Log("EditTaskDeleteAttachmentSave", "Click on remove Attachment");
                officeActivities_TasksHelper.ClickElement("RemoveAttachment");

                executionLog.Log("EditTaskDeleteAttachmentSave", "Click on save button");
                officeActivities_TasksHelper.ClickElement("SaveAttachment");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTaskDeleteAttachmentSave", "Verify text on page.");
                officeActivities_TasksHelper.VerifyText("AttachmentError", "This field is required.");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditTaskDeleteAttachmentSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Task Delete Attachment Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Task Delete Attachment Save", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Task Delete Attachment Save");
                        TakeScreenshot("EditTaskDeleteAttachmentSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTaskDeleteAttachmentSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditTaskDeleteAttachmentSave");
                        string id = loginHelper.getIssueID("Edit Task Delete Attachment Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTaskDeleteAttachmentSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Task Delete Attachment Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Task Delete Attachment Save");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditTaskDeleteAttachmentSave");
                executionLog.WriteInExcel("Edit Task Delete Attachment Save", Status, JIRA, "Office Activities");
            }
        }
    }
}
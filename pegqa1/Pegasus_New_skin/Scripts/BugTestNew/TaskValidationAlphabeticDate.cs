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
    public class TaskValidationAlphabeticDate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void taskValidationAlphabeticDate()
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

            // Random Variables
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("TaskValidationAlphabeticDate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TaskValidationAlphabeticDate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TaskValidationAlphabeticDate", "Redirect at create task page.");
                VisitOffice("tasks/create");

                executionLog.Log("TaskValidationAlphabeticDate", "Enter task name.");
                officeActivities_TasksHelper.TypeText("Subjuct1", "Tester");

                executionLog.Log("TaskValidationAlphabeticDate", "Enter invalid date in end date field.");
                officeActivities_TasksHelper.TypeText("DueDate", "Test");

                executionLog.Log("TaskValidationAlphabeticDate", "Click on Save button.");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskValidationAlphabeticDate", "Verify Alert text for invalid date.");
                officeActivities_TasksHelper.VerifyPageText("This field is required.");

                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TaskValidationAlphabeticDate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Task Validation Alphabetic Date");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Task Validation Alphabetic Date", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Task Validation Alphabetic Date");
                        TakeScreenshot("TaskValidationAlphabeticDate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskValidationAlphabeticDate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TaskValidationAlphabeticDate");
                        string id = loginHelper.getIssueID("Task Validation Alphabetic Date");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskValidationAlphabeticDate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Task Validation Alphabetic Date"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Task Validation Alphabetic Date");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TaskValidationAlphabeticDate");
                executionLog.WriteInExcel("Task Validation Alphabetic Date", Status, JIRA, "Office Actitvities");
            }
        }
    }
}
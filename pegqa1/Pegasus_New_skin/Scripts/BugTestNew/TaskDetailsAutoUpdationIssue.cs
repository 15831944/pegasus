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
    public class TaskDetailsAutoUpdationIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void taskDetailsAutoUpdationIssue()
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

            // Variable
            var name = "Task" + RandomNumber(111, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TaskEndDateAutoPopulateIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Redirect at activities tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify page title as tasks");
                VerifyTitle("Tasks");

                executionLog.Log("TaskEndDateAutoPopulateIssue", " Click On Create button.");
                officeActivities_TasksHelper.ClickElement("Create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify page title as create a task.");
                VerifyTitle("Create a Task");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Enter task Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", name);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Enter task start date");
                officeActivities_TasksHelper.TypeText("StartDate", "17/12/2017");
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Enter task start date");
                officeActivities_TasksHelper.TypeText("DueDate", "17/12/2017");
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Click  on Save button. ");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Wait for creation success message. ");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify page title as tasks.");
                VerifyTitle("Tasks");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("EnterSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Click on the edit icon.");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify page title as edit task.");
                VerifyTitle("Edit Task");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify task end date is filled.");
                officeActivities_TasksHelper.VerifyEndDate();
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Click On Save Button.");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Wait for task updation success message.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Verify page title as tasks");
                VerifyTitle("Tasks");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Enter Subject name in Search field");
                officeActivities_TasksHelper.TypeText("EnterSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Select first task by clicking checkbox.");
                officeActivities_TasksHelper.ClickElement("CheckBox1");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Click on delete button.");
                officeActivities_TasksHelper.ClickElement("DeleteTask");

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Accept alert message by clcking OK ");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("TaskEndDateAutoPopulateIssue", "Wait for deletion success message. ");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TaskEndDateAutoPopulateIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Task End Date Auto Populate Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Task End Date Auto Populate Issue", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Task End Date Auto Populate Issue");
                        TakeScreenshot("TaskEndDateAutoPopulateIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskEndDateAutoPopulateIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TaskEndDateAutoPopulateIssue");
                        string id = loginHelper.getIssueID("Task End Date Auto Populate Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskEndDateAutoPopulateIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Task End Date Auto Populate Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Task End Date Auto Populate Issue");
                //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("TaskEndDateAutoPopulateIssue");
                executionLog.WriteInExcel("Task End Date Auto Populate Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}
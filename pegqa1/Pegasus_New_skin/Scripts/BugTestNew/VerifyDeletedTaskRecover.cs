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
    public class VerifyDeletedTaskRecover : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("BugTestNew")]
        public void verifyDeletedTaskRecover()
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

            //try
            //{
                executionLog.Log("VerifyDeletedTaskRecover", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDeletedTaskRecover", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyDeletedTaskRecover", "Redirect at activities tasks page.");
                VisitOffice("tasks");

                executionLog.Log("VerifyDeletedTaskRecover", "Verify page title as tasks");
                VerifyTitle("Tasks");

                executionLog.Log("VerifyDeletedTaskRecover", " Click On Create button.");
                officeActivities_TasksHelper.ClickElement("Create");

                executionLog.Log("VerifyDeletedTaskRecover", "Verify page title as create a task.");
                VerifyTitle("Create a Task");

                executionLog.Log("VerifyDeletedTaskRecover", "Enter task Subject");
                officeActivities_TasksHelper.TypeText("Subject", name);

                executionLog.Log("VerifyDeletedTaskRecover", "Enter task start date");
                officeActivities_TasksHelper.TypeText("StartDate", "05/05/2017");
               // officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyDeletedTaskRecover", "Enter task start date");
                officeActivities_TasksHelper.TypeText("DueDate", "05/05/2017");
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyDeletedTaskRecover", "Click  on Save button. ");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyDeletedTaskRecover", "Wait for creation success message. ");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

                executionLog.Log("VerifyDeletedTaskRecover", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTaskRecover", "Verify page title as tasks.");
                VerifyTitle("Tasks");

                executionLog.Log("VerifyDeletedTaskRecover", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("SearchSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTaskRecover", "Click on check box");
                officeActivities_TasksHelper.ClickElement("FirstCheckBox");

                executionLog.Log("VerifyDeletedTaskRecover", "Click on Delete button");
                officeActivities_TasksHelper.ClickElement("DeleteTask");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDeletedTaskRecover", "Accept alert message by clcking OK ");
                officeActivities_TasksHelper.AcceptAlert();
                officeActivities_TasksHelper.WaitForWorkAround(1000);
                officeActivities_TasksHelper.WaitForText("Task deleted successfully", 10);

                executionLog.Log("VerifyDeletedTaskRecover", "Redirect at Recycle Bin page.");
                VisitOffice("tasks/recyclebin");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTaskRecover", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("SearchSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTaskRecover", "Click on Restore button");
                officeActivities_TasksHelper.ClickElement("FirstRestoreBtn");

                executionLog.Log("VerifyDeletedTaskRecover", "Wait for restore success message. ");
                officeActivities_TasksHelper.WaitForText("Task Restored Successfully.", 10);

                executionLog.Log("VerifyDeletedTaskRecover", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDeletedTaskRecover", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("SearchSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDeletedTaskRecover", "Enter Subject in Search field");
                officeActivities_TasksHelper.VerifyText("FirstTask", name);

                executionLog.Log("VerifyDeletedTaskRecover", "Click on check box");
                officeActivities_TasksHelper.ClickElement("FirstCheckBox");

                executionLog.Log("VerifyDeletedTaskRecover", "Click on Delete button");
                officeActivities_TasksHelper.ClickElement("DeleteTask");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDeletedTaskRecover", "Accept alert message by clcking OK ");
                officeActivities_TasksHelper.AcceptAlert();
                officeActivities_TasksHelper.WaitForWorkAround(1000);
                officeActivities_TasksHelper.WaitForText("Task deleted successfully", 10);


            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("TaskEndDateAutoPopulateIssue");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Task End Date Auto Populate Issue");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("Task End Date Auto Populate Issue", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Task End Date Auto Populate Issue");
            //            TakeScreenshot("TaskEndDateAutoPopulateIssue");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\TaskEndDateAutoPopulateIssue.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("TaskEndDateAutoPopulateIssue");
            //            string id = loginHelper.getIssueID("Task End Date Auto Populate Issue");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\TaskEndDateAutoPopulateIssue.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Task End Date Auto Populate Issue"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Task End Date Auto Populate Issue");
            //  //  executionLog.DeleteFile("Error");
            //    throw;
            //}
            //finally
            //{
            //    executionLog.DeleteFile("TaskEndDateAutoPopulateIssue");
            //    executionLog.WriteInExcel("Task End Date Auto Populate Issue", Status, JIRA, "Office Activities");
            //}
        }
    }
}
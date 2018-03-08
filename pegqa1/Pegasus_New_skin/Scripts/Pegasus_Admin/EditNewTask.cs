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
    public class EditNewTask : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editNewTask()
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
            var name = "Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditNewTask", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditNewTask", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditNewTask", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("EditNewTask", "Go tot add task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Verify title");
                VerifyTitle("Tasks");

                executionLog.Log("EditNewTask", " Click On Create");
                officeActivities_TasksHelper.ClickElement("Create");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Verify title");
                VerifyTitle("Create a Task");

                executionLog.Log("EditNewTask", "Enter Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", name);

                executionLog.Log("EditNewTask", "Enter start date");
                officeActivities_TasksHelper.TypeText("StartDate", "12/22/2016");

                executionLog.Log("EditNewTask", "Enter Due date");
                officeActivities_TasksHelper.TypeText("DueDate", "12/30/2016");

                executionLog.Log("EditNewTask", "Click  on Save  ");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("EditNewTask", "Wait for success message. ");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

                executionLog.Log("EditNewTask", "Go to task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewTask", "Verify title");
                VerifyTitle("Tasks");

                executionLog.Log("EditNewTask", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("EnterSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Selecet All in Owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Verify title");
                VerifyTitle("Edit Task");

                executionLog.Log("EditNewTask", "Enter Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", "Updated Subject");

                executionLog.Log("EditNewTask", "Click On Save Btn");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("EditNewTask", "Go tot add task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("EditNewTask", "Verify title");
                VerifyTitle("Tasks");

                executionLog.Log("EditNewTask", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("EnterSubject", "Updated Subject");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewTask", "Selecet All in Owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewTask", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");

                executionLog.Log("EditNewTask", "Click on delete btn ");
                officeActivities_TasksHelper.ClickElement("DeleteTask");

                executionLog.Log("EditNewTask", "Accept alert message. ");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("EditNewTask", "Wait for delete message. ");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditNewTask");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit New Task");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit New Task", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit New Task");
                        TakeScreenshot("EditNewTask");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNewTask.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditNewTask");
                        string id = loginHelper.getIssueID("Edit New Task");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNewTask.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit New Task"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit New Task");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditNewTask");
                executionLog.WriteInExcel("Edit New Task", Status, JIRA, "Office Activities");
            }
        }
    }
}
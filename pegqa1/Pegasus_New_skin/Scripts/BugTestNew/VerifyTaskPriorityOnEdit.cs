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
    public class VerifyTaskPriorityOnEdit : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyTaskPriorityOnEdit()
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
            var name = "Task" + RandomNumber(111, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTaskPriorityOnEdit", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyTaskPriorityOnEdit", "Redirect at Create Task page.");
                VisitOffice("tasks/create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Verify page title as tasks");
                VerifyTitle("Create a Task");

                executionLog.Log("VerifyTaskPriorityOnEdit", "Enter task Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", name);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Enter task start date");
                officeActivities_TasksHelper.TypeText("StartDate", "11/11/2017");

                executionLog.Log("VerifyTaskPriorityOnEdit", "Enter task Due date");
                officeActivities_TasksHelper.TypeText("DueDate", "11/11/2017");

                executionLog.Log("VerifyTaskPriorityOnEdit", "Select Priority >> High");
                officeActivities_TasksHelper.SelectByText("SelPriority", "High");

                executionLog.Log("VerifyTaskPriorityOnEdit", "Click on Save button. ");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Wait for creation success message. ");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 05);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Enter Subject in Search field");
                officeActivities_TasksHelper.TypeText("EnterSubject", name);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Click on the edit icon.");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskPriorityOnEdit", "Verify selected priority.");
                officeActivities_TasksHelper.VerifySelectdOptn("SelPriority", "High");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTaskPriorityOnEdit");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Task Priority On Edit");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Task Priority On Edit", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Task Priority On Edit");
                        TakeScreenshot("VerifyTaskPriorityOnEdit");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTaskPriorityOnEdit.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTaskPriorityOnEdit");
                        string id = loginHelper.getIssueID("Verify Task Priority On Edit");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTaskPriorityOnEdit.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Task Priority On Edit"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Task Priority On Edit");
          //      executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyTaskPriorityOnEdit");
                executionLog.WriteInExcel("Verify Task Priority On Edit", Status, JIRA, "Office Activities");
            }
        }
    }
}
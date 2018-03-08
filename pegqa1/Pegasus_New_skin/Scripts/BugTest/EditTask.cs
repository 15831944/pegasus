using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditTask : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editTask()
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
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());
            var Subject = "Task" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("EditTask", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditTask", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditTask", "Redirect To Create Task");
                VisitOffice("tasks/create");

                executionLog.Log("EditTask", "Verify Page title.");
                VerifyTitle("Create a Task");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTask", "Enter Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject);

                executionLog.Log("EditTask", "Task Start Date");
                officeActivities_TasksHelper.TypeText("StartDate", "2015-07-02");

                executionLog.Log("EditTask", "Task End Date");
                officeActivities_TasksHelper.TypeText("DueDate", "2015-09-08");

                executionLog.Log("EditTask", "Click on Save");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 20);

                executionLog.Log("EditTask", "Redirect To Task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTask", "Search Task");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);

                executionLog.Log("EditTask", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTask", "Click on Task To Edit");
                officeActivities_TasksHelper.ClickElement("ClickOnTaskEdit");

                executionLog.Log("EditTask", "Click on Edit Button");
                officeActivities_TasksHelper.ClickElement("ClickEditBtn");

                executionLog.Log("EditTask", "Enter Subject");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject.Replace("Task", "Edit"));

                executionLog.Log("EditTask", "Click On Save");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("EditTask", "Wait for success test.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("EditTask", "Redirect To Task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTask", "Search Task");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject.Replace("Task", "Edit"));
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("EditTask", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTask", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");

                executionLog.Log("EditTask", "Click on delete task");
                officeActivities_TasksHelper.ClickJs("DeleteTask");

                executionLog.Log("EditTask", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("EditTask", "Wait for success message.");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditTask");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Task");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Task", "Bug", "Medium", "Task page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Task");
                        TakeScreenshot("EditTask");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTask.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditTask");
                        string id = loginHelper.getIssueID("Edit Task");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTask.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Task"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Task");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditTask");
                executionLog.WriteInExcel("Edit Task", Status, JIRA, "Office Activities");
            }
        }
    }
}

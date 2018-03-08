using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TaskRelatedToIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void taskRelatedToIssue()
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

            // Random Variables.
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TaskRelatedToIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TaskRelatedToIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TaskRelatedToIssue", "Redirect at tasks page.");
                VisitOffice("tasks");

                executionLog.Log("TaskRelatedToIssue", "Redirect at meetings page.");
                VerifyTitle("Tasks");

                executionLog.Log("TaskRelatedToIssue", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");

                executionLog.Log("TaskRelatedToIssue", "Verify page title.");
                VerifyTitle("Edit Task");

                executionLog.Log("TaskRelatedToIssue", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "14");

                executionLog.Log("TaskRelatedToIssue", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskRelatedToIssue", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("TaskRelatedToIssue", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskRelatedToIssue", "Wait for updation success.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("TaskRelatedToIssue", "Redirect at meetings page.");
                VisitOffice("tasks");

                executionLog.Log("TaskRelatedToIssue", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");

                executionLog.Log("TaskRelatedToIssue", "Verify page title.");
                VerifyTitle("Edit Task");

                executionLog.Log("TaskRelatedToIssue", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "15");

                executionLog.Log("TaskRelatedToIssue", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskRelatedToIssue", "Click on opportunity for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("TaskRelatedToIssue", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("TaskRelatedToIssue", "Wait for updation success message.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TaskRelatedToIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Task Related To Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Task Related To Issue", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Task Related To Issue");
                        TakeScreenshot("TaskRelatedToIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskRelatedToIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TaskRelatedToIssue");
                        string id = loginHelper.getIssueID("Task Related To Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskRelatedToIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Task Related To Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Task Related To Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TaskRelatedToIssue");
                executionLog.WriteInExcel("Task Related To Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}


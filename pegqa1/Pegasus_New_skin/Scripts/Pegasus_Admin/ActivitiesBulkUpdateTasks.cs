using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesBulkUpdateTasks : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        public void activitiesBulkUpdateTasks()
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
            executionLog.Log("ActivitiesBulkUpdateTasks", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Redirect at tasks page.");
            VisitOffice("tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify Page title as Tasks");
            VerifyTitle("Tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on Bulk Update");
            officeActivities_TasksHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on Change Status");
            officeActivities_TasksHelper.ClickElement("ChangeStatus");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify alert text for selecting task.");
            officeActivities_TasksHelper.VerifyAlertText("Please select atleast one record to proceed.");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Accept alert message by clickin ok.");
            officeActivities_TasksHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on first task.");
            officeActivities_TasksHelper.ClickElement("CheckBox1");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on second task.");
            officeActivities_TasksHelper.ClickElement("CheckBox2");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on bulk update.");
            officeActivities_TasksHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on change status.");
            officeActivities_TasksHelper.ClickElement("ChangeStatus");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Select status to be updated.");
            officeActivities_TasksHelper.SelectByText("SelectStatus", "In Progress");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on update button.");
            officeActivities_TasksHelper.ClickElement("UpdateStatus");
            officeActivities_TasksHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateTasks", "Wait for success text.");
            officeActivities_TasksHelper.WaitForText("Task status updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateTasks", "Redirect at tasks page.");
            VisitOffice("tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify Page title as tasks.");
            VerifyTitle("Tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on first task.");
            officeActivities_TasksHelper.ClickElement("CheckBox1");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on second task.");
            officeActivities_TasksHelper.ClickElement("CheckBox2");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on bulk update.");
            officeActivities_TasksHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on change user group.");
            officeActivities_TasksHelper.ClickElement("ChangeUserGroup");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Select group to be updated.");
            officeActivities_TasksHelper.Select("SelectGroup", "169");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on update button.");
            officeActivities_TasksHelper.ClickElement("UpdateGroup");
            officeActivities_TasksHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateTasks", "Wait for success text.");
            officeActivities_TasksHelper.WaitForText("Task user group updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateTasks", "Redirect at tasks page.");
            VisitOffice("tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify Page title as tasks.");
            VerifyTitle("Tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on first task.");
            officeActivities_TasksHelper.ClickElement("CheckBox1");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on second task.");
            officeActivities_TasksHelper.ClickElement("CheckBox2");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on bulk update.");
            officeActivities_TasksHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on change owner.");
            officeActivities_TasksHelper.ClickElement("ChangeOwner");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Select owner to be updated.");
            officeActivities_TasksHelper.SelectByText("SelectOwner", "Howard Tang");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on update button.");
            officeActivities_TasksHelper.ClickElement("UpdateOwner");
            officeActivities_TasksHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateTasks", "Wait for success text.");
            officeActivities_TasksHelper.WaitForText("Task owner updated successfully.", 10);

            executionLog.Log("ActivitiesBulkUpdateTasks", "Redirect at tasks page.");
            VisitOffice("tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Verify Page title as tasks.");
            VerifyTitle("Tasks");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on first task.");
            officeActivities_TasksHelper.ClickElement("CheckBox1");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on second task.");
            officeActivities_TasksHelper.ClickElement("CheckBox2");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on bulk update.");
            officeActivities_TasksHelper.ClickElement("BulkUpdate");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on change priority.");
            officeActivities_TasksHelper.ClickElement("ChangePriority");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Select status to be updated.");
            officeActivities_TasksHelper.SelectByText("SelectPriority", "High");

            executionLog.Log("ActivitiesBulkUpdateTasks", "Click on update button.");
            officeActivities_TasksHelper.ClickElement("UpdatePriority");
            officeActivities_TasksHelper.AcceptAlert();

            executionLog.Log("ActivitiesBulkUpdateTasks", "Wait for success text.");
            officeActivities_TasksHelper.WaitForText("Task priority updated successfully.", 10);

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesBulkUpdateTasks");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Bulk Update Tasks");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Bulk Update Tasks", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Bulk Update Tasks");
                        TakeScreenshot("ActivitiesBulkUpdateTasks");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateTasks.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesBulkUpdateTasks");
                        string id = loginHelper.getIssueID("Activities Bulk Update Tasks");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesBulkUpdateTasks.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Bulk Update Tasks"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Bulk Update Tasks");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ActivitiesBulkUpdateTasks");
                executionLog.WriteInExcel("Activities Bulk Update Tasks", Status, JIRA, "Office Activities");
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientTaskUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void clientTaskUrlChange()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            // Random variable.
            var Subject = "Subject" + RandomNumber(1, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientTaskUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientTaskUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientTaskUrlChange", "Goto User Agent >> Leads");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientTaskUrlChange", "Click On Any Client");
                office_ClientsHelper.ClickElement("ClickOnAnyClient");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientTaskUrlChange", "Click On Add Document");
                office_ClientsHelper.ClickElement("AddTask");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientTaskUrlChange", "Enter Task Name");
                officeActivities_TasksHelper.TypeText("TaskSubject", Subject);

                executionLog.Log("ClientTaskUrlChange", "Enter Start Date");
                officeActivities_TasksHelper.TypeText("TaskStartDate", "17/11/2017");

                executionLog.Log("ClientTaskUrlChange", "Enter Start Date");
                officeActivities_TasksHelper.TypeText("TaskDueDate", "17/11/2017");

                executionLog.Log("ClientTaskUrlChange", "Click Save");
                officeActivities_TasksHelper.ClickElement("TaskSave");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientTaskUrlChange", "Select Activity >> Tasks");
                officeActivities_TasksHelper.Select("TaskActivity", "Tasks");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientTaskUrlChange", "Click On Subject");
                officeActivities_TasksHelper.PressEnter("OpenTaskClient");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientTaskUrlChange", "Change the url with the url number of another office");
                VisitOffice("tasks/view/41");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientTaskUrlChange", "Verify Validation");
                officeActivities_TasksHelper.WaitForText("You don't have privileges to view this task.", 10);
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ClientNotesUrlChange", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientNotesUrlChange", "Search subject by task");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientNotesUrlChange", "Click on first tasks");
                officeActivities_TasksHelper.ClickElement("CheckBox1");

                executionLog.Log("ClientNotesUrlChange", "Click on delete btn.");
                officeActivities_TasksHelper.ClickElement("DeleteTask");

                executionLog.Log("ClientNotesUrlChange", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("ClientNotesUrlChange", "Wait for delete text");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientTaskUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Task Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Task Url Change", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Task Url Change");
                        TakeScreenshot("ClientTaskUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientTaskUrlChange");
                        string id = loginHelper.getIssueID("Client Task Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Task Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Task Url Change");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientTaskUrlChange");
                executionLog.WriteInExcel("Client Task Url Change", Status, JIRA, "Client Management");
            }
        }
    }
}

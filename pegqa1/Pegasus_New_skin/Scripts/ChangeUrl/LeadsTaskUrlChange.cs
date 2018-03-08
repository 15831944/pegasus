using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsTaskUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS4")]
        [TestCategory("ChangeUrl")]
        public void leadsTaskUrlChange()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            // Variable
            var Subject = "Subject" + RandomNumber(1, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsTaskUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsTaskUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadsTaskUrlChange", "Goto Leads page");
                VisitOffice("leads");

                executionLog.Log("LeadsTaskUrlChange", "Click On Any Lead");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsTaskUrlChange", "Click On Add task");
                office_LeadsHelper.ClickElement("AddTask");

                executionLog.Log("LeadsTaskUrlChange", "Enter Task Name");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject);

                executionLog.Log("LeadsTaskUrlChange", "Enter Start Date");
                officeActivities_TasksHelper.TypeText("StartDate", "2015-10-07");

                executionLog.Log("LeadsTaskUrlChange", "Enter Start Date");
                officeActivities_TasksHelper.TypeText("DueDate", "2015-10-20");

                executionLog.Log("LeadsTaskUrlChange", "Click Save");
                officeActivities_TasksHelper.ClickDisplayed("//button[@title='Save']");

                executionLog.Log("LeadsTaskUrlChange", "Click On Subject");
                officeActivities_TasksHelper.PressEnter("ClickMeeting1");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsTaskUrlChange", "Change the url with the url number of another office");
                VisitOffice("tasks/view/41");

                executionLog.Log("LeadsTaskUrlChange", "Verify Validation");
                officeActivities_TasksHelper.WaitForText("You don't have privileges to view this task.", 10);

                executionLog.Log("LeadsTaskUrlChange", "Redirect at tasks page.");
                VisitOffice("tasks");

                executionLog.Log("LeadsTaskUrlChange", "Search subject by task");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadsTaskUrlChange", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadsTaskUrlChange", "Click on first tasks");
                officeActivities_TasksHelper.ClickElement("CheckBox1");

                executionLog.Log("LeadsTaskUrlChange", "Click on delete btn.");
                officeActivities_TasksHelper.ClickElement("DeleteTask");

                executionLog.Log("LeadsTaskUrlChange", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("LeadsTaskUrlChange", "Wait for delete text");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsTaskUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Task Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Task Url Change", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Task Url Change");
                        TakeScreenshot("LeadsTaskUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsTaskUrlChange");
                        string id = loginHelper.getIssueID("Leads Task Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Task Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Task Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadsTaskUrlChange");
                executionLog.WriteInExcel("Leads Task Url Change", Status, JIRA, "Leads Management");
            }
        }
    }
}

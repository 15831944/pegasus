using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesTasksManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesTasksManagement()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Random Variables.
            var Subject = "Task" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ActivitiesTasksManagement", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesTasksManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesTasksManagement", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On create button");
                officeActivities_TasksHelper.ClickElement("Create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify page title.");
                VerifyTitle("Create a Task");

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("ActivitiesTasksManagement", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

                executionLog.Log("ActivitiesTasksManagement", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

                executionLog.Log("ActivitiesTasksManagement", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ErrorDuedate", "This field is required.");

                executionLog.Log("ActivitiesTasksManagement", "Enter Subject for the meeting");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject);

                executionLog.Log("ActivitiesTasksManagement", "Enter start date.");
                officeActivities_TasksHelper.TypeText("StartDate", "12/29/2016");

                executionLog.Log("ActivitiesTasksManagement", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "12/20/2016");

                executionLog.Log("ActivitiesTasksManagement", "Select Related To");
                officeActivities_TasksHelper.SelectByText("SelectRelatedTo", "Client");

                executionLog.Log("ActivitiesTasksManagement", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("ActivitiesTasksManagement", "Enter start date");
                officeActivities_TasksHelper.TypeText("StartDate", "12/29/2017");

                executionLog.Log("ActivitiesTasksManagement", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "12/29/2017");

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("ActivitiesTasksManagement", "verify page text");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

                executionLog.Log("ActivitiesTasksManagement", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click on any client.");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Select actitivity type as tasks.");
                office_ClientsHelper.Select("SelectActivityType", "Tasks");

                executionLog.Log("ActivitiesTasksManagement", "Enter task name to be search.");
                office_ClientsHelper.TypeText("ActivitySubject", Subject);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Verify created task present on client page.");
                office_ClientsHelper.IsElementPresent("OpenFirstActivity");

                executionLog.Log("ActivitiesTasksManagement", "Redirect at meetings page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify page title.");
                VerifyTitle("Edit Task");

                executionLog.Log("ActivitiesTasksManagement", "Edit end Date");
                officeActivities_TasksHelper.TypeText("DueDate", "02/02/2017");

                executionLog.Log("ActivitiesTasksManagement", "Select Related To");
                officeActivities_TasksHelper.SelectByText("SelectRelatedTo", "Lead");

                executionLog.Log("ActivitiesTasksManagement", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("ActivitiesTasksManagement", "Wait for updation success.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("ActivitiesTasksManagement", "Redirect at leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On any lead.");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Select actitivity type as tasks.");
                office_LeadsHelper.Select("SelectActivityType", "Tasks");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Enter tasks name to be search.");
                office_LeadsHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("CreatedByField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Verify created task present on leads page.");
                office_LeadsHelper.IsElementPresent("ClickNotes1");

                executionLog.Log("ActivitiesTasksManagement", "Redirect at meetings page.");
                VisitOffice("tasks");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify page title.");
                VerifyTitle("Edit Task");
                

                executionLog.Log("ActivitiesTasksManagement", "Select Related To");
                officeActivities_TasksHelper.SelectByText("SelectRelatedTo", "Opportunity");

                executionLog.Log("ActivitiesTasksManagement", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on opportunity for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Wait for updation success message.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("ActivitiesTasksManagement", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On any opportunity.");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Select actitivity type as meetings");
                office_LeadsHelper.Select("SelectActivityType", "Tasks");

                executionLog.Log("ActivitiesTasksManagement", "Enter tasks name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify created task present on opportunity page");
                office_OpportunitiesHelper.IsElementPresent("OpenOpportunity");

                executionLog.Log("ActivitiesTasksManagement", "Redirect at tasks page.");
                VisitOffice("tasks");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Select All in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify page title.");
                VerifyTitle("Edit Task");
                //office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("ActivitiesTasksManagement", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "36");

                executionLog.Log("ActivitiesTasksManagement", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Click on ticket for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("ActivitiesTasksManagement", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("ActivitiesTasksManagement", "Wait for success message.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("ActivitiesTasksManagement", "Redirect at tickets page.");
                VisitOffice("tickets");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On any ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Select actitivity type as tasks");
                office_LeadsHelper.Select("SelectActivityType", "Tasks");

                executionLog.Log("ActivitiesTasksManagement", "Enter task name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", Subject);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify created task present on page.");
                ticket_CreateATicketHelper.IsElementPresent("OpenTicket");

                executionLog.Log("ActivitiesTasksManagement", "Redirect at clients page.");
                VisitOffice("tasks");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesTasksManagement", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("DeleteClientTask");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("ActivitiesTasksManagement", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

                executionLog.Log("ActivitiesTasksManagement", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("RecycleTask");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Verify page title");
                VerifyTitle("Recycled Tasks");

                executionLog.Log("ActivitiesTasksManagement", "Search task by name.");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesTasksManagement", "Click On restore task");
                officeActivities_TasksHelper.ClickElement("DeleteTaskRecy");

                executionLog.Log("ActivitiesTasksManagement", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("ActivitiesTasksManagement", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesTasksManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Tasks Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Tasks Management", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Tasks Management");
                        TakeScreenshot("ActivitiesTasksManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesTasksManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesTasksManagement");
                        string id = loginHelper.getIssueID("Activities Tasks Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesTasksManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Tasks Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Tasks Management");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ActivitiesTasksManagement");
                executionLog.WriteInExcel("Activities Tasks Management", Status, JIRA, "Office Activities");
            }
        }
    }
}

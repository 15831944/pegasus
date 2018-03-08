using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTaskCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyTaskCreatedAndModifiedByCredits()
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
            var Subject = "Task" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Redirect at tasks page.");
                VisitOffice("tasks");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On create button");
                officeActivities_TasksHelper.ClickElement("Create");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Create a Task");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ErrorDuedate", "This field is required.");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Enter Subject for the meeting");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Enter start date.");
                officeActivities_TasksHelper.TypeText("StartDate", "01/27/2017");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "01/03/2017");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "20");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Enter start date");
                officeActivities_TasksHelper.TypeText("StartDate", "01/03/2017");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "01/27/2017");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "verify page text");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 05);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select 'All' type at owner field.");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click on searched Task");
                officeActivities_TasksHelper.ClickElement("ClickTask1");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify task created by creadits");
                officeActivities_TasksHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select 'All' type at owner field.");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click on Edit");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Edit Task");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "14");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Edit end Date");
                officeActivities_TasksHelper.TypeText("DueDate", "01/24/2017");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Wait for updation success.");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Redirect at meetings page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select 'All' type at owner field.");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click on searched Task");
                officeActivities_TasksHelper.ClickElement("ClickTask1");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify task created by creadits");
                officeActivities_TasksHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify task modified by creadits");
                officeActivities_TasksHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Redirect at clients page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select 'All' type at owner field.");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("DeleteClientTask");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("RecycleTask");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify page title");
                VerifyTitle("Recycled Tasks");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Search task by name.");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Select 'All' type at owner field.");
                officeActivities_TasksHelper.SelectByText("OwnerFieldRecycle", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Click On restore task");
                officeActivities_TasksHelper.ClickElement("DeleteTaskRecy");

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyTaskCreatedAndModifiedByCredits", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTaskCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Task Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Task Created And Modified By Credits", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Task Created And Modified By Credits");
                        TakeScreenshot("VerifyTaskCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTaskCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTaskCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Task Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTaskCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Task Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Task Created And Modified By Credits");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTaskCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Task Created And Modified By Credits", Status, JIRA, "Office Activities");
            }
        }
    }
}
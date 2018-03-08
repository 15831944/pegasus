using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyModifiedByTaskIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyModifiedByTaskIssue()
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
                executionLog.Log("VerifyModifiedByTaskIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyModifiedByTaskIssue", "Redirect at tasks page.");
                VisitOffice("tasks");

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On create button");
                officeActivities_TasksHelper.ClickElement("Create");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify page title.");
                VerifyTitle("Create a Task");

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ErrorDuedate", "This field is required.");

                executionLog.Log("VerifyModifiedByTaskIssue", "Enter Subject for the meeting");
                officeActivities_TasksHelper.TypeText("Subjuct1", Subject);

                executionLog.Log("VerifyModifiedByTaskIssue", "Enter start date.");
                officeActivities_TasksHelper.TypeText("StartDate", "01/08/2017");

                executionLog.Log("VerifyModifiedByTaskIssue", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "01/07/2017");

                executionLog.Log("VerifyModifiedByTaskIssue", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "20");

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On Save button");
                officeActivities_TasksHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On Save button");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyModifiedByTaskIssue", "Enter start date");
                officeActivities_TasksHelper.TypeText("StartDate", "01/07/2017");

                executionLog.Log("VerifyModifiedByTaskIssue", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "01/08/2017");

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyModifiedByTaskIssue", "verify page text");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 10);

                executionLog.Log("VerifyModifiedByTaskIssue", "Redirect at meetings page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Select 'All' in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click on searched Task");
                officeActivities_TasksHelper.ClickElement("ClickTask1");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify task created by creadits");
                officeActivities_TasksHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify task modified by creadits");
                officeActivities_TasksHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyModifiedByTaskIssue", "Redirect at clients page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Select 'All' in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("DeleteClientTask");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("RecycleTask");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify page title");
                VerifyTitle("Recycled Tasks");


                executionLog.Log("VerifyModifiedByTaskIssue", "Search task by name.");
                officeActivities_TasksHelper.TypeText("EnterSubject", Subject);
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Select 'All' in owner field");
                officeActivities_TasksHelper.SelectByText("OwnerFieldRecycle", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyModifiedByTaskIssue", "Click On restore task");
                officeActivities_TasksHelper.ClickElement("DeleteTaskRecy");

                executionLog.Log("VerifyModifiedByTaskIssue", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyModifiedByTaskIssue", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyModifiedByTaskIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Modified By Task Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Modified By Task Issue", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Modified By Task Issue");
                        TakeScreenshot("VerifyModifiedByTaskIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByTaskIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyModifiedByTaskIssue");
                        string id = loginHelper.getIssueID("Verify Modified By Task Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyModifiedByTaskIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Modified By Task Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Modified By Task Issue");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyModifiedByTaskIssue");
                executionLog.WriteInExcel("Verify Modified By Task Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForTasks : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForTasks()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var task = "Task" + RandomNumber(99, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Redirect at tasks page.");
                VisitOffice("tasks/create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify page title.");
                VerifyTitle("Create a Task");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify validation text for mandatoryness.");
                officeActivities_TasksHelper.VerifyText("ErrorDuedate", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Enter Subject for the meeting");
                officeActivities_TasksHelper.TypeText("Subjuct1", task);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Enter start date.");
                officeActivities_TasksHelper.TypeText("StartDate", "08/08/2017");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "07/07/2017");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select Related To");
                officeActivities_TasksHelper.Select("SelectRelatedTo", "20");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On find list icon");
                officeActivities_TasksHelper.ClickElement("SelectAssignedTo");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click on client for which task is created.");
                officeActivities_TasksHelper.ClickElement("ClickToSelectClient");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Save button");
                officeActivities_TasksHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time.");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Save button");
                officeActivities_TasksHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Enter start date");
                officeActivities_TasksHelper.TypeText("StartDate", "07/07/2017");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Enter End Date.");
                officeActivities_TasksHelper.TypeText("DueDate", "08/08/2017");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Save button");
                officeActivities_TasksHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForTasks", "verify page text");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 05);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", task);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select All in Owner Field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click on any task .");
                officeActivities_TasksHelper.ClickElement("ClickOnTaskEdit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for task status.");
                officeActivities_TasksHelper.VerifyText("VerifyStatus", "Not Started");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for task  priority.");
                officeActivities_TasksHelper.VerifyText("VerifyPriority", "Medium");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for task category.");
                officeActivities_TasksHelper.VerifyText("TaskCategory", "Select Category");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for task responsibility.");
                officeActivities_TasksHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click on edit button.");
                officeActivities_TasksHelper.ClickElement("ClickEditBtn");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select status for task.");
                officeActivities_TasksHelper.SelectByText("SelStatus", "In Progress");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select priority for task.");
                officeActivities_TasksHelper.SelectByText("SelPriority", "High");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select category for task.");
                officeActivities_TasksHelper.SelectByText("SelectCategory", "Personal");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select assigned owner for task.");
                officeActivities_TasksHelper.SelectByText("SelOwner", "Howard Tang");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click on save button.");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyQuickLookLabelsForTasks", "Wait for locator to present.");
                //officeActivities_TasksHelper.WaitForElementPresent("VerifyStatus", 10);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for meeting status.");
                officeActivities_TasksHelper.VerifyText("VerifyStatus", "In Progress");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for meeting  priority.");
                officeActivities_TasksHelper.VerifyText("VerifyPriority", "High");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for meeting category.");
                officeActivities_TasksHelper.VerifyText("TaskCategory", "Personal");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify label for meeting responsibility.");
                officeActivities_TasksHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Redirect at clients page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Search task by subject");
                officeActivities_TasksHelper.TypeText("EnterSubject", task);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select All in Owner Field");
                officeActivities_TasksHelper.SelectByText("OwnerField", "All");
                officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Select first task");
                officeActivities_TasksHelper.ClickElement("CheckBox1");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On delete.");
                officeActivities_TasksHelper.ClickElement("DeleteClientTask");
                officeActivities_TasksHelper.AcceptAlert();
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task deleted successfully.", 10);
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On Recycle Bin");
                officeActivities_TasksHelper.ClickElement("RecycleTask");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify page title");
                VerifyTitle("Recycled Tasks");
                //officeActivities_TasksHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Search task by name.");
                officeActivities_TasksHelper.TypeText("EnterSubject", task);
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                officeActivities_TasksHelper.SelectByText("OwnerFieldRecycle", "All");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Click On restore task");
                officeActivities_TasksHelper.ClickElement("DeleteTaskRecy");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Accept alert message.");
                officeActivities_TasksHelper.AcceptAlert();
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForTasks", "Verify text.");
                officeActivities_TasksHelper.WaitForText("Task Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForTasks");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForTasks");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyQuickLookLabelsForTasks", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForTasks");
                        TakeScreenshot("VerifyQuickLookLabelsForTasks");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForTasks");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForTasks");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForTasks"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForTasks");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForTasks");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForTasks", Status, JIRA, "Activities Management");
            }
        }
    }
}



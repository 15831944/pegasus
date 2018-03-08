using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class TaskAndMeeting : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void taskAndMeeting()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Task = "New" + RandomNumber(99, 99999);
            var Meeting = "Meeting" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TaskAndMeeting", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("TaskAndMeeting", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TaskAndMeeting", "Redirect at create task page.");
                VisitOffice("tasks/create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                //executionLog.Log("TaskAndMeeting", "Wait for locator to present.");
                //officeActivities_MeetingHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("TaskAndMeeting", "Click on Save");
                officeActivities_TasksHelper.ClickElement("Save");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Verify Validation");
                officeActivities_TasksHelper.VerifyText("ValdatiponForSujectField", "This field is required.");

                executionLog.Log("TaskAndMeeting", "Enter Sucject");
                officeActivities_TasksHelper.TypeText("Subjuct1", Task);

                executionLog.Log("TaskAndMeeting", " Click Save");
                officeActivities_TasksHelper.ClickElement("Save");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Verify Validation");
                officeActivities_TasksHelper.VerifyText("Valdationforstartdate", "This field is required.");

                executionLog.Log("TaskAndMeeting", "Enter Start date");
                officeActivities_TasksHelper.TypeText("StartDate", "05/05/2018");

                executionLog.Log("TaskAndMeeting", "Enter Due date");
                officeActivities_TasksHelper.TypeText("DueDate", "06/06/2018");

                executionLog.Log("TaskAndMeeting", "Click Save");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Verify Confrmation");
                officeActivities_TasksHelper.WaitForText("Task saved successfully.", 05);

                executionLog.Log("TaskAndMeeting", "Rediret to task");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Search Task");
                officeActivities_TasksHelper.TypeText("EnterSubject", Task);
                officeActivities_TasksHelper.WaitForWorkAround(2000);
                officeActivities_MeetingHelper.selectOwner("//*[@id='gs_user_name']");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Edit Task");
                officeActivities_TasksHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Save");
                officeActivities_TasksHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Verify Confrmation");
                officeActivities_TasksHelper.WaitForText("Task Updated Success.", 10);

                executionLog.Log("TaskAndMeeting", "Rediected to Meetings page ");
                VisitOffice("meetings/create");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Enter Subject");
                officeActivities_MeetingHelper.TypeText("Subject", Meeting);

                executionLog.Log("TaskAndMeeting", "Start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "06/06/2018");

                executionLog.Log("TaskAndMeeting", "End date");
                officeActivities_MeetingHelper.TypeText("EndDate", "07/07/2018");

                executionLog.Log("TaskAndMeeting", "Select Client");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");
                officeActivities_TasksHelper.WaitForWorkAround(1000);

                executionLog.Log("TaskAndMeeting", "Click Assigned To");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Click Assigned To");
                officeActivities_MeetingHelper.ClickElement("SelectedClient");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Click Save");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Wait for confirmation.");
                officeActivities_TasksHelper.WaitForText("Meeting saved successfully. ", 10);

                executionLog.Log("TaskAndMeeting", "Redirect to meeting");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Search Task");
                officeActivities_MeetingHelper.TypeText("SearchSubject", Meeting);
                officeActivities_TasksHelper.WaitForWorkAround(2000);
                officeActivities_MeetingHelper.selectOwner("//select[@id='gs_owner']");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TaskAndMeeting", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Click on Save");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TaskAndMeeting", "Wait for success message.");
                officeActivities_TasksHelper.WaitForText("Meeting updated successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TaskAndMeeting");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("TaskAndMeeting");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("TaskAndMeeting", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("TaskAndMeeting");
                        TakeScreenshot("TaskAndMeeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskAndMeeting.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TaskAndMeeting");
                        string id = loginHelper.getIssueID("TaskAndMeeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TaskAndMeeting.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("TaskAndMeeting"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("TaskAndMeeting");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TaskAndMeeting");
                executionLog.WriteInExcel("TaskAndMeeting", Status, JIRA, "Office Activities.");
            }
        }
    }
}
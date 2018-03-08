using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LabelSelectforTask : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void labelSelectforTask()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var OfficeActivities_TasksHelper = new OfficeActivities_TasksHelper(GetWebDriver());

            var TaskName = "Testtask" + RandomNumber(1, 100);


            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("LabelSelectforTask", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LabelSelectforTask", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LabelSelectforTask", "Click on Activities >> Task");
                VisitOffice("tasks");
                OfficeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelSelectforTask", "Click on create button");
                OfficeActivities_TasksHelper.ClickElement("Create");
                OfficeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelSelectforTask", "Enter the subject Name");
                OfficeActivities_TasksHelper.TypeText("Subjuct1", TaskName);

                executionLog.Log("LabelSelectforTask", "Enter the Start Date");
                OfficeActivities_TasksHelper.TypeText("StartDate", "01/02/2017");

                executionLog.Log("LabelSelectforTask", "Enter the Due date");
                OfficeActivities_TasksHelper.TypeText("DueDate", "01/06/2017");

                executionLog.Log("LabelSelectforTask", "Click on Save btn");
                OfficeActivities_TasksHelper.ClickElement("Save");
                OfficeActivities_TasksHelper.WaitForWorkAround(5000);

                VisitOffice("tasks");
                OfficeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelSelectforTask", "Search the same task");
                OfficeActivities_TasksHelper.TypeText("EnterSubject", TaskName);
                OfficeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("LabelSelectforTask", "Click on any Task");
                OfficeActivities_TasksHelper.ClickElement("ClickTask1");

                executionLog.Log("LabelSelectforTask", "Verify Select for category");
                OfficeActivities_TasksHelper.VerifyText("TaskCategory", "Select");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LabelSelectforTask");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Label Select for Task");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Label Select for Task", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Label Select for Task");
                        TakeScreenshot("LabelSelectforTask");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelSelectforTask.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LabelSelectforTask");
                        string id = loginHelper.getIssueID("Label Select for Task");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LabelSelectforTask.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Label Select for Task"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Label Select for Task");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LabelSelectforTask");
                executionLog.WriteInExcel("Label Select for Task", Status, JIRA, "Office Activities");
            }
        }
    }
}
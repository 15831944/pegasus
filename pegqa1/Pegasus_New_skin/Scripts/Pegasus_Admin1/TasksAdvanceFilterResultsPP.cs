using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TasksAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void tasksAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("TasksAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("TasksAdvanceFilterResultsPP", "Redirect To URL");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify page title.");
                VerifyTitle("Tasks");

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Select number of records to 10.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "10");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                //officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_TasksHelper.ShowResult(10);
                // officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 10 of");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Select number of records to 20.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "20");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                //officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_TasksHelper.ShowResult(20);
                //officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 20 of");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Select number of records to 50.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "50");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                //officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_TasksHelper.ShowResult(50);
                // officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 50 of");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(2000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Select number of records to 100.");
                officeActivities_TasksHelper.SelectByText("ResultsPerPage", "100");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);
                //officeActivities_TasksHelper.WaitForElementPresent("No.ofResults", 10);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Verify number of records displayed.");
                officeActivities_TasksHelper.ShowResult(100);
                // officeActivities_TasksHelper.VerifyText("No.ofResults", "Showing 1 - 100 of");
                //officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("TasksAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TasksAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Tasks Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Tasks Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Tasks Advance Filter ResultsPP");
                        TakeScreenshot("TasksAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TasksAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TasksAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Tasks Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TasksAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Tasks Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Tasks Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TasksAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Tasks Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
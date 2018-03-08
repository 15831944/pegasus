using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTasksAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyTasksAdvanceFilerColumnOrder()
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

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Tasks");

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify status column is visible on the page..");
                officeActivities_TasksHelper.IsElementPresent("HeadStatus");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify priority column is visible on the page.");
                officeActivities_TasksHelper.IsElementPresent("HeadPriority");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify owner column is visible on the page.");
                officeActivities_TasksHelper.IsElementPresent("HeadOwner");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify Modified column is visible on the page.");
                officeActivities_TasksHelper.IsElementPresent("HeadModified");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select status in displayed columns.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Status");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeActivities_TasksHelper.ClickElement("RemoveCols");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select priority in displayed columns.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Priority");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_TasksHelper.ClickElement("RemoveCols");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select owner in displayed columns.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Owner");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_TasksHelper.ClickElement("RemoveCols");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select Modified in displayed columns.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Modified");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_TasksHelper.ClickElement("RemoveCols");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify status not present on page.");
                officeActivities_TasksHelper.IsElementNotPresent("HeadStatus");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify priority not present on page.");
                officeActivities_TasksHelper.IsElementNotPresent("HeadPriority");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify owner not present on page.");
                officeActivities_TasksHelper.IsElementNotPresent("HeadOwner");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify Modified not present on page.");
                officeActivities_TasksHelper.IsElementNotPresent("HeadModified");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify page title as tasks");
                VerifyTitle("Tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify default position of status column.");
                officeActivities_TasksHelper.IsElementPresent("HeadStatus6");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify default position of prority column.");
                officeActivities_TasksHelper.IsElementPresent("HeadPriority3");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Redirect at tasks page.");
                VisitOffice("tasks");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_TasksHelper.ClickElement("AdvanceFilter");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select status in displayed column.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Status");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_TasksHelper.ClickElement("MoveUp");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_TasksHelper.ClickElement("MoveUp");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Move status 1 step up.");
                officeActivities_TasksHelper.ClickElement("MoveUp");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Select priority in displayed column.");
                officeActivities_TasksHelper.SelectByText("DisplayedCols", "Priority");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Move priority 1 step down.");
                officeActivities_TasksHelper.ClickElement("MoveDown");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_TasksHelper.ClickElement("Apply");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify changed position of status column.");
                officeActivities_TasksHelper.IsElementPresent("HeadStatus4");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Verify changed position of priority column.");
                officeActivities_TasksHelper.IsElementPresent("HeadPriority5");
                officeActivities_TasksHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTasksAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTasksAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tasks Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tasks Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
                        TakeScreenshot("VerifyTasksAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTasksAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTasksAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTasksAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tasks Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tasks Advance Filer Column Order");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTasksAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Tasks Advance Filer Column Order", Status, JIRA, "Tasks Management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifySalesAgentAdvanceFilterColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifySalesAgentAdvanceFilterColumnOrder()
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
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("sales_agents");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify status column is visible on the page..");
                agent_1099SalesAgentHelper.IsElementPresent("HeadStatus");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify role column is visible on the page.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadRole");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify department column is visible on the page.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadDepartment");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify team column is visible on the page.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadTeam");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select status in displayed columns.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Status");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols.");
                agent_1099SalesAgentHelper.ClickElement("RemoveCols");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select role in displayed columns.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Role");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agent_1099SalesAgentHelper.ClickElement("RemoveCols");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select department in displayed columns.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Department");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agent_1099SalesAgentHelper.ClickElement("RemoveCols");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select team in displayed columns.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Team");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click arrow to move column to avail cols");
                agent_1099SalesAgentHelper.ClickElement("RemoveCols");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click on Apply button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify status not present on page.");
                agent_1099SalesAgentHelper.IsElementNotPresent("HeadStatus");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify role not present on page.");
                agent_1099SalesAgentHelper.IsElementNotPresent("HeadRole");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify department not present on page.");
                agent_1099SalesAgentHelper.IsElementNotPresent("HeadDepartment");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify team not present on page.");
                agent_1099SalesAgentHelper.IsElementNotPresent("HeadTeam");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Redirect To URL");
                VisitOffice("sales_agents");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify default position of role column.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadRole5");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify default position of department column.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadDepartment6");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Redirect at sales agents page.");
                VisitOffice("sales_agents");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select role in displayed column.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Role");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Move role 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Move role 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Move role 1 step up.");
                agent_1099SalesAgentHelper.ClickElement("MoveUp");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Select department in displayed column.");
                agent_1099SalesAgentHelper.SelectByText("DisplayedCols", "Department");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Move department 1 step down.");
                agent_1099SalesAgentHelper.ClickElement("MoveDown");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Click on Apply button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify changed position of role column.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadRole3");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Verify changed position of department column.");
                agent_1099SalesAgentHelper.IsElementPresent("HeadDepartment7");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifySalesAgentAdvanceFilterColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifySalesAgentAdvanceFilterColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Sales Agent Advance Filter Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Sales Agent Advance Filter Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Sales Agent Advance Filter Column Order");
                        TakeScreenshot("VerifySalesAgentAdvanceFilterColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySalesAgentAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifySalesAgentAdvanceFilterColumnOrder");
                        string id = loginHelper.getIssueID("Verify Sales Agent Advance Filter Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifySalesAgentAdvanceFilterColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Sales Agent Advance Filter Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Sales Agent Advance Filter Column Order");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifySalesAgentAdvanceFilterColumnOrder");
                executionLog.WriteInExcel("Verify Sales Agent Advance Filter Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}
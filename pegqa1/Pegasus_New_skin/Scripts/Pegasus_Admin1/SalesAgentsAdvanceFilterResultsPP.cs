using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SalesAgentsAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void salesAgentsAdvanceFilterResultsPP()
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

            // Variable
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("sales_agents");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Select number of records to 10.");
                agent_1099SalesAgentHelper.SelectByText("ResultsPerPage", "10");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on Apply button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);
                agent_1099SalesAgentHelper.WaitForElementPresent("BottomResults", 05);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agent_1099SalesAgentHelper.ShowResult(10);
                //agent_1099SalesAgentHelper.VerifyText("BottomResults", "Showing 1 - 10 of ");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Select number of records to 20.");
                agent_1099SalesAgentHelper.SelectByText("ResultsPerPage", "20");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on Apply button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);
                agent_1099SalesAgentHelper.WaitForElementPresent("BottomResults", 05);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agent_1099SalesAgentHelper.ShowResult(20);
                //agent_1099SalesAgentHelper.VerifyText("BottomResults", "Showing 1 - 20 of ");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Select number of records to 50.");
                agent_1099SalesAgentHelper.SelectByText("ResultsPerPage", "50");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);
                agent_1099SalesAgentHelper.WaitForElementPresent("BottomResults", 05);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agent_1099SalesAgentHelper.ShowResult(50);
                //agent_1099SalesAgentHelper.VerifyText("BottomResults", "Showing 1 - 50 of ");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on advance filter.");
                agent_1099SalesAgentHelper.ClickElement("AdvanceFilter");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Select number of records to 100.");
                agent_1099SalesAgentHelper.SelectByText("ResultsPerPage", "100");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Click on ApplyButton button.");
                agent_1099SalesAgentHelper.ClickElement("ApplyButton");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);
                agent_1099SalesAgentHelper.WaitForElementPresent("BottomResults", 05);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Verify number of records displayed.");
                agent_1099SalesAgentHelper.ShowResult(100);
                //agent_1099SalesAgentHelper.VerifyText("BottomResults", "Showing 1 - 100 of ");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesAgentsAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SalesAgentsAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sales Agents Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sales Agents Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sales Agents Advance Filter ResultsPP");
                        TakeScreenshot("SalesAgentsAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesAgentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SalesAgentsAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Sales Agents Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesAgentsAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sales Agents Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sales Agents Advance Filter ResultsPP");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SalesAgentsAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Sales Agents Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
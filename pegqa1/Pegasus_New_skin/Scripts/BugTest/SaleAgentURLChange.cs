using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaleAgentURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void saleAgentURLChange()
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
            var agent_1099SaleAagentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SaleAgentURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaleAgentURLChange", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SaleAgentURLChange", "Redirect To Sale Agent");
                VisitOffice("sales_agents");

                executionLog.Log("SaleAgentURLChange", "Click on Any Sale Agent");
                agent_1099SaleAagentHelper.ClickElement("ClickOnAgent1099");

                executionLog.Log("SaleAgentURLChange", "Change Url Number");
                VisitOffice("sales_agents/view/178");
                agent_1099SaleAagentHelper.WaitForWorkAround(4000);

                executionLog.Log("SaleAgentURLChange", "Verify Validation");
                agent_1099SaleAagentHelper.WaitForText("You don't have privilege.", 05);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgentURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sale Agent URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sale Agent URL Change", "Bug", "Medium", "Sale agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sale Agent URL Change");
                        TakeScreenshot("SaleAgentURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgentURLChange");
                        string id = loginHelper.getIssueID("Sale Agent URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgentURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sale Agent URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sale Agent URL Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgentURLChange");
                executionLog.WriteInExcel("Sale Agent URL Change", Status, JIRA, "Agents Portal");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SalesUserIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void salesUserIssue()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());

            try
            {
                executionLog.Log("SalesUserIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("SalesUserIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SalesUserIssue", "Go to Create sales user page");
                VisitOffice("sales_agents/create");
                agent_1099SalesAgentHelper.WaitForWorkAround(5000);

                executionLog.Log("SalesUserIssue", "Verify title");
                VerifyTitle("Create a 1099 Sales User");

                executionLog.Log("SalesUserIssue", "Collapse the Contact info section");
                agent_1099SalesAgentHelper.ClickElement("CollapseSignCont.Info");
                agent_1099SalesAgentHelper.WaitForWorkAround(6000);

                executionLog.Log("SalesUserIssue", "Collapse the Details section");
                agent_1099SalesAgentHelper.ClickElement("CollapseDetails");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SalesUserIssue", "Click on 'Save' button");
                agent_1099SalesAgentHelper.ClickJs("SaveSaleAgent");
                agent_1099SalesAgentHelper.WaitForWorkAround(7000);

                executionLog.Log("SalesUserIssue", "Verify title");
                VerifyTitle("Create a 1099 Sales User");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SalesUserIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Sales User Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Sales User Issue", "Bug", "Medium", "Sales User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Sales User Issue");
                        TakeScreenshot("Sales User Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesUserIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SalesUserIssue");
                        string id = loginHelper.getIssueID("Sales User Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SalesUserIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Sales User Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Sales User Issue");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SalesUserIssue");
                executionLog.WriteInExcel("Sales User Issue", Status, JIRA, "Agent Portal");
            }
        }
    }
}
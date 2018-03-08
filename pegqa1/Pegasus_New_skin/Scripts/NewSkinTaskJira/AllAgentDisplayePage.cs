using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AllAgentDisplayePage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void allAgentDisplayePage()
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
            var agents_AllAgentsHelper = new Agents_AllAgentsHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AllAgentDisplayePage", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AllAgentDisplayePage", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AllAgentDisplayePage", "Click on Agent in Topmenu");
                agents_AllAgentsHelper.ClickElement("AgentTab");
                agents_AllAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("AllAgentDisplayePage", "Select SelectUserType");
                agents_AllAgentsHelper.Select("SelectUserType", "Employee");
                agents_AllAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("AllAgentDisplayePage", "Select SelectUserType");
                agents_AllAgentsHelper.Select("SelectUserType", "1099 Sales Agent");
                agents_AllAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("AllAgentDisplayePage", "Select SelectUserType");
                agents_AllAgentsHelper.Select("SelectUserType", "Partner Agent");
                agents_AllAgentsHelper.WaitForWorkAround(2000);

                executionLog.Log("AllAgentDisplayePage", "Select SelectUserType");
                agents_AllAgentsHelper.Select("SelectUserType", "Partner Association");
                agents_AllAgentsHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AllAgentDisplayePage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("All Agent Displaye Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("All Agent Displaye Page", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("All Agent Displaye Page");
                        TakeScreenshot("AllAgentDisplayePage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AllAgentDisplayePage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AllAgentDisplayePage");
                        string id = loginHelper.getIssueID("All Agent Displaye Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AllAgentDisplayePage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("All Agent Displaye Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("All Agent Displaye Page");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AllAgentDisplayePage");
                executionLog.WriteInExcel("All Agent Displaye Page", Status, JIRA, "Agent Portal");
            }
        }
    }
}
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
    public class DisabledPartnerAgentIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void disabledPartnerAgentIssue()
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
            var agent_AllAgentHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("DisabledPartnerAgentIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DisabledPartnerAgentIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DisabledPartnerAgentIssue", "Redirect at partner agents page.");
                VisitOffice("partners/agents");
                agent_AllAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("DisabledPartnerAgentIssue", "Verify Page title");
                VerifyTitle("Partner Agents");

                executionLog.Log("DisabledPartnerAgentIssue", "Search mark matthews");
                agent_AllAgentHelper.TypeText("AgentName", "Mark Menu");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("DisabledPartnerAgentIssue", "Check the active agent");
                agent_AllAgentHelper.CheckAndClick("InActiveIcon");
                agent_AllAgentHelper.WaitForWorkAround(4000);

                executionLog.Log("DisabledPartnerAgentIssue", "Redirect at create opportunities page.");
                VisitOffice("opportunities/create");

                executionLog.Log("DisabledPartnerAgentIssue", "Wait for locator to be present.");
                office_OpportunitiesHelper.WaitForElementPresent("Responsibility", 10);

                executionLog.Log("DisabledPartnerAgentIssue", "Verify disabled Partner agent not present for selection.");
                office_OpportunitiesHelper.PAgentNotAvail();

                executionLog.Log("DisabledPartnerAgentIssue", "Redirect at partner agents page.");
                VisitOffice("partners/agents");
                agent_AllAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("DisabledPartnerAgentIssue", "Verify Page title");
                VerifyTitle("Partner Agents");

                executionLog.Log("DisabledPartnerAgentIssue", "Enter partner name to be search");
                agent_AllAgentHelper.TypeText("AgentName", "Mark Menu");
                agent_AllAgentHelper.WaitForWorkAround(2000);

                executionLog.Log("DisabledPartnerAgentIssue", "Verify partner status as inactive.");
                agent_AllAgentHelper.VerifyText("VerifyStatus", "Inactive");
                //agent_AllAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("DisabledPartnerAgentIssue", "Click to make partner agent active.");
                agent_AllAgentHelper.ClickJava("MakeActiveIcon");
                agent_AllAgentHelper.WaitForWorkAround(1000);
                agent_AllAgentHelper.AcceptAlert();

                executionLog.Log("DisabledPartnerAgentIssue", "Wait for success text.");
                agent_AllAgentHelper.WaitForText("Partner Agent is Activated.", 10);
                //agent_AllAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("DisabledPartnerAgentIssue", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DisabledPartnerAgentIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Disabled Partner Agent Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Disabled Partner Agent Issue", "Bug", "Medium", "Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Disabled Partner Agent Issue");
                        TakeScreenshot("DisabledPartnerAgentIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DisabledPartnerAgentIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DisabledPartnerAgentIssue");
                        string id = loginHelper.getIssueID("Disabled Partner Agent Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DisabledPartnerAgentIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Disabled Partner Agent Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Disabled Partner Agent Issue");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DisabledPartnerAgentIssue");
                executionLog.WriteInExcel("Disabled Partner Agent Issue", Status, JIRA, "Agent Portal");
            }
        }
    }
}
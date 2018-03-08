using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AgentCreateField : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void agentCreateField()
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
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AgentCreateField", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AgentCreateField", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AgentCreateField", "navigate to the Create partner agent page.");
                VisitOffice("partners/agent/create");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentCreateField", "verify title");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("AgentCreateField", "Click on Create an user checkbox");
                agents_PartnerAgentsHelper.scrollToElement("CreateCheck");
                agents_PartnerAgentsHelper.WaitForWorkAround(1000);

                executionLog.Log("AgentCreateField", "Click on Avatar check box. ");
                agents_PartnerAgentsHelper.ClickElement("CreateCheck");

                executionLog.Log("AgentCreateField", "Verify fields displayed");
                agents_PartnerAgentsHelper.verifyElementPresent("VerifyField");
                agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                executionLog.Log("AgentCreateField", "Verify field is displayed");
                agents_PartnerAgentsHelper.verifyElementPresent("VerifyField");
            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AgentCreateField");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Agent Create Field");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Agent Create Field", "Bug", "Medium", "Create agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Agent Create Field");
                        TakeScreenshot("AgentCreateField");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentCreateField.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AgentCreateField");
                        string id = loginHelper.getIssueID("Agent Create Field");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentCreateField.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Agent Create Field"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Agent Create Field");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AgentCreateField");
                executionLog.WriteInExcel("Agent Create Field", Status, JIRA, "Agent Portal");
            }
        }
    }
}
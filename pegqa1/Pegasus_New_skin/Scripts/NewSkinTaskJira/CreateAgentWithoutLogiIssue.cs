using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateAgentWithoutLoginIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createAgentWithoutLoginIssue()
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
                executionLog.Log("CreateAgentWithoutLoginIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateAgentWithoutLoginIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateAgentWithoutLoginIssue", "navigate to the Create partner agent page.");
                VisitOffice("partners/agent/create");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateAgentWithoutLoginIssue", "verify title");
                VerifyTitle("Create a Partner Agent");

                executionLog.Log("CreateAgentWithoutLoginIssue", "Click on Save button without checking Create a User Account For This Partner Agent checkbox");
                agents_PartnerAgentsHelper.ClickElement("SaveAgent");

                executionLog.Log("CreateAgentWithoutLoginIssue", "Verify fields displayed");
                agents_PartnerAgentsHelper.verifyElementPresent("UserNameError");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAgentWithoutLoginIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Agent Without Login Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Agent Without Login Issue", "Bug", "Medium", "Create agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Agent Without Login Issue");
                        TakeScreenshot("CreateAgentWithoutLoginIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAgentWithoutLoginIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAgentWithoutLoginIssue");
                        string id = loginHelper.getIssueID("Create Agent Without Login Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateAgentWithoutLoginIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Agent Without Login Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Agent Without Login Issue");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateAgentWithoutLoginIssue");
                executionLog.WriteInExcel("Create Agent Without Login Issue", Status, JIRA, "Agent Portal");
            }
        }
    }
}

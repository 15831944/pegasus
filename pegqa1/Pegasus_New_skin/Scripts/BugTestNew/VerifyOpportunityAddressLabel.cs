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
    public class OpportunityLabelAgentBlankSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void opportunityLabelAgentBlankSave()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // Random Variable.
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("OpportunityLabelAgentBlankSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunityLabelAgentBlankSave", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunityLabelAgentBlankSave", "Redirect at opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("OpportunityLabelAgentBlankSave", "Click on any Opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunities1");

                executionLog.Log("OpportunityLabelAgentBlankSave", "Click to add partner agent");
                office_OpportunitiesHelper.DoubleClick("//*[@id='partner']");
                office_OpportunitiesHelper.WaitForWorkAround(1000);

                executionLog.Log("OpportunityLabelAgentBlankSave", "Click on save");
                office_OpportunitiesHelper.ClickElement("SaveQuicklook");

                executionLog.Log("OpportunityLabelAgentBlankSave", "Verify label for Partner Agent");
                office_OpportunitiesHelper.VerifyText("VerifyLabel", "Select");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunityLabelAgentBlankSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunity Label Agent Blank Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunity Label Agent Blank Save", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunity Label Agent Blank Save");
                        TakeScreenshot("OpportunityLabelAgentBlankSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityLabelAgentBlankSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunityLabelAgentBlankSave");
                        string id = loginHelper.getIssueID("Opportunity Label Agent Blank Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityLabelAgentBlankSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunity Label Agent Blank Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunity Label Agent Blank Save");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunityLabelAgentBlankSave");
                executionLog.WriteInExcel("Opportunity Label Agent Blank Save", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
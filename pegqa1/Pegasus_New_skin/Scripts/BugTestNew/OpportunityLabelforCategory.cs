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
    public class OpportunityLabelforCategory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void opportunityLabelforCategory()
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

                executionLog.Log("OpportunityLabelforCategory", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("OpportunityLabelforCategory", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunityLabelforCategory", "Redirect at opportunities page.");
                VisitOffice("opportunities");

                executionLog.Log("OpportunityLabelforCategory", "Click on any Opportunity");
                office_OpportunitiesHelper.ClickElement("Opportunities1");

                executionLog.Log("OpportunityLabelforCategory", "Verify label for category");
                office_OpportunitiesHelper.VerifyText("VerifyCategory", "Select");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunityLabelforCategory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Opportunity Label for Category");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunity Label for Category", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Opportunity Label for Category");
                        TakeScreenshot("OpportunityLabelforCategory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityLabelforCategory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunityLabelforCategory");
                        string id = loginHelper.getIssueID("Opportunity Label for Category");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityLabelforCategory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Opportunity Label for Category"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Opportunity Label for Category");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunityLabelforCategory");
                executionLog.WriteInExcel("Opportunity Label for Category", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
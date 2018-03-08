using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOpportunityCreateFormTabs : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void verifyOpportunityCreateFormTabs()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyOpportunityCreateFormTabs", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify page title as create opportunity ");
                VerifyTitle("Create an Opportunity");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify section details present");
                office_OpportunitiesHelper.verifyElementPresent("Details");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify label opportunity name present.");
                office_OpportunitiesHelper.verifyElementPresent("OpportunityName");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify label company name present.");
                office_OpportunitiesHelper.verifyElementPresent("CompanyName");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify assignment section present.");
                office_OpportunitiesHelper.verifyElementPresent("HAssignments");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify label status present.");
                office_OpportunitiesHelper.verifyElementPresent("LabelStatus");

                executionLog.Log("VerifyOpportunityCreateFormTabs", "Verify label responsibility present.");
                office_OpportunitiesHelper.verifyElementPresent("LabelResponsibility");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunityCreateFormTabs");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyOpportunityCreateFormTabs");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyOpportunityCreateFormTabs");
                        TakeScreenshot("VerifyOpportunityCreateFormTabs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityCreateFormTabs.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunityCreateFormTabs");
                        string id = loginHelper.getIssueID("VerifyOpportunityCreateFormTabs");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityCreateFormTabs.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyOpportunityCreateFormTabs"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyOpportunityCreateFormTabs");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunityCreateFormTabs");
                executionLog.WriteInExcel("VerifyOpportunityCreateFormTabs", Status, JIRA, "Opportunity management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OpportunityToLeadConversionIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void opportunityToLeadConversionIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("OpportunityToLeadConversionIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("OpportunityToLeadConversionIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunityToLeadConversionIssue", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("OpportunityToLeadConversionIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunityToLeadConversionIssue", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("OpportunityToLeadConversionIssue", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("OpportunityToLeadConversionIssue", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("OpportunityToLeadConversionIssue", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("OpportunityToLeadConversionIssue", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("OpportunityToLeadConversionIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunityToLeadConversionIssue", "Wait for success message");
                office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                executionLog.Log("OpportunityToLeadConversionIssue", "Click on convert.");
                office_OpportunitiesHelper.ClickElement("Convert");

                executionLog.Log("OpportunityToLeadConversionIssue", "Click on radio button lead.");
                office_OpportunitiesHelper.ClickElement("LeadRadio");

                executionLog.Log("OpportunityToLeadConversionIssue", "Click on save button.");
                office_OpportunitiesHelper.clickJS("SaveConfirm");
                office_OpportunitiesHelper.WaitForWorkAround(5000);

                executionLog.Log("OpportunityToLeadConversionIssue", "Verify converted name present on lead page.");
                office_OpportunitiesHelper.VerifyName(Oppname);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunityToLeadConversionIssue", "Verify converted DBA name present on the page.");
                office_OpportunitiesHelper.VerifyCompName(CDBA);
                office_OpportunitiesHelper.WaitForWorkAround(4000);

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunityToLeadConversionIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("OpportunityToLeadConversionIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("OpportunityToLeadConversionIssue");
                        TakeScreenshot("OpportunityToLeadConversionIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityToLeadConversionIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunityToLeadConversionIssue");
                        string id = loginHelper.getIssueID("OpportunityToLeadConversionIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityToLeadConversionIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("OpportunityToLeadConversionIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("OpportunityToLeadConversionIssue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunityToLeadConversionIssue");
                executionLog.WriteInExcel("OpportunityToLeadConversionIssue", Status, JIRA, "Opportunities management");
            }
        }
    }
}
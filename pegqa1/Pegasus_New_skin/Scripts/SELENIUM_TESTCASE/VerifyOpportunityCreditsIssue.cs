using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOpportunityCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS3")]
        public void verifyOpportunityCreditsIssue()
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

                executionLog.Log("VerifyOpportunityCreditsIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyOpportunityCreditsIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunityCreditsIssue", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("VerifyOpportunityCreditsIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityCreditsIssue", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("VerifyOpportunityCreditsIssue", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("VerifyOpportunityCreditsIssue", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyOpportunityCreditsIssue", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("VerifyOpportunityCreditsIssue", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyOpportunityCreditsIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Opportunities']";
                if (office_OpportunitiesHelper.IsElementPresent(loc))
                {

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click Dublicate Button");
                    office_OpportunitiesHelper.ClickOnDisplayed("ClickOnDubBtn");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Redirect at Opportunities");
                    VisitOffice("opportunities");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click on  1st Opportunities");
                    office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click On Merge Records");
                    office_OpportunitiesHelper.ClickElement("DeleteLink");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait for confirmation");
                    office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Redirect at recyclebin Page.");
                    VisitOffice("opportunities/recyclebin");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify page title.");
                    VerifyTitle("Recycled Opportunities");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click on delete icon.");
                    office_OpportunitiesHelper.ClickElement("DeletePermanaently");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait For Confirmation");
                    office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);

                }
                else
                {
                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait for success message");
                    office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify opportunity created by");
                    office_OpportunitiesHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify opportunity modified by");
                    office_OpportunitiesHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Redirect at Opportunities");
                    VisitOffice("opportunities");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click on  1st Opportunities");
                    office_OpportunitiesHelper.ClickElement("ClickOn1stOpp");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click On Merge Records");
                    office_OpportunitiesHelper.ClickElement("DeleteLink");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait for confirmation");
                    office_OpportunitiesHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Redirect at recyclebin Page.");
                    VisitOffice("opportunities/recyclebin");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Verify page title.");
                    VerifyTitle("Recycled Opportunities");

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Click on delete icon.");
                    office_OpportunitiesHelper.ClickElement("DeletePermanaently");
                    office_OpportunitiesHelper.AcceptAlert();

                    executionLog.Log("VerifyOpportunityCreditsIssue", "Wait For Confirmation");
                    office_OpportunitiesHelper.WaitForText("Opportunity permanently deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunityCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyOpportunityCreditsIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyOpportunityCreditsIssue", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyOpportunityCreditsIssue");
                        TakeScreenshot("VerifyOpportunityCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunityCreditsIssue");
                        string id = loginHelper.getIssueID("VerifyOpportunityCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Opportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyOpportunityCreditsIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyOpportunityCreditsIssue");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunityCreditsIssue");
                executionLog.WriteInExcel("VerifyOpportunityCreditsIssue", Status, JIRA, "Opportunity management");
            }
        }
    }
}
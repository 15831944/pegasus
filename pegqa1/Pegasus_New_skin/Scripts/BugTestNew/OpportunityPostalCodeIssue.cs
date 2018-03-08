using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class OpportunityPostalCodeIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void opportunityPostalCodeIssue()
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
                executionLog.Log("OpportunityPostalCodeIssue", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("OpportunityPostalCodeIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("OpportunityPostalCodeIssue", "Create Opportunities");
                VisitOffice("opportunities/create");

                executionLog.Log("OpportunityPostalCodeIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("OpportunityPostalCodeIssue", "Verify text on page.");
                office_OpportunitiesHelper.VerifyText("RequiredFieldsOpp", "This field is required.");

                executionLog.Log("OpportunityPostalCodeIssue", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("OpportunityPostalCodeIssue", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("OpportunityPostalCodeIssue", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("OpportunityPostalCodeIssue", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("OpportunityPostalCodeIssue", "ENter Zip Code");
                office_OpportunitiesHelper.TypeText("ZipCode", "60601");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunityPostalCodeIssue", "Enter address line1");
                office_OpportunitiesHelper.TypeText("AddressLine1Opp", "Test");

                executionLog.Log("OpportunityPostalCodeIssue", "Click on Save");
                office_OpportunitiesHelper.ClickElement("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunityPostalCodeIssue", "Wait for success message");
                office_OpportunitiesHelper.WaitForText("Opportunity saved successfully.", 10);
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("OpportunityPostalCodeIssue", "Wait for success message");
                office_OpportunitiesHelper.VerifyPageText("60601");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("OpportunityPostalCodeIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("OpportunityPostalCodeIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Opportunity page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("OpportunityPostalCodeIssue");
                        TakeScreenshot("OpportunityPostalCodeIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityPostalCodeIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("OpportunityPostalCodeIssue");
                        string id = loginHelper.getIssueID("OpportunityPostalCodeIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\OpportunityPostalCodeIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("OpportunityPostalCodeIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("OpportunityPostalCodeIssue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("OpportunityPostalCodeIssue");
                executionLog.WriteInExcel("OpportunityPostalCodeIssue", Status, JIRA, "Opportunity management");
            }
        }
    }
}
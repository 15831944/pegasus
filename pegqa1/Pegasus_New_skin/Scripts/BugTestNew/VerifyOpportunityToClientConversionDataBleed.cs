using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyOpportunityToClientConversionDataBleed : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("BugTestNew")]
        public void verifyOpportunityToClientConversionDataBleed()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Oppname = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Create Opportunities");
                VisitOffice("opportunities/create");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Enter Opportunity Name");
                office_OpportunitiesHelper.TypeText("Name", Oppname);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Enter Company DBA Name");
                office_OpportunitiesHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Select Opp Status");
                office_OpportunitiesHelper.SelectByText("State", "New");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Select Opp Responsibility");
                office_OpportunitiesHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on Save");
                office_OpportunitiesHelper.clickJS("SaveOpp");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on convert button");
                office_OpportunitiesHelper.ClickElement("Convert");
                office_OpportunitiesHelper.WaitForWorkAround(500);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on client radio button");
                office_OpportunitiesHelper.ClickElement("ClientRadio");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on No Recycle Bin");
                office_OpportunitiesHelper.ClickElement("RecycleNo");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on save button.");
                office_OpportunitiesHelper.clickJS("SaveConfirm");
                office_OpportunitiesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on Company Details tab");
                office_ClientsHelper.ClickElement("CompanyDetailsTab");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Verify Legal Name is blank");
                Assert.AreEqual(office_ClientsHelper.getInputText("//input[@id='ClientDetailCompanyLegalName']"),"");

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Click on Merchant Numbers tab");
                office_ClientsHelper.ClickElement("MerchantNumber");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyOpportunityToClientConversionDataBleed", "Verify Merchant ID is blank");
                Assert.AreEqual(office_ClientsHelper.getInputText("//input[@id='ClientDetailMerchID']"),"");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyOpportunityToClientConversionDataBleed");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Opportunity To Client Conversion Data Bleed");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Opportunity To Client Conversion Data Bleed", "Bug", "Medium", "Terminals and Equipment tab", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Opportunity To Client Conversion Data Bleed");
                        TakeScreenshot("VerifyOpportunityToClientConversionDataBleed");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityToClientConversionDataBleed.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyOpportunityToClientConversionDataBleed");
                        string id = loginHelper.getIssueID("Verify Opportunity To Client Conversion Data Bleed");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyOpportunityToClientConversionDataBleed.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Opportunity To Client Conversion Data Bleed"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Opportunity To Client Conversion Data Bleed");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyOpportunityToClientConversionDataBleed");
                executionLog.WriteInExcel("Verify Opportunity To Client Conversion Data Bleed", Status, JIRA, "Office Opportunity");
            }
        }
    }
}
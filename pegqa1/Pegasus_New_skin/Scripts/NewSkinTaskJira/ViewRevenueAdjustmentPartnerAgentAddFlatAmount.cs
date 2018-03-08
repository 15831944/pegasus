using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ViewRevenueAdjustmentPartnerAgentAddFlatAmount : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void viewRevenueAdjustmentPartnerAgentAddFlatAmount()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            String JIRA = "";
            String Status = "Pass";

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Variable random
            var name = "PAgent" + RandomNumber(1, 999);
            var Email = "Part" + RandomNumber(1, 999) + "@yopmail.com";
            var Adjustment = "Adjustment" + GetRandomNumber();


            try
            {
                executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at Partner Agent page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select status of agent..");
                agents_PartnerAgentsHelper.Select("SelectStatusAdjtmnt", "");

                executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Search the 'Partner Agent Tester");
                agents_PartnerAgentsHelper.TypeText("SearchAgent", "Partner Agent Tester");
                agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                var loc = "//table[@id='list1']/tbody/tr[2]/td[@title='Partner Agent Tester']";
                agents_PartnerAgentsHelper.WaitForElementPresent(loc, 10);
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Partner Agent");
                    agents_PartnerAgentsHelper.ClickElement("ClikOnPartnerAgent");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Processor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Verify message");
                    agents_PartnerAgentsHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
                else
                {

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("Create");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstName", "Partner Agent");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "12/16/1991");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.con");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select DBAName");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter eAddressType");
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter SelectPhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Postal Code");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Checkbox");
                    agents_PartnerAgentsHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter UserName");
                    agents_PartnerAgentsHelper.TypeText("UserName", name);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Avatar");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("SaveAgent");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create adjustment button");
                    agents_PartnerAgentsHelper.scrollToElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Processor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "0");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "200");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Select AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Save Btn");
                    agents_PartnerAgentsHelper.ClickElement("SaveAdjustment");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Verify message");
                    agents_PartnerAgentsHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("ViewRevenueAdjustmentPartnerAgentAddFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ViewRevenueAdjustmentPartnerAgentAddFlatAmount");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("View Revenue Adjustment Partner Agent Add Flat Amount");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("View Revenue Adjustment Partner Agent Add Flat Amount", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("View Revenue Adjustment Partner Agent Add Flat Amount");
                        TakeScreenshot("ViewRevenueAdjustmentPartnerAgentAddFlatAmount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewRevenueAdjustmentPartnerAgentAddFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ViewRevenueAdjustmentPartnerAgentAddFlatAmount");
                        string id = loginHelper.getIssueID("View Revenue Adjustment Partner Agent Add Flat Amount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewRevenueAdjustmentPartnerAgentAddFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("View Revenue Adjustment Partner Agent Add Flat Amount"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("View Revenue Adjustment Partner Agent Add Flat Amount");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ViewRevenueAdjustmentPartnerAgentAddFlatAmount");
                executionLog.WriteInExcel("View Revenue Adjustment Partner Agent Add Flat Amount", Status, JIRA, "Agent Portal");
            }
        }
    }
}
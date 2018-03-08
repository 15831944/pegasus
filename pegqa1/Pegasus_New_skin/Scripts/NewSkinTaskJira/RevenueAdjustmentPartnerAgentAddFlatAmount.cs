using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentPartnerAgentAddFlatAmount : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueAdjustmentPartnerAgentAddFlatAmount()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_PartnerAgentsHelper = new Agents_PartnerAgentsHelper(GetWebDriver());
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            var Adjustment = "Adjustment" + GetRandomNumber();

            try
            {
                executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at dashboard page.");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Navigate to partner agent page.");
                VisitOffice("partners/agents");

                var loc = "//table[@id='list1']/tbody/tr[2]";
                agents_PartnerAgentsHelper.WaitForElementPresent(loc, 20);
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))

                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Sale Agent");
                    agents_PartnerAgentsHelper.ClickElement("ClikOnPartnerAgent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "scroll to element");
                    agents_PartnerAgentsHelper.scrollToElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Peiod");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "SelectProcessor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Save Btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Verify Message");
                    agents_PartnerAgentsHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
                else
                {
                    agents_PartnerAgentsHelper.WaitForElementPresent("ClickOnCreateBtn", 10);
                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreateBtn");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstNAME", "Partner Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "1991-03-02");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click on Middle name");
                    agents_PartnerAgentsHelper.ClickElement("ClickMiddleName");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select DBAName");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter eAddressType");
                    var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select PhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter City");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Checkbox");
                    agents_PartnerAgentsHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter UserName");
                    agents_PartnerAgentsHelper.TypeText("UserName", name);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Avatar");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveNskin");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Verify Text");
                    agents_PartnerAgentsHelper.WaitForText("The user is successfully added", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select Processor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Remove");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click On Save Btn Adjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Verify Message");
                    agents_PartnerAgentsHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentPartnerAgentAddFlatAmount");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Partner Agent Add Flat Amount");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Partner Agent Add Flat Amount", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Flat Amount");
                        TakeScreenshot("RevenueAdjustmentPartnerAgentAddFlatAmount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentAddFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentPartnerAgentAddFlatAmount");
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Flat Amount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentAddFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Flat Amount"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Flat Amount");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentPartnerAgentAddFlatAmount");
                executionLog.WriteInExcel("Revenue Adjustment Partner Agent Add Flat Amount", Status, JIRA, "Agent Portal");
            }
        }
    }
}
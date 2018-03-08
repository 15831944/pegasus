using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentPartnerAgentRemoveFlatAmount : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueAdjustmentPartnerAgentRemoveFlatAmount()
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
            var Adjustment = "Test" + GetRandomNumber();

            try
            {

                executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Redirect at dashboard page.");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Navigate to partner agent page.");
                VisitOffice("partners/agents");
               
                var loc = "//table[@id='list1']/tbody/tr[2]";
                agents_PartnerAgentsHelper.WaitForElementPresent(loc ,20);
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Sale Agent");
                    agents_PartnerAgentsHelper.ClickElement("ClikOnPartnerAgent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "scroll to element");
                    agents_PartnerAgentsHelper.scrollToElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName",Adjustment );

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Peiod");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Processor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Add Remove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Save Btn ");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Verify Message");
                    agents_PartnerAgentsHelper.WaitForText("Master Adjustment Rules Created Successfully." ,10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
                else
                {
                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreateBtn");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstNAME", "Partner Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "1991-03-02");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click on Middle name");
                    agents_PartnerAgentsHelper.ClickElement("ClickMiddleName");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Language");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter eAddressType");
                    var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select PhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter City");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Checkbox");
                    agents_PartnerAgentsHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter UserName");
                    agents_PartnerAgentsHelper.TypeText("UserName", name);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Avatar");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveNskin");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Verify Text");
                    agents_PartnerAgentsHelper.WaitForText("The user is successfully added", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "EnterAdjustmentName");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "SelectProcessor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Remove");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Verify Message");
                    agents_PartnerAgentsHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemoveFlatAmount", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentPartnerAgentRemoveFlatAmount");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Partner Agent Remove Flat Amount");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Partner Agent Remove Flat Amount", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Flat Amount");
                        TakeScreenshot("RevenueAdjustmentPartnerAgentRemoveFlatAmount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentRemoveFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentPartnerAgentRemoveFlatAmount");
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Flat Amount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentRemoveFlatAmount.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Flat Amount"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Flat Amount");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentPartnerAgentRemoveFlatAmount");
                executionLog.WriteInExcel("Revenue Adjustment Partner Agent Remove Flat Amount", Status, JIRA, "Agent Portal");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentPartnerAgentAddPercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueAdjustmentPartnerAgentAddPercentage()
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

                executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Redirect at dashboard page.");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Navigate to partner agent page.");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter the agent name.");
                agents_PartnerAgentsHelper.TypeText("SearchAgent", "Partner Agent Tester");
                agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                var loc = "//table[@id='list1']/tbody/tr[2]/td[@title='Partner Agent Tester']";
                agents_PartnerAgentsHelper.WaitForElementPresent(loc, 20);
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Partner Agent");
                    agents_PartnerAgentsHelper.ClickElement("ClikOnPartnerAgent");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "scroll to element");
                    agents_PartnerAgentsHelper.scrollToElement("ClickOnCreatebtnAdjmnt");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter Adjustment Name");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Adjustment For");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Peiod");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Processor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Save Btn Adjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Verify Message");
                    agents_PartnerAgentsHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");
                    agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);


                }
                else
                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreateBtn");
                    agents_PartnerAgentsHelper.WaitForWorkAround(5000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstNAME", "Partner Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "01/25/1990");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click on Middle name");
                    agents_PartnerAgentsHelper.ClickElement("ClickMiddleName");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Language");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter eAddressType");
                    var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select PhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter City");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Checkbox");
                    agents_PartnerAgentsHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter UserName");
                    agents_PartnerAgentsHelper.TypeText("UserName", name);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Avatar");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveNskin");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Verify Text");
                    agents_PartnerAgentsHelper.WaitForText("The user is successfully added", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");
                    agents_PartnerAgentsHelper.WaitForWorkAround(4000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "EnterAdjustmentName");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "SelectProcessor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Remove");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Verify Message");
                    agents_PartnerAgentsHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentAddPercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentPartnerAgentAddPercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Partner Agent Add Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Partner Agent Add Percentage", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Percentage");
                        TakeScreenshot("RevenueAdjustmentPartnerAgentAddPercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentAddPercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentPartnerAgentAddPercentage");
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentAddPercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Partner Agent Add Percentage");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentPartnerAgentAddPercentage");
                executionLog.WriteInExcel("Revenue Adjustment Partner Agent Add Percentage", Status, JIRA, "Agent Portal");
            }
        }
    }
}
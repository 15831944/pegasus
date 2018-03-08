using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentPartnerAgentRemovePercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void revenueAdjustmentPartnerAgentRemovePercentage()
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

                executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click on Click On Partner Agent");
                VisitOffice("partners/agents");
                agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                var loc = "//table[@id='list1']/tbody/tr[2]";
                agents_PartnerAgentsHelper.WaitForElementPresent(loc, 20);
                if (agents_PartnerAgentsHelper.IsElementPresent(loc))
                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Sale Agent");
                    agents_PartnerAgentsHelper.ClickElement("ClikOnPartnerAgent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "scroll to element");
                    agents_PartnerAgentsHelper.scrollToElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "EnterAdjustmentName");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select Type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select Peiod");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "SelectProcessor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify Message");
                    agents_PartnerAgentsHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);


                }
                else
                {

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Create");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreateBtn");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select Salutation");
                    agents_PartnerAgentsHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter FirstNAME");
                    agents_PartnerAgentsHelper.TypeText("FirstNAME", "Partner Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter LastName");
                    agents_PartnerAgentsHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter Date Of Birth");
                    agents_PartnerAgentsHelper.TypeText("BirthDay", "1991-03-02");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click on Middle name");
                    agents_PartnerAgentsHelper.ClickElement("ClickMiddleName");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter DBAName");
                    agents_PartnerAgentsHelper.TypeText("DBAName", "Test DBA");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter LinkedInUrl");
                    agents_PartnerAgentsHelper.TypeText("LinkedInUrl", "LinkedIn.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter FaceBook Url");
                    agents_PartnerAgentsHelper.TypeText("FaceBookUrl", "Facebook.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter TwitterURL");
                    agents_PartnerAgentsHelper.TypeText("TwitterURL", "Twitter.com");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select Language");
                    agents_PartnerAgentsHelper.Select("SelectLanguage", "English");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select eAddressType");
                    agents_PartnerAgentsHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select eAddressLebel");
                    agents_PartnerAgentsHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter eAddressType");
                    var Email = "P.Agent" + RandomNumber(1, 999) + "@yopmail.com";
                    agents_PartnerAgentsHelper.TypeText("eAddress", Email);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select PhoneType");
                    agents_PartnerAgentsHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter PhoneNumber");
                    agents_PartnerAgentsHelper.TypeText("PhoneNumber", "1212121212");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select Address Type    ");
                    agents_PartnerAgentsHelper.Select("AddressType", "Office");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter AddressLine1");
                    agents_PartnerAgentsHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter City");
                    agents_PartnerAgentsHelper.TypeText("PostalCode", "60601");
                    agents_PartnerAgentsHelper.WaitForWorkAround(2000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Checkbox");
                    agents_PartnerAgentsHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter UserName");
                    agents_PartnerAgentsHelper.TypeText("UserName", name);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Avatar");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnAvatar");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "CLICK Save AGENT btn");
                    agents_PartnerAgentsHelper.ClickElement("ClickSaveNskin");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify Text");
                    agents_PartnerAgentsHelper.WaitForText("The user is successfully added", 10);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click On Create btn Adjmnt");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "EnterAdjustmentName");
                    agents_PartnerAgentsHelper.TypeText("EnterAdjustmentName", Adjustment);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "SelectAdjustmentFor");
                    agents_PartnerAgentsHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select type");
                    agents_PartnerAgentsHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select period");
                    agents_PartnerAgentsHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "SelectProcessor");
                    agents_PartnerAgentsHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Select rule");
                    agents_PartnerAgentsHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter Amount");
                    agents_PartnerAgentsHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "AddRemove");
                    agents_PartnerAgentsHelper.Select("AddRemove", "Remove");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agents_PartnerAgentsHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify Message");
                    agents_PartnerAgentsHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Redirect at adjustment tools page.");
                    VisitOffice("rir/adjustments_tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Verify page title.");
                    VerifyTitle("Adjustments Tool");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Enter adjustment to be deleted.");
                    residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
                    residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Click on delete icon.");
                    residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Accept alert message.");
                    residualIncome_MasterDataHelper.AcceptAlert();

                    executionLog.Log("RevenueAdjustmentPartnerAgentRemovePercentage", "Wait for delete success.");
                    residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentPartnerAgentRemovePercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Partner Agent Remove Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Partner Agent Remove Percentage", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Percentage");
                        TakeScreenshot("RevenueAdjustmentPartnerAgentRemovePercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentRemovePercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentPartnerAgentRemovePercentage");
                        string id = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentPartnerAgentRemovePercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Partner Agent Remove Percentage");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentPartnerAgentRemovePercentage");
                executionLog.WriteInExcel("Revenue Adjustment Partner Agent Remove Percentage", Status, JIRA, "Agent Portal");
            }
        }
    }
}
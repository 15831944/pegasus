using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ViewRevenueAdjustmentSaleAgentPercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void viewRevenueAdjustmentSaleAgentPercentage()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());


            // Variable random
            String JIRA = "";
            String Status = "Pass";
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            var Email = "Sale" + RandomNumber(1, 999) + "@yopmail.com";
            var Adjustment = "Test" + GetRandomNumber();


            try
            {
            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Redirect To URL");
            VisitOffice("sales_agents");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click On Create Btn");
            agent_1099SalesAgentHelper.ClickElement("ClickOnCreateBtn");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Salutation");
            agent_1099SalesAgentHelper.Select("SelectSalutation", "Mr");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter FirstNAME");
            agent_1099SalesAgentHelper.TypeText("FirstNAME", name);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter LastName");
            agent_1099SalesAgentHelper.TypeText("LastName", "Tester");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Date Of Birth");
            agent_1099SalesAgentHelper.TypeText("BirthDay", "1991-03-02");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter eAddressType");
            agent_1099SalesAgentHelper.Select("eAddressType", "E-Mail");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter eAddressLebel");
            agent_1099SalesAgentHelper.Select("eAddressLebel", "Work");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter eAddressType");
            agent_1099SalesAgentHelper.TypeText("eAddress", Email);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select SelectPhoneType");
            agent_1099SalesAgentHelper.Select("SelectPhoneType", "Work");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter PhoneNumber");
            agent_1099SalesAgentHelper.TypeText("PhoneNumber", "9828928943");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Address Type    ");
            agent_1099SalesAgentHelper.Select("AddressType", "Office");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter AddressLine1");
            agent_1099SalesAgentHelper.TypeText("AddressLine1", "FC 89");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter City");
            agent_1099SalesAgentHelper.TypeText("City", "Test City");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Postal code.");
            agent_1099SalesAgentHelper.TypeText("PostalCode", "60601");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter agent name to search");
            agent_1099SalesAgentHelper.TypeText("UserName", name);
            
            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select the avatar");
            agent_1099SalesAgentHelper.ClickElement("ClickOnSAvatarBtn");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click on save button.");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveNskin");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);
            
            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click On Sale Agent");
            agent_1099SalesAgentHelper.ClickElement("ClickOnAgent1099");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click On Create btn Adjmnt");
            agent_1099SalesAgentHelper.ClickElement("ClickOnCreatebtnAdjmnt");
            agent_1099SalesAgentHelper.WaitForWorkAround(1000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click on SaleAgent");
            agent_1099SalesAgentHelper.ClickElement("ClickSaleManager");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Adjustment Name");
            agent_1099SalesAgentHelper.TypeText("EnterAdjustmentName", Adjustment);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Adjustment For");
            agent_1099SalesAgentHelper.Select("SelectAdjustmentFor", "Agent");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "select type");
            agent_1099SalesAgentHelper.Select("AdjustmentType", "Transaction");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "select period");
            agent_1099SalesAgentHelper.Select("SelectReportingPeriod", "00");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Processor");
            agent_1099SalesAgentHelper.Select("SelectProcessor", "Any");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Adjustment For");
            agent_1099SalesAgentHelper.Select("SelectRuleType", "1");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Amount");
            agent_1099SalesAgentHelper.TypeText("EnterAmount", "20");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Select Add");
            agent_1099SalesAgentHelper.Select("AddRemove", "Add");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click On Save Btn");
            agent_1099SalesAgentHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
            agent_1099SalesAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify message");
            agent_1099SalesAgentHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Redirect To adjustment rule");
            VisitOffice("rir/adjustments_tool");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Adjustment Name");
            residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
            residualIncome_MasterDataHelper.WaitForWorkAround(2000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter Srch Adjustment For");
            residualIncome_MasterDataHelper.SelectByText("EnterScrhAdjustmentFor", "Agent");
            agent_1099SalesAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click on Adjustment ");
            residualIncome_MasterDataHelper.ClickElement("ClickAdjustmentName");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify Adjustment Name");
            residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", Adjustment);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify Select Rule Type");
            residualIncome_MasterDataHelper.Select("SelectRuleType", "1");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify Enter Amount");
            residualIncome_MasterDataHelper.TypeText("EnterAmount", "20");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Redirect at adjustment tools page.");
            VisitOffice("rir/adjustments_tool");
            residualIncome_MasterDataHelper.WaitForWorkAround(3000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Verify page title.");
            VerifyTitle("Adjustments Tool");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Enter adjustment to be deleted.");
            residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", Adjustment);
            residualIncome_MasterDataHelper.WaitForWorkAround(2000);

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Click on delete icon.");
            residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Accept alert message.");
            residualIncome_MasterDataHelper.AcceptAlert();

            executionLog.Log("ViewRevenueAdjustmentSaleAgentPercentage", "Wait for delete success.");
            residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ViewRevenueAdjustmentSaleAgentPercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("View Revenue Adjustment SaleAgent Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("View Revenue Adjustment SaleAgent Percentage", "Bug", "Medium", "Sale Agent page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("View Revenue Adjustment SaleAgent Percentage");
                        TakeScreenshot("ViewRevenueAdjustmentSaleAgentPercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewRevenueAdjustmentSaleAgentPercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ViewRevenueAdjustmentSaleAgentPercentage");
                        string id = loginHelper.getIssueID("View Revenue Adjustment SaleAgent Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ViewRevenueAdjustmentSaleAgentPercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("View Revenue Adjustment SaleAgent Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("View Revenue Adjustment SaleAgent Percentage");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ViewRevenueAdjustmentSaleAgentPercentage");
                executionLog.WriteInExcel("View Revenue Adjustment SaleAgent Percentage", Status, JIRA, "Agent Portal");
            }
        }
    }
} 
 
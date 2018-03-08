using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RevenueAdjustmentSaleAgentAddAmount : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        public void revenueAdjustmentSaleAgentAddAmount()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);


            try
            {

                executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click on Click On Partner Agent");
                VisitOffice("partners/agents");
               
                var loc = "//table[@id='list1']/tbody/tr[2]";
                if (agent_1099SalesAgentHelper.IsElementPresent(loc))
                {

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Sale Agent");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnAgent1099");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Create btn Adjmnt");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Adjustment Name");
                    agent_1099SalesAgentHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Adjustment For");
                    agent_1099SalesAgentHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select type");
                    agent_1099SalesAgentHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select period");
                    agent_1099SalesAgentHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Processor");
                    agent_1099SalesAgentHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select rule");
                    agent_1099SalesAgentHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Amount");
                    agent_1099SalesAgentHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "AddRemove");
                    agent_1099SalesAgentHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Save Btn");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                    agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Verify Messsage");
                    agent_1099SalesAgentHelper.WaitForText("Master Adjustment Rules Created Successfully." ,10);
                    
                }
                else
                {

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", " Click On Create button");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnCreateBtn");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Salutation");
                    agent_1099SalesAgentHelper.Select("SelectSalutation", "Mr");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter FirstNAME");
                    agent_1099SalesAgentHelper.TypeText("FirstNAME", "Sale Agent Revenue Adjustment");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter LastName");
                    agent_1099SalesAgentHelper.TypeText("LastName", "Tester");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Date Of Birth");
                    agent_1099SalesAgentHelper.TypeText("BirthDay", "1991-03-02");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select eAddressType");
                    agent_1099SalesAgentHelper.Select("eAddressType", "E-Mail");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select eAddressLebel");
                    agent_1099SalesAgentHelper.Select("eAddressLebel", "Work");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter eAddressType");
                    var Email = "Sale" + RandomNumber(1, 999) + "@yopmail.com";
                    agent_1099SalesAgentHelper.TypeText("eAddress", Email);

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select SelectPhoneType");
                    agent_1099SalesAgentHelper.Select("SelectPhoneType", "Work");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter PhoneNumber");
                    agent_1099SalesAgentHelper.TypeText("PhoneNumber", "9828928943");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Address Type    ");
                    agent_1099SalesAgentHelper.Select("AddressType", "Office");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter AddressLine1");
                    agent_1099SalesAgentHelper.TypeText("AddressLine1", "FC 89");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter City");
                    agent_1099SalesAgentHelper.TypeText("City", "Test City");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Postal code");
                    agent_1099SalesAgentHelper.TypeText("PostalCode", "60601");
                    agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Checkbox");
                    agent_1099SalesAgentHelper.ClickElement("ClickONcheckBox");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter UserName");
                    agent_1099SalesAgentHelper.TypeText("UserName", name);

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Avatar");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnSAvatarBtn");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click on save button");
                    agent_1099SalesAgentHelper.ClickElement("ClickSaveNskin");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter agent name to search");
                    agent_1099SalesAgentHelper.TypeText("EnterAgentName", "Sale Agent Revenue Adjustment Tester");

                    agent_1099SalesAgentHelper.Select("SelectStatusAdjtmnt", "");
                    agent_1099SalesAgentHelper.WaitForWorkAround(4000);

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Sale Agent");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnAgent1099");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Create btn Adjmnt");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnCreatebtnAdjmnt");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click on SaleAgent");
                    agent_1099SalesAgentHelper.ClickElement("ClickSaleManager");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Adjustment Name");
                    agent_1099SalesAgentHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Adjustment For");
                    agent_1099SalesAgentHelper.Select("SelectAdjustmentFor", "Agent");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select type");
                    agent_1099SalesAgentHelper.Select("AdjustmentType", "Transaction");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select period");
                    agent_1099SalesAgentHelper.Select("SelectReportingPeriod", "00");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Select Processor");
                    agent_1099SalesAgentHelper.Select("SelectProcessor", "Any");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "select rule");
                    agent_1099SalesAgentHelper.Select("SelectRuleType", "1");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Enter Amount");
                    agent_1099SalesAgentHelper.TypeText("EnterAmount", "20");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "AddRemove");
                    agent_1099SalesAgentHelper.Select("AddRemove", "Add");

                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Click On Save Btn");
                    agent_1099SalesAgentHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                   
                    executionLog.Log("RevenueAdjustmentSaleAgentAddAmount", "Verify Message");
                    agent_1099SalesAgentHelper.WaitForText("Master Adjustment Rules Created Successfully." ,10);
                    
                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RevenueAdjustmentSaleAgentAddAmount");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Revenue Adjustment Sale Agent Add Amount");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Revenue Adjustment Sale Agent Add Amount", "Bug", "Medium", "Agent sale page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Revenue Adjustment Sale Agent Add Amount");
                        TakeScreenshot("RevenueAdjustmentSaleAgentAddAmount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentSaleAgentAddAmount.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RevenueAdjustmentSaleAgentAddAmount");
                        string id = loginHelper.getIssueID("Revenue Adjustment Sale Agent Add Amount");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RevenueAdjustmentSaleAgentAddAmount.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Revenue Adjustment Sale Agent Add Amount"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Revenue Adjustment Sale Agent Add Amount");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RevenueAdjustmentSaleAgentAddAmount");
                executionLog.WriteInExcel("Revenue Adjustment Sale Agent Add Amount", Status, JIRA, "Agent Portal");
            }
        }
    }
}
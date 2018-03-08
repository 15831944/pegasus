using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIncomeOfficeAdjustmentToolAgentsPercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualIncomeOfficeAdjustmentToolAgentsPercentage()
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
            var residualIncome_MasterDataHelper = new ResidualIncome_MasterDataHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();


            try
            {
                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Agent");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "SelectProcessor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "rule");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "1");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "20");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Remove");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Click On Save Button");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Verify message");
                residualIncome_MasterDataHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Verify title");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", name);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjustment");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsPercentage", "Wait for text.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIncomeOfficeAdjustmentToolAgentsPercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Income Office Adjustment Tool Agents Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Income Office Adjustment Tool Agents Percentage", "Bug", "Medium", "Partner page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Percentage");
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolAgentsPercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolAgentsPercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolAgentsPercentage");
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolAgentsPercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Percentage");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIncomeOfficeAdjustmentToolAgentsPercentage");
                executionLog.WriteInExcel("Residual Income Office Adjustment Tool Agents Percentage", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}
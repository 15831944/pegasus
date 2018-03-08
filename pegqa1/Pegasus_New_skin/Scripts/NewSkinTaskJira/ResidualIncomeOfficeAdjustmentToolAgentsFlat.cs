using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIncomeOfficeAdjustmentToolAgentsFlat : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualIncomeOfficeAdjustmentToolAgentsFlat()
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
                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Agent");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "select type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "SelectProcessor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "select type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "0");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "200");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Select Add");
                residualIncome_MasterDataHelper.Select("AddRemove", "Add");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Click on save button");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Verify mesage");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Verify title");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", name);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjustment");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolAgentsFlat", "Wait for text.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIncomeOfficeAdjustmentToolAgentsFlat");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Income Office Adjustment Tool Agents Flat");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Income Office Adjustment Tool Agents Flat", "Bug", "Medium", "Residual adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Flat");
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolAgentsFlat");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolAgentsFlat.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolAgentsFlat");
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Flat");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolAgentsFlat.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Flat"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Income Office Adjustment Tool Agents Flat");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIncomeOfficeAdjustmentToolAgentsFlat");
                executionLog.WriteInExcel("Residual Income Office Adjustment Tool Agents Flat", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}
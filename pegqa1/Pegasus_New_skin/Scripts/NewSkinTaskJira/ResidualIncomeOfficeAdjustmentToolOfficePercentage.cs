using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIncomeOfficeAdjustmentToolOfficePercentage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualIncomeOfficeAdjustmentToolOfficePercentage()
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
                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Select adjustment for");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Select Processor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "select rule type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "1");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "20");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Select AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Remove");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Click On Save Btn");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Verify message");
                residualIncome_MasterDataHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Enter Adjustment name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", name);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjustment");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();
                residualIncome_MasterDataHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficePercentage", "Wait for text.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIncomeOfficeAdjustmentToolOfficePercentage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Income Office Adjustment Tool Office Percentage");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Income Office Adjustment Tool Office Percentage", "Bug", "Medium", "Residual Adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Percentage");
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolOfficePercentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolOfficePercentage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolOfficePercentage");
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Percentage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolOfficePercentage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Percentage"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Percentage");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIncomeOfficeAdjustmentToolOfficePercentage");
                executionLog.WriteInExcel("Residual Income Office Adjustment Tool Office Percentage", Status, JIRA, "Residual Adjustment tool");
            }
        }
    }
}
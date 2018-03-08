using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIncomeOfficeAdjustmentToolOfficeFlat : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void residualIncomeOfficeAdjustmentToolOfficeFlat()
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
                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "SelectProcessor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "rule type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "0");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "200");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Add");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Click On Save Button");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Verify message");
                residualIncome_MasterDataHelper.VerifyPageText("Master Adjustment Rules Created Successfully.");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Verify title");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", name);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjustment");

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeOfficeAdjustmentToolOfficeFlat", "Wait for text.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIncomeOfficeAdjustmentToolOfficeFlat");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Income Office Adjustment Tool Office Flat");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Income Office Adjustment Tool Office Flat", "Bug", "Medium", "Residual Adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Flat");
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolOfficeFlat");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolOfficeFlat.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIncomeOfficeAdjustmentToolOfficeFlat");
                        string id = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Flat");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeOfficeAdjustmentToolOfficeFlat.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Flat"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Income Office Adjustment Tool Office Flat");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIncomeOfficeAdjustmentToolOfficeFlat");
                executionLog.WriteInExcel("Residual Income Office Adjustment Tool Office Flat", Status, JIRA, "Residual Adjustment tool");
            }
        }
    }
}
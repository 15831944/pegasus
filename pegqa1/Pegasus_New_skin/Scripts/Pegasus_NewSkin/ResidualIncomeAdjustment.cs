using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIncomeAdjustment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void residualIncomeAdjustment()
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
                executionLog.Log("ResidualIncomeAdjustment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIncomeAdjustment", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIncomeAdjustment", "Redirect at Adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeAdjustment", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                executionLog.Log("ResidualIncomeAdjustment", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Agent");

                executionLog.Log("ResidualIncomeAdjustment", "select type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeAdjustment", "select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeAdjustment", "select Rule type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "0");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "200");

                executionLog.Log("ResidualIncomeAdjustment", " Select AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Add");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Save Btn Adjustmnet");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeAdjustment", "Verify mesage");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                executionLog.Log("ResidualIncomeAdjustment", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeAdjustment", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeAdjustment", "Click on Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                executionLog.Log("ResidualIncomeAdjustment", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Agent");

                executionLog.Log("ResidualIncomeAdjustment", "Select adjustment type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeAdjustment", "Select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeAdjustment", "Select rule");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "1");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "20");

                executionLog.Log("ResidualIncomeAdjustment", "Select AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Remove");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Save Btn ");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualIncomeAdjustment", "Wait for success message");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                executionLog.Log("ResidualIncomeAdjustment", "Redirect at adjustment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeAdjustment", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Create btn Adjmnt");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                executionLog.Log("ResidualIncomeAdjustment", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualIncomeAdjustment", "Select type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeAdjustment", "Select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeAdjustment", "Select rule type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "0");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "200");

                executionLog.Log("ResidualIncomeAdjustment", "Select AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Add");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Save Btn");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIncomeAdjustment", "Wait for message");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                executionLog.Log("ResidualIncomeAdjustment", "Redirect at Adjsutment tool page.");
                VisitOffice("rir/adjustments_tool");

                executionLog.Log("ResidualIncomeAdjustment", "Verify title");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Create btn");
                residualIncome_MasterDataHelper.ClickElement("ClickOnCreateAdjust");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Adjustment Name");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", "SaleAdjustment");

                executionLog.Log("ResidualIncomeAdjustment", "Select Adjustment For");
                residualIncome_MasterDataHelper.Select("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualIncomeAdjustment", "Select type");
                residualIncome_MasterDataHelper.Select("AdjustmentType", "Transaction");

                executionLog.Log("ResidualIncomeAdjustment", "select period");
                residualIncome_MasterDataHelper.Select("SelectReportingPeriod", "00");

                executionLog.Log("ResidualIncomeAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.Select("SelectProcessor", "Any");

                executionLog.Log("ResidualIncomeAdjustment", "select rule type");
                residualIncome_MasterDataHelper.Select("SelectRuleType", "1");

                executionLog.Log("ResidualIncomeAdjustment", "Enter Amount");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "20");

                executionLog.Log("ResidualIncomeAdjustment", "Select AddRemove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Remove");

                executionLog.Log("ResidualIncomeAdjustment", "Click On Save Btn");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualIncomeAdjustment", "Verify message");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIncomeAdjustment");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Income Adjustment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Income Adjustment", "Bug", "Medium", "Residual Adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Income Adjustment");
                        TakeScreenshot("ResidualIncomeAdjustment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeAdjustment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIncomeAdjustment");
                        string id = loginHelper.getIssueID("Residual Income Adjustment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIncomeAdjustment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Income Adjustment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Income Adjustment");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIncomeAdjustment");
                executionLog.WriteInExcel("Residual Income Adjustment", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}
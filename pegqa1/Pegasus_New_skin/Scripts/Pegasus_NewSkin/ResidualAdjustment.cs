using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ResidualAdjustment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void residualAdjustment()
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

            // Variable random
            var usernme = "Sysprins" + RandomNumber(44, 799999977);
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ResidualAdjustment", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ResidualAdjustment", "Verify Page title");
                VerifyTitle("Dashboard");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Goto Residual Incomwe Adjustment tool create");
                VisitOffice("rir/adjustments_tool/create");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Enter Adjustment NAME");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualAdjustment", "Select Adjustment for");
                residualIncome_MasterDataHelper.SelectByText("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualAdjustment", "Adjustment Type");
                residualIncome_MasterDataHelper.SelectByText("AdjustmentType", "Transaction");

                executionLog.Log("ResidualAdjustment", "Reporting Period");
                residualIncome_MasterDataHelper.SelectByText("SelectReportingPeriod", "Any");

                executionLog.Log("ResidualAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.SelectByText("SelectProcessor", "First Data Omaha");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Click on save button.");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualAdjustment", "Wait for confirmation.");
                residualIncome_MasterDataHelper.WaitForText("Master Adjustment Rules Created Successfully.", 10);

                executionLog.Log("ResidualAdjustment", "Goto Residual Income Adjustment tool create");
                VisitOffice("rir/adjustments_tool/create");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Enter Adjustment NAME");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualAdjustment", "Select Adjustment for");
                residualIncome_MasterDataHelper.SelectByText("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualAdjustment", "Adjustment Type");
                residualIncome_MasterDataHelper.SelectByText("AdjustmentType", "Transaction");

                executionLog.Log("ResidualAdjustment", "Reporting Period");
                residualIncome_MasterDataHelper.SelectByText("SelectReportingPeriod", "Any");

                executionLog.Log("ResidualAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.SelectByText("SelectProcessor", "First Data Omaha");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Click on cancel button.");
                residualIncome_MasterDataHelper.ClickElement("CancelResidualAdjustment");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Verify page text.");
                residualIncome_MasterDataHelper.VerifyText("ClickOnAdjustmentToolRI", "Adjustments Tool");

                executionLog.Log("ResidualAdjustment", "Goto Residual Income Adjustment tool create");
                VisitOffice("rir/adjustments_tool/create");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Enter Adjustment NAME");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentName", name);

                executionLog.Log("ResidualAdjustment", "Select Adjustment for");
                residualIncome_MasterDataHelper.SelectByText("SelectAdjustmentFor", "Office");

                executionLog.Log("ResidualAdjustment", "Adjustment Type");
                residualIncome_MasterDataHelper.SelectByText("AdjustmentType", "Transaction");

                executionLog.Log("ResidualAdjustment", "Reporting Period");
                residualIncome_MasterDataHelper.SelectByText("SelectReportingPeriod", "Any");

                executionLog.Log("ResidualAdjustment", "Select Processor");
                residualIncome_MasterDataHelper.SelectByText("SelectProcessor", "First Data Omaha");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Select RuleType");
                residualIncome_MasterDataHelper.SelectByText("SelectRuleType", "Percentage");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "EnterAmountRA");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "TEST AMOUNT");

                executionLog.Log("ResidualAdjustment", " Select Add / Remove");
                residualIncome_MasterDataHelper.Select("AddRemove", "Add");

                executionLog.Log("ResidualAdjustment", "Click on Save");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualAdjustment", "Wait for confirmation.");
                residualIncome_MasterDataHelper.WaitForText("Please enter a valid number.", 10);

                executionLog.Log("ResidualAdjustment", "EnterAmountRA");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "-22");

                executionLog.Log("ResidualAdjustment", "Click on Save");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualAdjustment", "Wait for validation message.");
                residualIncome_MasterDataHelper.WaitForText("Please enter a value greater than or equal to 0.", 10);

                executionLog.Log("ResidualAdjustment", "Enter Amount RA");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "500");

                executionLog.Log("ResidualAdjustment", "Click on Save");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");

                executionLog.Log("ResidualAdjustment", "Wait for validation.");
                residualIncome_MasterDataHelper.WaitForText("Please enter a value less than or equal to 100.", 10);

                executionLog.Log("ResidualAdjustment", "Select Rule Type");
                residualIncome_MasterDataHelper.SelectByText("SelectRuleType", "Flat Amount");
                residualIncome_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("ResidualAdjustment", "EnterAmountRA");
                residualIncome_MasterDataHelper.TypeText("EnterAmount", "44.2343");

                executionLog.Log("ResidualAdjustment", "Click on Save");
                residualIncome_MasterDataHelper.ClickElement("ClickOnSaveBtnAdjustmnet");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Redirect at adjustment tools page.");
                VisitOffice("rir/adjustments_tool");
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Verify page title.");
                VerifyTitle("Adjustments Tool");

                executionLog.Log("ResidualAdjustment", "Enter adjustment to be deleted.");
                residualIncome_MasterDataHelper.TypeText("EnterAdjustmentNameSrch", name);
                residualIncome_MasterDataHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualAdjustment", "Click on delete icon.");
                residualIncome_MasterDataHelper.ClickElement("DeleteAdjtmnt");

                executionLog.Log("ResidualAdjustment", "Accept alert message.");
                residualIncome_MasterDataHelper.AcceptAlert();

                executionLog.Log("ResidualAdjustment", "Wait for delete success.");
                residualIncome_MasterDataHelper.WaitForText("Ruleset deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualAdjustment");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Adjustment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Adjustment", "Bug", "Medium", "Residul Adjustment page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Adjustment");
                        TakeScreenshot("ResidualAdjustment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualAdjustment");
                        string id = loginHelper.getIssueID("Residual Adjustment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualAdjustment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Adjustment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Adjustment");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualAdjustment");
                executionLog.WriteInExcel("Residual Adjustment", Status, JIRA, "Residual Income");
            }
        }
    }
}
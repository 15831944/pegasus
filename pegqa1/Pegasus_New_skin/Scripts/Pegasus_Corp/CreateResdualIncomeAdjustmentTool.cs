using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateResdualIncomeAdjustmentTool : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createResdualIncomeAdjustmentTool()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpResidualIncome_Masterdata_AdjustmentToolHelper = new CorpResidualIncome_Masterdata_AdjustmentToolHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            var RuleSet = "RuleSet" + RandomNumber(9, 999);

            try
            {

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Click to Import");
            VisitCorp("rir/masterrules");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Verify Page title");
            VerifyTitle("Residual Master Rules - Adjustments");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Click On Create Btn");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("Create");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Verify Page title");
            VerifyTitle("Residual Master Rules - Create Adjustments");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Select Processor Type");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("ProcessorType", "First Data Omaha");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Enter Rule Set Name");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("RuleSetName", RuleSet);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Rule Type");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.SelectByText("RuleType", "Amount");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Enter Amount ");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("Amount", "200");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Select AddRemove");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.Select("AddRemove", "Add");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Click On SaveBtn");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("Save");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Wait for success message");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForText("Residual Master Rule Created.", 10);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Redirect to master rules page.");
            VisitCorp("rir/masterrules");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Verify Page title");
            VerifyTitle("Residual Master Rules - Adjustments");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Enter Name to search");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.TypeText("SearchRuleSetNAme", RuleSet);
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(2000);

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Click Delete btn  ");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("DeleteIcon");

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Accept alert message. ");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.AcceptAlert();

            executionLog.Log("CreateResdualIncomeAdjustmentTool", "Wait for delete message. ");
            corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForText("Master rule deleted successfully.", 10);

        }
    

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateResdualIncomeAdjustmentTool");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Resdual Income Adjustment Tool");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Resdual Income Adjustment Tool", "Bug", "Medium", "Residual Income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Resdual Income Adjustment Tool");
                        TakeScreenshot("CreateResdualIncomeAdjustmentTool");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateResdualIncomeAdjustmentTool.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateResdualIncomeAdjustmentTool");
                        string id = loginHelper.getIssueID("Create Resdual Income Adjustment Tool");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateResdualIncomeAdjustmentTool.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Resdual Income Adjustment Tool"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Resdual Income Adjustment Tool");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateResdualIncomeAdjustmentTool");
                executionLog.WriteInExcel("Create Resdual Income Adjustment Tool", Status, JIRA, "Corp Residual Adjustment");
            }
        }
    }
}  
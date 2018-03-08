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
    public class VerifyBlankMasterRuleError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        public void verifyBlankMasterRuleError()
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

                executionLog.Log("VerifyBlankMasterRuleError", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyBlankMasterRuleError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyBlankMasterRuleError", "Click to Import");
                VisitCorp("rir/masterrules");

                executionLog.Log("VerifyBlankMasterRuleError", "Verify Page title");
                VerifyTitle("Residual Master Rules - Adjustments");

                executionLog.Log("VerifyBlankMasterRuleError", "Click on edit icon.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("EditAdjustment");

                executionLog.Log("VerifyBlankMasterRuleError", "Click on add another rule.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("AddAnotherRule");

                executionLog.Log("VerifyBlankMasterRuleError", "Click on save butoon.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.ClickElement("Save");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBlankMasterRuleError", "Verify page text for required field.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.VerifyPageText("This field is required.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBlankMasterRuleError", "Verify updation text not present.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.VerifyTextNotPresent("Residual Master Rule Updated.");
                corpResidualIncome_Masterdata_AdjustmentToolHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyBlankMasterRuleError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Blank Master Rule Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Blank Master Rule Error", "Bug", "Medium", "Residual Income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Blank Master Rule Error");
                        TakeScreenshot("VerifyBlankMasterRuleError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBlankMasterRuleError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyBlankMasterRuleError");
                        string id = loginHelper.getIssueID("Verify Blank Master Rule Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBlankMasterRuleError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Blank Master Rule Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Blank Master Rule Error");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyBlankMasterRuleError");
                executionLog.WriteInExcel("Verify Blank Master Rule Error", Status, JIRA, "Corp Residual Adjustment");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpResidualIncomePayoutSummaryUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpResidualIncomePayoutSummaryUrlChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_ResidualIncome_Payout_PayoutSummaryHelper = new CorpResidualIncome_PayoutsHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Go To Resisual Income");
                VisitCorp("rir/office_payout_summary");
               
                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Click On Payout Summary");
                corp_ResidualIncome_Payout_PayoutSummaryHelper.ClickElement("ClickOnAnyPayoutSummary");
                corp_ResidualIncome_Payout_PayoutSummaryHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("rir/office_payouts/18747");
                
                executionLog.Log("CorpResidualIncomePayoutSummaryUrlChange", "Verify Validation");
                corp_ResidualIncome_Payout_PayoutSummaryHelper.WaitForText("oops something went wrong" ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpResidualIncomePayoutSummaryUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Corp Residual Income Payout Summary Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Residual Income Payout Summary Url Change", "Bug", "Medium", "Corp residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Residual Income Payout Summary Url Change");
                        TakeScreenshot("CorpResidualIncomePayoutSummaryUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomePayoutSummaryUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpResidualIncomePayoutSummaryUrlChange");
                        string id = loginHelper.getIssueID("Corp Residual Income Payout Summary Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomePayoutSummaryUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Residual Income Payout Summary Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Residual Income Payout Summary Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpResidualIncomePayoutSummaryUrlChange");
                executionLog.WriteInExcel("Corp Residual Income Payout Summary Url Change", Status, JIRA, "Corp Residual Income");
            }
        }
    }
}

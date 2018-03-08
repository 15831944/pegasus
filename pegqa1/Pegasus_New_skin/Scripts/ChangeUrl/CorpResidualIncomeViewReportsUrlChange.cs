using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpResidualIncomeViewReportsUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpResidualIncomeViewReportsUrlChange()
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
            var corp_ResidualIncome_Payout_RepostHelper = new CorpResidualIncome_PayoutsHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Go To Resisual Income");
                VisitCorp("rir/imports");

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Select status as published");
                corp_ResidualIncome_Payout_RepostHelper.SelectByText("ResidualStatus", "Published");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(1000);

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Click On View Reports");
                corp_ResidualIncome_Payout_RepostHelper.ClickElement("ResidualIncoemViewReports");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("rir/reports/file/2203");

                executionLog.Log("CorpResidualIncomeViewReportsUrlChange", "Verify Validation");
                corp_ResidualIncome_Payout_RepostHelper.WaitForText("No Payouts.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpResidualIncomeViewReportsUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Residual Income View Reports Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Residual Income View Reports Url Change", "Bug", "Medium", "Corp Residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Residual Income View Reports Url Change");
                        TakeScreenshot("CorpResidualIncomeViewReportsUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeViewReportsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpResidualIncomeViewReportsUrlChange");
                        string id = loginHelper.getIssueID("Corp Residual Income View Reports Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpResidualIncomeViewReportsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Residual Income View Reports Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Residual Income View Reports Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpResidualIncomeViewReportsUrlChange");
                executionLog.WriteInExcel("Corp Residual Income View Reports Url Change", Status, JIRA, "Corp Residual Income");
            }
        }
    }
}

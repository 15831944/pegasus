using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ResidualIcomeViewReports : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void residualIcomeViewReports()
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
                executionLog.Log("ResidualIcomeViewReports", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ResidualIcomeViewReports", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ResidualIcomeViewReports", "Go To Resisual Income");
                VisitCorp("rir/imports");

                executionLog.Log("ResidualIcomeViewReports", "Select status as published");
                corp_ResidualIncome_Payout_RepostHelper.SelectByText("ResidualStatus", "Published");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualIcomeViewReports", "Click On View Reports");
                corp_ResidualIncome_Payout_RepostHelper.ClickElement("ResidualIncoemViewReports");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(3000);

                executionLog.Log("ResidualIcomeViewReports", "Title of page is Residual Income - Reports");
                VerifyTitle("Residual Income - Reports");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(2000);

                executionLog.Log("ResidualIcomeViewReports", "Verify text on page is Residual Income / Reports");
                corp_ResidualIncome_Payout_RepostHelper.VerifyPageText("Residual Income / Reports");
                corp_ResidualIncome_Payout_RepostHelper.WaitForWorkAround(2000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ResidualIcomeViewReports");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Residual Icome View Reports");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Residual Icome View Reports", "Bug", "Medium", "Corp Residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Residual Icome View Reports");
                        TakeScreenshot("ResidualIcomeViewReports");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIcomeViewReports.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ResidualIcomeViewReports");
                        string id = loginHelper.getIssueID("Residual Icome View Reports");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ResidualIcomeViewReports.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Residual Icome View Reports"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Residual Icome View Reports");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ResidualIcomeViewReports");
                executionLog.WriteInExcel("Residual Icome View Reports", Status, JIRA, "Corp Residual Income");
            }
        }
    }
}

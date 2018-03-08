using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CheckFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void checkFilter()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var residualIncome_OfficePayout_DetailedPayoutHelper = new ResidualIncome_OfficePayout_DetailedPayoutHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CheckFilter","Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CheckFilter","Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CheckFilter","Go to Detailed payout page");
                VisitOffice("rir/detailed_payouts");

                executionLog.Log("CheckFilter","Verify title");
                VerifyTitle("Residual Income - Payouts");

                executionLog.Log("CheckFilter","Click on advanced filter");
                residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Advance");

                executionLog.Log("CheckFilter","Verify title");
                VerifyTitle("Residual Income - Payouts");

                executionLog.Log("CheckFilter","Go to the transaction page");
                VisitOffice("rir/payout_summary");

                executionLog.Log("CheckFilter","Verify title");
                VerifyTitle("Payouts Summary");

                executionLog.Log("CheckFilter","Logout from the application");
                VisitOffice("logout");
        }
            
        catch(Exception e)
        {
            executionLog.Log("Error", e.StackTrace);
            Status = "Fail";

            String counter = executionLog.readLastLine("counter");
            String Description = executionLog.GetAllTextFile("CheckFilter");
            String Error = executionLog.GetAllTextFile("Error");
            if (counter == "")
            {
                counter = "0";
            }
            bool result = loginHelper.CheckExstingIssue("Check Filter");
            if (!result)
            {
                if (Int16.Parse(counter) < 9)
                {
                    executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                    loginHelper.CreateIssue("Check Filter", "Bug", "Medium", "Payout summary page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description+"\n\n\nError Description:\n"+Error);
                    string id = loginHelper.getIssueID("Check Filter");
                    TakeScreenshot("CheckFilter");
                    string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                    var location = directoryName + "\\CheckFilter.png";
                    loginHelper.AddAttachment(location, id);
                }
            }
            else
            {
                if (Int16.Parse(counter) < 9)
                {
                    executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                    TakeScreenshot("CheckFilter");
                    string id = loginHelper.getIssueID("Check Filter");
                    string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                    var location = directoryName + "\\CheckFilter.png";
                    loginHelper.AddAttachment(location, id);
                    loginHelper.AddComment(loginHelper.getIssueID("Check Filter"), "This issue is still occurring");
                }
            }
            JIRA = loginHelper.getIssueID("Check Filter");
        //    executionLog.DeleteFile("Error");
            throw;

        }
            finally
            {
                executionLog.DeleteFile("CheckFilter");
                executionLog.WriteInExcel("Check Filter", Status, JIRA, "Office Residual Adjustment");
            }
        }
    }
}
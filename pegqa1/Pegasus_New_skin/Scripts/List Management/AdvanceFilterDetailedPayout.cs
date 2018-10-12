using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class AdvanceFilterDetailedPayout : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS9")]
        [TestCategory("ListManagement")]
        public void advanceFilterDetailedPayout()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var residualIncome_OfficePayout_DetailedPayoutHelper = new ResidualIncome_OfficePayout_DetailedPayoutHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("AdvanceFilterDetailedPayout", "Login with valid username and password");
            Login(username1[0], password1[0]);

            executionLog.Log("AdvanceFilterDetailedPayout", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("AdvanceFilterDetailedPayout", "Redirect To Detailed Payout page");
            GetWebDriver().Navigate().GoToUrl("https://www.pegasus-test.com/newthemecorp/pegasustestoffice/rir/detailed_payouts");
            residualIncome_OfficePayout_DetailedPayoutHelper.WaitForWorkAround(2000);

            executionLog.Log("AdvanceFilterDetailedPayout", "Click on advance filter");
            residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Advance");
            residualIncome_OfficePayout_DetailedPayoutHelper.WaitForWorkAround(1000);

            executionLog.Log("AdvanceFilterDetailedPayout", "Enter Filter Name");
            residualIncome_OfficePayout_DetailedPayoutHelper.TypeText("Filtername", "testerqa");

            executionLog.Log("AdvanceFilterDetailedPayout", "Click Update Button");
            residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Update");
            residualIncome_OfficePayout_DetailedPayoutHelper.WaitForWorkAround(3000);

            executionLog.Log("AdvanceFilterDetailedPayout", "Click on advance filter");
            residualIncome_OfficePayout_DetailedPayoutHelper.ClickElement("Advance");

            executionLog.Log("AdvanceFilterDetailedPayout", "Verify Filter Name in Saved Filter");
            residualIncome_OfficePayout_DetailedPayoutHelper.OptionNotPresentInDropdown("SavedFilter", "testerqa");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdvanceFilterDetailedPayout");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Advance Filter Detailed Payout");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Advance Filter Detailed Payout", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Advance Filter Detailed Payout");
                        TakeScreenshot("AdvanceFilterDetailedPayout");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdvanceFilterDetailedPayout.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdvanceFilterDetailedPayout");
                        string id = loginHelper.getIssueID("Advance Filter Detailed Payout");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdvanceFilterDetailedPayout.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Advance Filter Detailed Payout"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Advance Filter Detailed Payout");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdvanceFilterDetailedPayout");
                executionLog.WriteInExcel("Advance Filter Detailed Payout", Status, JIRA, "Corp Employee");
            }
            }
    }
}

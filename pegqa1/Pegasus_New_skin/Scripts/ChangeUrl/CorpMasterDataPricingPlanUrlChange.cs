using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMasterDataPricingPlanUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMasterDataPricingPlanUrlChange()
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
            var corp_MasterData_PricingPlanHelper = new CorpMasterdata_PricingPlanHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Go To Corp Master Data >> Pricing Plan");
                VisitCorp("masterdata/pricing_plans");
                corp_MasterData_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Click On any Pricing Plan");
                corp_MasterData_PricingPlanHelper.ClickElement("ClickOnMasterPricingPlan");
                corp_MasterData_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("masterdata/manage_pricing_plans/107");
                corp_MasterData_PricingPlanHelper.WaitForWorkAround(1000);

                executionLog.Log("CorpMasterDataPricingPlanUrlChange", "Verify Validation");
                corp_MasterData_PricingPlanHelper.WaitForText("The pricing plan is does not exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMasterDataPricingPlanUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Master Data Pricing Plan Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Master Data Pricing Plan Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Master Data Pricing Plan Url Change");
                        TakeScreenshot("CorpMasterDataPricingPlanUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataPricingPlanUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMasterDataPricingPlanUrlChange");
                        string id = loginHelper.getIssueID("Corp Master Data Pricing Plan Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataPricingPlanUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Master Data Pricing Plan Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Master Data Pricing Plan Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMasterDataPricingPlanUrlChange");
                executionLog.WriteInExcel("Corp Master Data Pricing Plan Url Change", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

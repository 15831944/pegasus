using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMasterDataRateAndFeesUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void  corpMasterDataRateAndFeesUrlChange()
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
            var corp_MasterData_RateAndFeesHelper = new CorpMasterdata_RatesAndFeesHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Go To Employee");
                VisitCorp("masterdata/rates_fees");
                
                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Click On any Rate and Fees");
                corp_MasterData_RateAndFeesHelper.ClickElement("CorpRateFessClickAny");
                corp_MasterData_RateAndFeesHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("masterdata/manage_rates_fees/33");
                corp_MasterData_RateAndFeesHelper.WaitForWorkAround(1000);

                executionLog.Log("CorpMasterDataRateAndFeesUrlChange", "Verify Validation");
                corp_MasterData_RateAndFeesHelper.WaitForText("The Master rate is does not exists." ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMasterDataRateAndFeesUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Corp Master Data Rate And Fees Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Master Data Rate And Fees Url Change", "Bug", "Medium", "Corp rates and fee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Master Data Rate And Fees Url Change");
                        TakeScreenshot("CorpMasterDataRateAndFeesUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataRateAndFeesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMasterDataRateAndFeesUrlChange");
                        string id = loginHelper.getIssueID("Corp Master Data Rate And Fees Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataRateAndFeesUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Master Data Rate And Fees Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Master Data Rate And Fees Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMasterDataRateAndFeesUrlChange");
                executionLog.WriteInExcel("Corp Master Data Rate And Fees Url Change", Status, JIRA, "Corp Master Data Rates And Fees");
            }
        }
    }
}
      
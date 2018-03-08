using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMasterDataAmexRateUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMasterDataAmexRateUrlChange()
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
            var corp_MasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Go To Corp Master Data >> amex rate");
            VisitCorp("masterdata/amex_rates");
            corp_MasterData_AmexRateHelper.WaitForWorkAround(5000);

            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Click On any Master Amex rate");
            corp_MasterData_AmexRateHelper.ClickElement("ClickOnEmployeeCorp");
            corp_MasterData_AmexRateHelper.WaitForWorkAround(4000);

            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Change the url with the url number of another Corp");
            VisitCorp("masterdata/manage_amex_rates/477");
            corp_MasterData_AmexRateHelper.WaitForWorkAround(5000);

            executionLog.Log("CorpMasterDataAmexRateUrlChange", "Verify Validation");
            corp_MasterData_AmexRateHelper.VerifyPageText("The Amex Master rate is does not exists.");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMasterDataAmexRateUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Master Data Amex Rate Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Master Data Amex Rate Url Change", "Bug", "Medium", "Corp Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Master Data Amex Rate Url Change");
                        TakeScreenshot("CorpMasterDataAmexRateUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataAmexRateUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMasterDataAmexRateUrlChange");
                        string id = loginHelper.getIssueID("Corp Master Data Amex Rate Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataAmexRateUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Master Data Amex Rate Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Master Data Amex Rate Url Change");
        //        executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMasterDataAmexRateUrlChange");
                executionLog.WriteInExcel("Corp Master Data Amex Rate Url Change", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

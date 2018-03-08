using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMasterDataMerchantTypeUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void  corpMasterDataMerchantTypeUrlChange()
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
            var corp_MasterData_MerchantType = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());
     

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Go To Corp Master Data >> Merchant Type");
                VisitCorp("masterdata/merchant_types");
               
                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Click On any Merchnat");
                corp_MasterData_MerchantType.ClickElement("ClickOnAnyMerchantType");
                corp_MasterData_MerchantType.WaitForWorkAround(1000);

                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Change the url with the url number of another Corp");
                VisitCorp("masterdata/manage_merchant_types/36");
                corp_MasterData_MerchantType.WaitForWorkAround(4000);

                executionLog.Log("CorpMasterDataMerchantTypeUrlChange", "Verify Validation");
                corp_MasterData_MerchantType.WaitForText("The merchant type is does not exists." ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMasterDataMerchantTypeUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("CorpMaster Data Merchant Type Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CorpMaster Data Merchant Type Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CorpMaster Data Merchant Type Url Change");
                        TakeScreenshot("CorpMasterDataMerchantTypeUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataMerchantTypeUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMasterDataMerchantTypeUrlChange");
                        string id = loginHelper.getIssueID("CorpMaster Data Merchant Type Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMasterDataMerchantTypeUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CorpMaster Data Merchant Type Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CorpMaster Data Merchant Type Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMasterDataMerchantTypeUrlChange");
                executionLog.WriteInExcel("CorpMaster Data Merchant Type Url Change", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

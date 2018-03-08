using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminMasterDataMerchantTypeURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminMasterDataMerchantTypeURLChange()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_MerchantTypeHelper = new MasterData_MerchantTypeHelper(GetWebDriver());


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Goto Master Data>> Merchant Types.");
                VisitOffice("merchant_types");
                masterData_MerchantTypeHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Click On any Merchant Type");
                masterData_MerchantTypeHelper.ClickElement("ClickOnMerchantAdmin");
                masterData_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_merchant_types/568");
                masterData_MerchantTypeHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminMasterDataMerchantTypeURLChange", "Verify Validation");
                masterData_MerchantTypeHelper.WaitForText("The merchant type is does not exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminMasterDataMerchantTypeURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminMaster Data Merchant Type URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminMaster Data Merchant Type URL Change", "Bug", "Medium", "Merchant type Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminMaster Data Merchant Type URL Change");
                        TakeScreenshot("AdminMasterDataMerchantTypeURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataMerchantTypeURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminMasterDataMerchantTypeURLChange");
                        string id = loginHelper.getIssueID("AdminMaster Data Merchant Type URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataMerchantTypeURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("AdminMaster Data Merchant Type URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("AdminMaster Data Merchant Type URL Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminMasterDataMerchantTypeURLChange");
                executionLog.WriteInExcel("AdminMaster Data Merchant Type URL Change", Status, JIRA, "Master Data");
            }
        }
    }
}

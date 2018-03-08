using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMerchantTaskUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMerchantTaskUrlChange()
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
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("CorpMerchantTaskUrlChange", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("CorpMerchantTaskUrlChange", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("CorpMerchantTaskUrlChange", "Goto Merchant");
            VisitCorp("merchants");
            corp_MerchantHelper.WaitForWorkAround(3000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Enter merchant name.");
            corp_MerchantHelper.TypeText("MerchantSeacrhByCompany", "Chy");
            corp_MerchantHelper.WaitForWorkAround(2000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Click On any Merchant");
            corp_MerchantHelper.PressEnter("ClickOnMerchantAny");
            corp_MerchantHelper.WaitForWorkAround(3000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Click On Expand Icon");
            corp_MerchantHelper.Click("//*[@id='menu']/li[1]/a/i");
            corp_MerchantHelper.WaitForWorkAround(1000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Select Activity >> Task");
            corp_MerchantHelper.Select("SelectActivityType", "Tasks");
            corp_MerchantHelper.WaitForWorkAround(2000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Select All in created by field");
            corp_MerchantHelper.SelectByText("CreatedByField", "All");
            corp_MerchantHelper.WaitForWorkAround(2000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Click On Task Subject");
            corp_MerchantHelper.PressEnter("ClickOnMerchantSubject");
            corp_MerchantHelper.WaitForWorkAround(2000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Change the url with the url number of another office");
            VisitCorp("merchants/task/view/41");
            corp_MerchantHelper.WaitForWorkAround(1000);

            executionLog.Log("CorpMerchantTaskUrlChange", "Verify Validation");
            corp_MerchantHelper.WaitForText("You don't have privileges to view this merchant activity.", 10);
            corp_MerchantHelper.WaitForWorkAround(1000);
            }
    catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMerchantTaskUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Merchant Task Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Merchant Task Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Merchant Task Url Change");
                        TakeScreenshot("CorpMerchantTaskUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMerchantTaskUrlChange");
                        string id = loginHelper.getIssueID("Corp Merchant Task Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantTaskUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Merchant Task Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Merchant Task Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMerchantTaskUrlChange");
                executionLog.WriteInExcel("Corp Merchant Task Url Change", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMerchantEmailUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMerchantEmailUrlChange()
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
                executionLog.Log("CorpMerchantEmailUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMerchantEmailUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMerchantEmailUrlChange", "Goto Merchant");
                VisitCorp("merchants");

                executionLog.Log("CorpMerchantEmailUrlChange", "Enter name to search.");
                corp_MerchantHelper.TypeText("MerchantSeacrhByCompany", "Chy Company");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Click On any Merchant");
                corp_MerchantHelper.PressEnter("ClickOnMerchantAny");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Click On Expand Icon");
                corp_MerchantHelper.Click("//*[@id='menu']/li[1]/a/i");

                executionLog.Log("CorpMerchantEmailUrlChange", "Select Activity >> E-mails");
                corp_MerchantHelper.Select("SelectActivityType", "E-Mails");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Select All in ");
                corp_MerchantHelper.SelectByText("CreatedByField", "All");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Click On any email");
                corp_MerchantHelper.ClickElement("OpenEmailCorp");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Change the url with the url number of another office");
                VisitCorp("merchants/e-mail/view/14120");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMerchantEmailUrlChange", "Verify Validation");
                corp_MerchantHelper.WaitForText("You don't have privileges to view this merchant activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMerchantEmailUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Merchant Email Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Merchant Email Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Merchant Email Url Change");
                        TakeScreenshot("CorpMerchantEmailUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMerchantEmailUrlChange");
                        string id = loginHelper.getIssueID("Corp Merchant Email Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantEmailUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Merchant Email Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Merchant Email Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMerchantEmailUrlChange");
                executionLog.WriteInExcel("Corp Merchant Email Url Change", Status, JIRA, "Corp Merchant");
            }
        }
    }
}

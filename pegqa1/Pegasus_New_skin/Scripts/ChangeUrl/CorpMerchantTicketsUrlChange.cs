using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMerchantTicketsUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMerchantTicketsUrlChange()
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

                executionLog.Log("CorpMerchantTicketsUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMerchantTicketsUrlChange", "Goto Merchant");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(5000);

                corp_MerchantHelper.SendKeys("//*[@id='gs_contactNm']", "howard tang");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Click On any Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Click On Expand Icon");
                corp_MerchantHelper.ClickElement("ClickEmpandIcon");

                executionLog.Log("CorpMerchantTicketsUrlChange", "Select Activity >> Tickets");
                corp_MerchantHelper.Select("SelectActivityType", "Tickets");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Select All in created by field");
                corp_MerchantHelper.SelectByText("CreatedByField", "All");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Click On Ticket");
                corp_MerchantHelper.PressEnter("TIcket1_Merchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Change the url with the url number of another office");
                VisitCorp("merchants/ticket/view/41");
                corp_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpMerchantTicketsUrlChange", "Verify Validation");
                corp_MerchantHelper.WaitForText("You don't have privileges to view this merchant activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMerchantTicketsUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("corp Merchant Tickets Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("corp Merchant Tickets Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("corp Merchant Tickets Url Change");
                        TakeScreenshot("CorpMerchantTicketsUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantTicketsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMerchantTicketsUrlChange");
                        string id = loginHelper.getIssueID("corp Merchant Tickets Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantTicketsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("corp Merchant Tickets Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("corp Merchant Tickets Url Change");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMerchantTicketsUrlChange");
                executionLog.WriteInExcel("corp Merchant Tickets Url Change", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
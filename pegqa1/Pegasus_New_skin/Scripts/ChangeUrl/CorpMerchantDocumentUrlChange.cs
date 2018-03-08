using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CorpMerchantDocumentUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void corpMerchantDocumentUrlChange()
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
                executionLog.Log("CorpMerchantDocumentUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpMerchantDocumentUrlChange", "Goto Merchant");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Enter company name to search");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", "Chy Company");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Click On any Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Click On Expand Icon");
                corp_MerchantHelper.ClickElement("ClickEmpandIcon");

                executionLog.Log("CorpMerchantDocumentUrlChange", "Select Activity >> Document");
                corp_MerchantHelper.Select("SelectActivityType", "Documents");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Select All in created by field");
                corp_MerchantHelper.SelectByText("CreatedByField", "All");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Click On Any document");
                corp_MerchantHelper.PressEnter("ClickOnMerchantSubject");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Change the url with the url number of another office");
                VisitCorp("merchants/document/view/41");
                corp_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpMerchantDocumentUrlChange", "Verify Validation");
                corp_MerchantHelper.WaitForText("You don't have privileges to view this merchant activity.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMerchantDocumentUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Merchant Document Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Merchant Document Url Change", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Merchant Document Url Change");
                        TakeScreenshot("CorpMerchantDocumentUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMerchantDocumentUrlChange");
                        string id = loginHelper.getIssueID("Corp Merchant Document Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMerchantDocumentUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Merchant Document Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Merchant Document Url Change");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMerchantDocumentUrlChange");
                executionLog.WriteInExcel("Corp Merchant Document Url Change", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
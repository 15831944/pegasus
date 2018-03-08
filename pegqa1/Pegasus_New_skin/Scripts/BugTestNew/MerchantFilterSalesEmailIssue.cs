using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantFilterSalesEmailIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void merchantFilterSalesEmailIssue()
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
                executionLog.Log("MerchantFilterSalesEmailIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MerchantFilterSalesEmailIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchantFilterSalesEmailIssue", "Goto Merchant");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantFilterSalesEmailIssue", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantFilterSalesEmailIssue", "Select sales email from available column.");
                corp_MerchantHelper.Select("AvailableColumn", "salesEmail");

                executionLog.Log("MerchantFilterSalesEmailIssue", "Move sales email to displayed column.");
                corp_MerchantHelper.ClickElement("ArrowLeft");

                executionLog.Log("MerchantFilterSalesEmailIssue", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(5000);

                corp_MerchantHelper.ClickJS("SalesEmailField");

                executionLog.Log("MerchantFilterSalesEmailIssue", "Enter text in sales email.");
                corp_MerchantHelper.TypeText("SalesEmailField", "testnewonly");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantFilterSalesEmailIssue", "Verify no records to view text on page.");
                corp_MerchantHelper.VerifyPageText("No records to view");
                //corp_MerchantHelper.WaitForWorkAround(7000);

                executionLog.Log("MerchantFilterSalesEmailIssue", "Verify unexpected erro not present on page.");
                corp_MerchantHelper.VerifyTextNot("Oops you are trying to access a non existent page.");
                corp_MerchantHelper.WaitForWorkAround(3000);

                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchantFilterSalesEmailIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchant Filter Sales Email Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchant Filter Sales Email Issue", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchant Filter Sales Email Issue");
                        TakeScreenshot("MerchantFilterSalesEmailIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantFilterSalesEmailIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchantFilterSalesEmailIssue");
                        string id = loginHelper.getIssueID("Merchant Filter Sales Email Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantFilterSalesEmailIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchant Filter Sales Email Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchant Filter Sales Email Issue");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchantFilterSalesEmailIssue");
                executionLog.WriteInExcel("Merchant Filter Sales Email Issue", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
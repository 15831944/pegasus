using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantAdvanceFilterResultPerPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void merchantAdvanceFilterResultPerPage()
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
                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Goto Merchant");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeeAdvanceFilter", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Select number of records to 10.");
                corp_MerchantHelper.SelectByText("ResultsPerPage", "10");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);
                //corp_MerchantHelper.WaitForElementPresent("No.ofResults", 05);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Verify number of records displayed.");
                corp_MerchantHelper.ShowResult(10);
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Select number of records to 20.");
                corp_MerchantHelper.SelectByText("ResultsPerPage", "20");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);
                //corp_MerchantHelper.WaitForElementPresent("No.ofResults", 05);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Verify number of records displayed.");
                corp_MerchantHelper.ShowResult(20);
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Select number of records to 50.");
                corp_MerchantHelper.SelectByText("ResultsPerPage", "50");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);
                //corp_MerchantHelper.WaitForElementPresent("No.ofResults", 05);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Verify number of records displayed.");
                corp_MerchantHelper.ShowResult(50);
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Select number of records to 100.");
                corp_MerchantHelper.SelectByText("ResultsPerPage", "100");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);
                //corp_MerchantHelper.WaitForElementPresent("No.ofResults", 05);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Verify number of records displayed.");
                corp_MerchantHelper.ShowResult(100);
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterResultPerPage", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchantAdvanceFilterResultPerPage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchant Advance Filter Result Per Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchant Advance Filter Result Per Page", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchant Advance Filter Result Per Page");
                        TakeScreenshot("MerchantAdvanceFilterResultPerPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantAdvanceFilterResultPerPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchantAdvanceFilterResultPerPage");
                        string id = loginHelper.getIssueID("Merchant Advance Filter Result Per Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantAdvanceFilterResultPerPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchant Advance Filter Result Per Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchant Advance Filter Result Per Page");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchantAdvanceFilterResultPerPage");
                executionLog.WriteInExcel("Merchant Advance Filter Result Per Page", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
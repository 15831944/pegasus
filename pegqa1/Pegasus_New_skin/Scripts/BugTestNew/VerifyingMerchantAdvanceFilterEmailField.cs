using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyingMerchantAdvanceFilterEmailFieldEmailField : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        public void verifyingMerchantAdvanceFilterEmailFieldEmailField()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username2");
            password = oXMLData.getData("settings/Credentials", "password2");

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
                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Goto Merchant");
                VisitCorp("merchants");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Enter email to be searched");
                corp_MerchantHelper.TypeText("SearchEmail", "test248467@yopmail.com");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Wait for locator to be present.");
                corp_MerchantHelper.WaitForElementPresent("EnterEmail", 10);

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Enter email in email field.");
                corp_MerchantHelper.TypeText("EnterEmail", "test248467@yopmail.com");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Goto Merchant");
                corp_MerchantHelper.ClickElement("Apply");

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Wait for locator to be present.");
                corp_MerchantHelper.WaitForElementPresent("VerifyEmail1", 10);

                executionLog.Log("VerifyingMerchantAdvanceFilterEmailField", "Verify filtered email present on page.");
                corp_MerchantHelper.VerifyPageText("test248467@yopmail.com");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingMerchantAdvanceFilterEmailField");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Merchant Advance Filter Email Field");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Merchant Advance Filter Email Field", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Merchant Advance Filter Email Field");
                        TakeScreenshot("VerifyingMerchantAdvanceFilterEmailField");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingMerchantAdvanceFilterEmailField.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingMerchantAdvanceFilterEmailField");
                        string id = loginHelper.getIssueID("Verifying Merchant Advance Filter Email Field");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingMerchantAdvanceFilterEmailField.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Merchant Advance Filter Email Field"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Merchant Advance Filter Email Field");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingMerchantAdvanceFilterEmailField");
                executionLog.WriteInExcel("Verifying Merchant Advance Filter Email Field", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
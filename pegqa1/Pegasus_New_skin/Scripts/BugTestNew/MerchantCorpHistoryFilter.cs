using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantCorpHistoryFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void merchantCorpHistoryFilter()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {
                executionLog.Log("MerchantCorpHistoryFilter", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MerchantCorpHistoryFilter", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchantCorpHistoryFilter", "Redirect at merchants page.");
                VisitCorp("merchants");

                executionLog.Log("MerchantCorpHistoryFilter", "Click on any Merchant");
                corp_MerchantHelper.ClickElement("ClickOnMerchant");

                executionLog.Log("MerchantCorpHistoryFilter", "Wait for locator to pesent.");
                corp_MerchantHelper.WaitForElementPresent("TaskCreateDate", 10);

                executionLog.Log("MerchantCorpHistoryFilter", "Enter Date in Created Date Filter of History");
                corp_MerchantHelper.TypeText("TaskCreateDate", "2018-12-10");

                executionLog.Log("MerchantCorpHistoryFilter", "Wait for locator to pesent.");
                corp_MerchantHelper.WaitForElementPresent("TaskNoRecord", 10);

                executionLog.Log("MerchantCorpHistoryFilter", "Verify No Record found message is displayed.");
                corp_MerchantHelper.VerifyText("TaskNoRecord", "No records to view");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchantCorpHistoryFilter");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchant Corp History Filter");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchant Corp History Filter", "Bug", "Medium", "Merchant corp", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchant Corp History Filter");
                        TakeScreenshot("MerchantCorpHistoryFilter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantCorpHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchantCorpHistoryFilter");
                        string id = loginHelper.getIssueID("Merchant Corp History Filter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantCorpHistoryFilter.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchant Corp History Filter"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchant Corp History Filter");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchantCorpHistoryFilter");
                executionLog.WriteInExcel("Merchant Corp History Filter", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
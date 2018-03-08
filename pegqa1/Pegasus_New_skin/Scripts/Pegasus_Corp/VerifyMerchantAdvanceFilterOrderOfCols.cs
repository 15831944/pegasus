using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyMerchantAdvanceFilterOrderOfCols : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void verifyMerchantAdvanceFilterOrderOfCols()
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
                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Goto Merchant");
                VisitCorp("merchants");

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of company name column.");
                corp_MerchantHelper.IsElementPresent("HeadCompany");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of contact column.");
                corp_MerchantHelper.IsElementPresent("HeadContact");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of phone column.");
                corp_MerchantHelper.IsElementPresent("HeadPhone");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of email column.");
                corp_MerchantHelper.IsElementPresent("HeadEmail");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select company in displayed columns.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "Company");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click arrow to move column to avail cols.");
                corp_MerchantHelper.ClickElement("RemoveCols");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select contact in displayed columns.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "Contact Name");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click arrow to move column to avail cols");
                corp_MerchantHelper.ClickElement("RemoveCols");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select phone in displayed columns.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "Phone");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click arrow to move column to avail cols");
                corp_MerchantHelper.ClickElement("RemoveCols");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select email in displayed columns.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "E-Mail");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click arrow to move column to avail cols");
                corp_MerchantHelper.ClickElement("RemoveCols");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify company name not present on page.");
                corp_MerchantHelper.IsElementNotPresent("HeadCompany");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify contact name not present on page.");
                corp_MerchantHelper.IsElementNotPresent("HeadContact");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify phone not present on page.");
                corp_MerchantHelper.IsElementNotPresent("HeadPhone");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify email not present on page.");
                corp_MerchantHelper.IsElementNotPresent("HeadEmail");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Redirect at employees page.");
                VisitCorp("employees");

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify page title as employee.");
                VerifyTitle("Employees");

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify page title as merchants");
                VerifyTitle();
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of company column..");
                corp_MerchantHelper.IsElementPresent("HeadCompany5");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify default position of phone column.");
                corp_MerchantHelper.IsElementPresent("HeadPhone6");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Redirect at merchants page.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select company in displayed column.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "Company");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Move comapny 1 step up.");
                corp_MerchantHelper.ClickElement("MoveUp");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Move comapny 1 step up.");
                corp_MerchantHelper.ClickElement("MoveUp");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Move comapny 1 step up.");
                corp_MerchantHelper.ClickElement("MoveUp");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Select phone in displayed column.");
                corp_MerchantHelper.SelectByText("DisplayedCols", "Phone");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Move phone 1 step up.");
                corp_MerchantHelper.ClickElement("MoveDown");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify changed position of company column.");
                corp_MerchantHelper.IsElementPresent("HeadCompany1");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Verify changed position of phone column.");
                corp_MerchantHelper.IsElementPresent("HeadPhone7");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantAdvanceFilterOrderOfCols", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMerchantAdvanceFilterOrderOfCols");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Merchant Advance Filter Order Of Cols");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Merchant Advance Filter Order Of Cols", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Merchant Advance Filter Order Of Cols");
                        TakeScreenshot("VerifyMerchantAdvanceFilterOrderOfCols");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantAdvanceFilterOrderOfCols.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMerchantAdvanceFilterOrderOfCols");
                        string id = loginHelper.getIssueID("Verify Merchant Advance Filter Order Of Cols");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantAdvanceFilterOrderOfCols.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Merchant Advance Filter Order Of Cols"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Merchant Advance Filter Order Of Cols");
             //s   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMerchantAdvanceFilterOrderOfCols");
                executionLog.WriteInExcel("Verify Merchant Advance Filter Order Of Cols", Status, JIRA, "Corp Merchant");
            }
        }
    }
}
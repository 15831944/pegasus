using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantAdvanceFilterMerchantDetails : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void merchantAdvanceFilterMerchantDetails()
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
                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Goto Merchant");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Select number of records to 10.");
                corp_MerchantHelper.SelectByText("ResultsPerPage", "10");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click on advance filter.");
                corp_MerchantHelper.ClickElement("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(2000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Select city in avail columns.");
                corp_MerchantHelper.Select("AvailableCols", "client_details.location_city");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click arrow to move column.");
                corp_MerchantHelper.ClickElement("AddCols");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Select zipcode in avail columns.");
                corp_MerchantHelper.Select("AvailableCols", "client_details.location_postal_code");
                //corp_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click arrow to move column");
                corp_MerchantHelper.ClickElement("AddCols");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(4000);

                corp_MerchantHelper.ClickJS("AdvanceFilter");
                corp_MerchantHelper.WaitForWorkAround(3000);
                //corp_MerchantHelper.WaitForElementPresent("UserDetailEmail", 10);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Enter email in user details.");
                corp_MerchantHelper.TypeText("UserDetailEmail", "dunkin@yopmail.com");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Enter company name in user details.");
                corp_MerchantHelper.TypeText("UserDetailCompany", "DUNKIN");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Enter contact name in user details.");
                corp_MerchantHelper.TypeText("UserDetailContact", "Jobs");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Click on apply button.");
                corp_MerchantHelper.ClickElement("Apply");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Verify merchant name on the page.");
                corp_MerchantHelper.VerifyText("ClickOnMerchant", "DUNKIN");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Verify merchant email on the page.");
                corp_MerchantHelper.VerifyText("Email1", "dunkin@yopmail.com");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Verify merchant contact name on the page.");
                corp_MerchantHelper.VerifyText("Contact1", "Jobs");
                //corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("MerchantAdvanceFilterMerchantDetails", "Logout from the application.");
                VisitCorp("logout");



            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MerchantAdvanceFilterMerchantDetails");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Merchant Advance Filter Merchant Details");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Merchant Advance Filter Merchant Details", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Merchant Advance Filter Merchant Details");
                        TakeScreenshot("MerchantAdvanceFilterMerchantDetails");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantAdvanceFilterMerchantDetails.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MerchantAdvanceFilterMerchantDetails");
                        string id = loginHelper.getIssueID("Merchant Advance Filter Merchant Details");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MerchantAdvanceFilterMerchantDetails.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Merchant Advance Filter Merchant Details"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Merchant Advance Filter Merchant Details");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MerchantAdvanceFilterMerchantDetails");
                executionLog.WriteInExcel("Merchant Advance Filter Merchant Details", Status, JIRA, "Corp Merchant");
            }
        }
    }
}

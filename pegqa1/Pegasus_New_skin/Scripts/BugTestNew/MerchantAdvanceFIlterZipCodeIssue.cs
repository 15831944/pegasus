using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class MerchantAdvanceFIlterZipCodeIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        public void merchantAdvanceFIlterZipCodeIssue()
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

            //        try
            //      {
            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Verify Page title.");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Goto merchants page.");
            VisitCorp("merchants");

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Click on advance filter.");
            corp_MerchantHelper.ClickElement("AdvanceFilter");
            corp_MerchantHelper.WaitForWorkAround(5000);


            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Select Zip Code from available column.");
            corp_MerchantHelper.Select("AvailableColumn", "client_details.pcZip");
            corp_MerchantHelper.WaitForWorkAround(5000);

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Move zip code to displayed column.");
            corp_MerchantHelper.ClickElement("ArrowLeft");

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Click on apply button.");
            corp_MerchantHelper.ClickElement("Apply");

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Wait for locator to be present.");
            corp_MerchantHelper.WaitForElementPresent("ZipText", 10);

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Enter postal code in zip code.");
            corp_MerchantHelper.TypeText("ZipText", "60601");
            corp_MerchantHelper.WaitForWorkAround(3000);

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Verify no records to view text on page.");
            corp_MerchantHelper.VerifyPageText("No records to view");
            corp_MerchantHelper.WaitForWorkAround(5000);

            executionLog.Log("MerchantAdvanceFIlterZipCodeIssue", "Verify unexpected erro not present on page.");
            corp_MerchantHelper.VerifyPageText("Oops you are trying to access a non existent page.");
            corp_MerchantHelper.WaitForWorkAround(3000);

        }
    }
}
/*         catch (Exception e)
         {
             executionLog.Log("Error", e.StackTrace);
             Status = "Fail";

             String counter = executionLog.readLastLine("counter");
             String Description = executionLog.GetAllTextFile("MerchantAdvanceFIlterZipCodeIssue");
             String Error = executionLog.GetAllTextFile("Error");
             if (counter == "")
             {
                 counter = "0";
             }
             bool result = loginHelper.CheckExstingIssue("Merchant Advance FIlter Zip Code Issue");
             if (!result)
             {
                 if (Int16.Parse(counter) < 5)
                 {
                     executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                     loginHelper.CreateIssue("Merchant Advance FIlter Zip Code Issue", "Bug", "Medium", "Master Data Corp page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                     string id = loginHelper.getIssueID("Merchant Advance FIlter Zip Code Issue");
                     TakeScreenshot("MerchantAdvanceFIlterZipCodeIssue");
                     string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                     var location = directoryName + "\\MerchantAdvanceFIlterZipCodeIssue.png";
                     loginHelper.AddAttachment(location, id);
                 }
             }
             else
             {
                 if (Int16.Parse(counter) < 5)
                 {
                     executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                     TakeScreenshot("MerchantAdvanceFIlterZipCodeIssue");
                     string id = loginHelper.getIssueID("Merchant Advance FIlter Zip Code Issue");
                     string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                     var location = directoryName + "\\MerchantAdvanceFIlterZipCodeIssue.png";
                     loginHelper.AddAttachment(location, id);
                     loginHelper.AddComment(loginHelper.getIssueID("Merchant Advance FIlter Zip Code Issue"), "This issue is still occurring");
                 }
             }
             JIRA = loginHelper.getIssueID("Merchant Advance FIlter Zip Code Issue");
             executionLog.DeleteFile("Error");
             throw;

         }
         finally
         {
             executionLog.DeleteFile("MerchantAdvanceFIlterZipCodeIssue");
             executionLog.WriteInExcel("Merchant Advance FIlter Zip Code Issue", Status, JIRA, "Corp Merchant");
         }
     }
 }
}
*/

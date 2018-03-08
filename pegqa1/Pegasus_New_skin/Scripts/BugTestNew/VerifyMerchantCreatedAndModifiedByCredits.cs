using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyMerchantCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyMerchantCreatedAndModifiedByCredits()
        {
            string[] username = null;
            string[] password = null;

            string[] username1 = null;
            string[] password2 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password2 = oXMLData.getData("settings/Credentials", "password");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_MerchantHelper = new Corp_MerchantHelper(GetWebDriver());


            var DBA = "ClientDBA" + RandomNumber(1, 500);

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username1[0], password2[0]);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                VisitOffice("clients");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Click on Create button");
                corp_MerchantHelper.ClickElement("CreateBtn");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Enter DBA name");
                corp_MerchantHelper.TypeText("BusinessName", DBA);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Select the client status");
                corp_MerchantHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "select the responsibity");
                corp_MerchantHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Click on save btn");
                corp_MerchantHelper.ClickElement("SaveBtn");
                corp_MerchantHelper.WaitForWorkAround(5000);

                VisitOffice("logout");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Click on Merchants tab.");
                VisitCorp("merchants");
                corp_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Verify page title as merchants.");
                VerifyTitle();

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Search the same merchants/clien");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", DBA);
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Click on any merchant.");
                corp_MerchantHelper.ClickElement("CickonMercahnt");
                corp_MerchantHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Wait for locator to be present.");
                corp_MerchantHelper.WaitForElementPresent("CreatedBy", 10);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Verify merchant created by credits.");
                corp_MerchantHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Verify merchant modified by credits.");
                corp_MerchantHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Logout from the application.");
                VisitCorp("logout");
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username1[0], password2[0]);

                VisitOffice("clients");
                corp_MerchantHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Search the company Name");
                corp_MerchantHelper.TypeText("EnterClinentToSearch", DBA);
                corp_MerchantHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Click on check boc");
                corp_MerchantHelper.ClickElement("FirstCheckBox");

                executionLog.Log("VerifyMerchantCreatedAndModifiedByCredits", "Delete the client");
                corp_MerchantHelper.ClickJS("DeleteBtn");
                corp_MerchantHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMerchantCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Merchant Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Merchant Created And Modified By Credits", "Bug", "Medium", "Corp Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Merchant Created And Modified By Credits");
                        TakeScreenshot("VerifyMerchantCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMerchantCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Merchant Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMerchantCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Merchant Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Merchant Created And Modified By Credits");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMerchantCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Merchant Created And Modified By Credits", Status, JIRA, "Corp Merchant");
            }
        }
    }
}

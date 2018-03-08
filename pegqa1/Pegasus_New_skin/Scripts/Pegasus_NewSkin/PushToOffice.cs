using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void pushToOffice()
        {

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var corpMasterdata_LanguageHelper = new CorpMasterdata_LanguageHelper(GetWebDriver());
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_MerchantTypeHelper = new CorpMasterdata_MerchantTypeHelper(GetWebDriver());
            var corpMasterData_AmexRateHelper = new CorpMasterData_AmexRateHelper(GetWebDriver());
            var corpMasterdata_OmahaAuthGridHelper = new CorpMasterdata_OmahaAuthGridHelper(GetWebDriver());
            var corpMasterdata_PricingPlanHelper = new CorpMasterdata_PricingPlanHelper(GetWebDriver());

            string[] username = null;
            string[] password = null;
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var Num = RandomNumber(1, 9999).ToString();
            var Nam = "New" + GetRandomNumber();
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PushToOffice", "Login with valid credential");
                Login(username[0], password[0]);

                executionLog.Log("PushToOffice", "Verify Page title");
                VerifyTitle("Dashboard");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);
                
                executionLog.Log("CreateLanguageCorp", "Redirect To Language");
                VisitCorp("languages");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);
                
                executionLog.Log("PushToOffice", "Verify Page title");
                VerifyTitle("Languages");

                executionLog.Log("LanguagePushToOffice", "Click on Push To Office");
                corpMasterdata_LanguageHelper.ClickElement("PushToOfficeLang");

                executionLog.Log("LanguagePushToOffice", "Click ok To Confirm");
                corpMasterdata_LanguageHelper.AcceptAlert();

                executionLog.Log("LanguagePushToOffice", "Verify Confirmation Languges Successfully Pushed to Offices.");
                corpMasterdata_LanguageHelper.WaitForText("Languges Successfully Pushed to Offices.", 30);

                executionLog.Log("CreateMerchnatType", "Redirect to marchant type page");
                VisitCorp("masterdata/merchant_types");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("PushToOffice", "Verify Page title");
                VerifyTitle("Master Merchant Types");
                
                executionLog.Log("CreateMerchnatType", "Click On Push Office");
                corpMasterdata_MerchantTypeHelper.ClickElement("PushOffice");

                executionLog.Log("LanguagePushToOffice", "Click ok To Confirm");
                corpMasterdata_MerchantTypeHelper.AcceptAlert();

                executionLog.Log("CreateOmahaAuthGrid", "Redirect To URL");
                VisitCorp("masterdata/omaha_auth_grids");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("PushToOffice", "Verify Page title");
                VerifyTitle("Corporate Master Omaha Auth Grids");

                executionLog.Log("OmahaAuthGridPushToOffice", "Push To Office");
                corpMasterdata_OmahaAuthGridHelper.ClickElement("PushToOffice");

                executionLog.Log("OmahaAuthGridPushToOffice", "Click ok To Confirm");
                corpMasterdata_OmahaAuthGridHelper.AcceptAlert();

                executionLog.Log("OmahaAuthGridPushToOffice", "Verify Confirmation Languges Successfully Pushed to Offices.");
                corpMasterdata_OmahaAuthGridHelper.WaitForText("Omaha Auth Grids successfully pushed to offices.", 40);

                executionLog.Log("PricingPlanPushToOffice", "Procing plan page");
                VisitCorp("masterdata/pricing_plans");
                corpMasterdata_LanguageHelper.WaitForWorkAround(3000);

                executionLog.Log("PushToOffice", "Verify Page title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("PricingPlanPushToOffice", "Click On Push Office");
                corpMasterdata_PricingPlanHelper.ClickElement("PushOffice");

                executionLog.Log("PushToOffice", "Accept alert message.");
                corpMasterdata_PricingPlanHelper.AcceptAlert();

                executionLog.Log("PushToOffice", "Wait for success message.");
                corpMasterdata_PricingPlanHelper.WaitForText("Pricing Plans successfully pushed to offices.", 40);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Push To Office", "Bug", "Medium", "Master Data page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Push To Office");
                        TakeScreenshot("PushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PushToOffice");
                        string id = loginHelper.getIssueID("Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Push To Office");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PushToOffice");
                executionLog.WriteInExcel("Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
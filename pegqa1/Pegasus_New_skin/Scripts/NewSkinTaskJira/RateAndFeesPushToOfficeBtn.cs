using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class RateAndFeesPushToOfficeBtn : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void rateAndFeesPushToOfficeBtn()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";


            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_RatesAndFeesHelper = new CorpMasterdata_RatesAndFeesHelper(GetWebDriver());

            // Variable
            var name = "PushToOffice" + RandomNumber(33, 999);
            var Code = "1" + RandomNumber(1, 99);

            try
            {
                executionLog.Log("RateAndFeesPushToOfficeBtn", "Login with valid credential  Username");
                Login(username[0],password[0]);

                executionLog.Log("RateAndFeesPushToOfficeBtn", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("RateAndFeesPushToOfficeBtn", "Navigate to rates and fees page");
                VisitCorp("masterdata/rates_fees");

                executionLog.Log("RateAndFeesPushToOfficeBtn", "Click on Push To Office Button");
                corpMasterdata_RatesAndFeesHelper.VerifyText("ClickOnPushOffice", "Push to Offices");
                corpMasterdata_RatesAndFeesHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RateAndFeesPushToOfficeBtn");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Rate And Fees Push To Office Btn");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Rate And Fees Push To Office Btn", "Bug", "Medium", "Rates and fee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Rate And Fees Push To Office Btn");
                        TakeScreenshot("RateAndFeesPushToOfficeBtn");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RateAndFeesPushToOfficeBtn.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RateAndFeesPushToOfficeBtn");
                        string id = loginHelper.getIssueID("Rate And Fees Push To Office Btn");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RateAndFeesPushToOfficeBtn.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Rate And Fees Push To Office Btn"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Rate And Fees Push To Office Btn");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RateAndFeesPushToOfficeBtn");
                executionLog.WriteInExcel("Rate And Fees Push To Office Btn", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

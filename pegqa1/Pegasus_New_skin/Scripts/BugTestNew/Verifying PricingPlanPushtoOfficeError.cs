using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyingPricingPlanPushtoOfficeError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingPricingPlanPushtoOfficeError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_PricingPlanHelper = new CorpMasterdata_PricingPlanHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Procing plan page");
                VisitCorp("masterdata/pricing_plans");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Click On Push Office");
                corpMasterdata_PricingPlanHelper.ClickElement("PushOffice");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Click Ok to Accept alert.");
                corpMasterdata_PricingPlanHelper.AcceptAlert();

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Verify 500 Interanl error not occured.");
                corpMasterdata_PricingPlanHelper.VerifyTextNotPresent("500 Internal Server Error");

                executionLog.Log("VerifyingPricingPlanPushtoOfficeError", "Verify Success message for push to office");
                corpMasterdata_PricingPlanHelper.WaitForText("Pricing Plans successfully pushed to offices.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingPricingPlanPushtoOfficeError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Pricing Plan Push to office error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Pricing Plan Push to office error", "Bug", "Medium", "Corp Pricing plan page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Pricing Plan");
                        TakeScreenshot("VerifyingPricingPlanPushtoOfficeError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingPricingPlanPushtoOfficeError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingPricingPlanPushtoOfficeError");
                        string id = loginHelper.getIssueID("Verify Pricing Plan Push to office error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingPricingPlanPushtoOfficeError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Pricing Plan Push to office error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Pricing Plan Push to office error");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingPricingPlanPushtoOfficeError");
                executionLog.WriteInExcel("Verify Pricing Plan Push to Office error", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

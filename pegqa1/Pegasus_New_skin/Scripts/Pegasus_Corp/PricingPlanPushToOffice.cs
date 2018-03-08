using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PricingPlanPushToOffice : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_Corp")]
        public void pricingPlanPushToOffice()
        {
            string[] username = null;
            string[] username1 = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpMasterdata_PricingPlanHelper = new CorpMasterdata_PricingPlanHelper(GetWebDriver());
            var masterData_PricingPlanHelper = new MasterData_PricingPlanHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_corp");
            username1 = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var name = "Test" + GetRandomNumber();
            var Test = "New" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("PricingPlanPushToOffice", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("PricingPlanPushToOffice", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("PricingPlanPushToOffice", "Procing plan page");
            VisitCorp("masterdata/pricing_plans");
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("PricingPlanPushToOffice", "Verify Page title");
            VerifyTitle("Master Pricing Plans");

            executionLog.Log("PricingPlanPushToOffice", "Click On Create");
            corpMasterdata_PricingPlanHelper.ClickElement("Create");
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("PricingPlanPushToOffice", "Verify Page title");
            VerifyTitle("Manage Master Pricing Plans");

            executionLog.Log("PricingPlanPushToOffice", "Enter Pricing plan name");
            corpMasterdata_PricingPlanHelper.TypeText("PricingPlan", name);

            executionLog.Log("PricingPlanPushToOffice", "Enter Processor Code");
            corpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");

            executionLog.Log("PricingPlanPushToOffice", "Click On Save Btn");
            corpMasterdata_PricingPlanHelper.ClickElement("Save");
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(1000);

            executionLog.Log("PricingPlanPushToOffice", "Wait for success message.");
            corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully created!!", 10);
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

            executionLog.Log("PricingPlanPushToOffice", "Click On Push Office");
            corpMasterdata_PricingPlanHelper.ClickElement("PushOffice");
            corpMasterdata_PricingPlanHelper.AcceptAlert();

            executionLog.Log("PricingPlanPushToOffice", "Wait for success message");
            corpMasterdata_PricingPlanHelper.WaitForText("Pricing Plans successfully pushed to offices.", 15);

            executionLog.Log("PricingPlanPushToOffice", "Logout from the application");
            VisitCorp("logout");

            executionLog.Log("PricingPlanPushToOffice", "Login to application using office credentails");
            Login(username1[0], password[0]);

            executionLog.Log("PricingPlanPushToOffice", "Login to application using office credentails");
            VerifyTitle("Dashboard");

            executionLog.Log("PricingPlanPushToOffice", "Redirect to Pricing");
            VisitOffice("pricing_plans");
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("PricingPlanPushToOffice", "Verify Page title");
            VerifyTitle("Master Pricing Plans");

            executionLog.Log("PricingPlanPushToOffice", "Search pricing plan by name");
            masterData_PricingPlanHelper.TypeText("SearchPricingPlan", name);
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

            executionLog.Log("PricingPlanPushToOffice", "Verify Pricing Plan");
            corpMasterdata_PricingPlanHelper.VerifyPageText(name);
            //corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Logout button");
            VisitOffice("logout");

            executionLog.Log("AmexRatesPushToOffice", "Login with valid credential");
            Login(username[0], password[0]);
            //corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("AmexRatesPushToOffice", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("PricingPlanPushToOffice", "Procing plan page");
            VisitCorp("masterdata/pricing_plans");
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

            executionLog.Log("CreatePricingPlan", "Enter Name to search");
            corpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", name);
            corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

            executionLog.Log("CreatePricingPlan", "Click Delete btn  ");
            corpMasterdata_PricingPlanHelper.ClickElement("DeletePricing");

            executionLog.Log("CreatePricingPlan", "Accept alert message. ");
            corpMasterdata_PricingPlanHelper.AcceptAlert();

            executionLog.Log("CreatePricingPlan", "Wait for delete message. ");
            corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 10);


        }
    

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PricingPlanPushToOffice");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Pricing Plan Push To Office");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Pricing Plan Push To Office", "Bug", "Medium", "pricing plan page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Pricing Plan Push To Office");
                        TakeScreenshot("PricingPlanPushToOffice");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PricingPlanPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PricingPlanPushToOffice");
                        string id = loginHelper.getIssueID("Pricing Plan Push To Office");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PricingPlanPushToOffice.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Pricing Plan Push To Office"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Pricing Plan Push To Office");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PricingPlanPushToOffice");
                executionLog.WriteInExcel("Pricing Plan Push To Office", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
     
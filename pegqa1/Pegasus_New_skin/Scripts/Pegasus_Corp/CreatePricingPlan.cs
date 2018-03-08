using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreatePricingPlan : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createPricingPlan()
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

                executionLog.Log("CreatePricingPlan", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreatePricingPlan", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePricingPlan", "Procing plan page");
                VisitCorp("masterdata/pricing_plans");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePricingPlan", "Verify Page title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Click On Create");
                corpMasterdata_PricingPlanHelper.ClickElement("Create");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePricingPlan", "Verify Page title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Enter Pricing plan name");
                corpMasterdata_PricingPlanHelper.TypeText("PricingPlan", name);

                executionLog.Log("CreatePricingPlan", "Enter Processor Code");
                corpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");

                executionLog.Log("CreatePricingPlan", "Click On Save Btn");
                corpMasterdata_PricingPlanHelper.ClickElement("Save");

                executionLog.Log("CreatePricingPlan", "Wait for success message.");
                corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully created!!", 10);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePricingPlan", "Click On Push Office");
                corpMasterdata_PricingPlanHelper.ClickElement("PushOffice");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                corpMasterdata_PricingPlanHelper.AcceptAlert();
                corpMasterdata_PricingPlanHelper.WaitForText("Pricing Plans successfully pushed to offices.", 40);

                executionLog.Log("CreatePricingPlan", "Procing plan page");
                VisitCorp("masterdata/pricing_plans");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePricingPlan", "Verify Page title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Enter Name to search");
                corpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", name);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("CreatePricingPlan", "Click Delete btn  ");
                corpMasterdata_PricingPlanHelper.ClickElement("DeletePricing");

                executionLog.Log("CreatePricingPlan", "Accept alert message. ");
                corpMasterdata_PricingPlanHelper.AcceptAlert();

                executionLog.Log("CreatePricingPlan", "Wait for delete message. ");
                corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 10);

                VisitCorp("logout");
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreatePricingPlan");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Pricing Plan");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Pricing Plan", "Bug", "Medium", "Pricing plan page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Pricing Plan");
                        TakeScreenshot("CreatePricingPlan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreatePricingPlan");
                        string id = loginHelper.getIssueID("Create Pricing Plan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreatePricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Pricing Plan"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Pricing Plan");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreatePricingPlan");
                executionLog.WriteInExcel("Create Pricing Plan", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
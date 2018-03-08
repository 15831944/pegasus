using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class PricingPlan : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void pricingPlan()
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

            // Variable
            var name = "Test" + RandomNumber(99, 999);
            var Test = "New" + RandomNumber(99, 999);
            var Editedname = "New Name" + RandomNumber(1, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PricingPlan", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("PricingPlan", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePricingPlan", "Redirect at create page.");
                VisitCorp("masterdata/manage_pricing_plans");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("PricingPlan", "Verify Page title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Enter Pricing plan name");
                corpMasterdata_PricingPlanHelper.TypeText("PricingPlan", name);

                executionLog.Log("CreatePricingPlan", "Enter Processor Code");
                corpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");

                executionLog.Log("CreatePricingPlan", "Click On Save Btn");
                corpMasterdata_PricingPlanHelper.ClickElement("Save");
                //corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("CreatePricingPlan", "Wait for success message.");
                corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully created!!", 10);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("PricingPlan", "Search Pricing plan by name.");
                corpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", name);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("PricingPlan", "Click on Edit");
                corpMasterdata_PricingPlanHelper.ClickElement("ClickOnEdit");
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("PricingPlan", "Enter Processor name");
                corpMasterdata_PricingPlanHelper.TypeText("PricingPlan", Editedname);

                executionLog.Log("CreatePricingPlan", "Enter Processor Code");
                corpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");

                executionLog.Log("PricingPlan", "Click Save");
                corpMasterdata_PricingPlanHelper.ClickElement("Save");
                //corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("PricingPlan", "Verify text present");
                corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully updated!!", 50);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("PricingPlan", "Search");
                corpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", Editedname);
                corpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("PricingPlan", "Click to Delete pricing");
                corpMasterdata_PricingPlanHelper.ClickElement("DeletePricing");

                executionLog.Log("PricingPlan", "Click ok to confirm");
                corpMasterdata_PricingPlanHelper.AcceptAlert();

                executionLog.Log("PricingPlan", "Verify page text");
                corpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 40);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PricingPlan");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Pricing Plan");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Pricing Plan", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Pricing Plan");
                        TakeScreenshot("PricingPlan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PricingPlan");
                        string id = loginHelper.getIssueID("Pricing Plan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Pricing Plan"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Pricing Plan");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PricingPlan");
                executionLog.WriteInExcel("Pricing Plan", Status, JIRA, "Corp Master Data");
            }
        }
    }
}
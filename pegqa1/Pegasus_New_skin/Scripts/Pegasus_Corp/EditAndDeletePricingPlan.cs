using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditAndDeletePricingPlan : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void editAndDeletePricingPlan()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var CorpMasterdata_PricingPlanHelper = new CorpMasterdata_PricingPlanHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Test" + RandomNumber(99, 999);
            var Test = "New" + RandomNumber(99, 999);
            var NewNmae = "New Name" + RandomNumber(1, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EditAndDeletePricingPlan", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("EditAndDeletePricingPlan", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreatePricingPlan", "Procing plan page");
                VisitCorp("masterdata/pricing_plans");
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeletePricingPlan", "Verify Page title");
                VerifyTitle("Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Click On Create");
                CorpMasterdata_PricingPlanHelper.ClickElement("Create");
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeletePricingPlan", "Verify Page title");
                VerifyTitle("Manage Master Pricing Plans");

                executionLog.Log("CreatePricingPlan", "Enter Pricing plan name");
                CorpMasterdata_PricingPlanHelper.TypeText("PricingPlan", name);

                executionLog.Log("CreatePricingPlan", "Select Processor Code");
                CorpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");

                executionLog.Log("CreatePricingPlan", "Click On Save Btn");
                CorpMasterdata_PricingPlanHelper.ClickElement("Save");
                //CorpMasterdata_PricingPlanHelper.WaitForWorkAround(5000);

                executionLog.Log("CreatePricingPlan", "Wait for success message.");
                CorpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully created!!", 10);
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeletePricingPlan", "Search");
                CorpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", name);
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeletePricingPlan", "Click on Edit");
                CorpMasterdata_PricingPlanHelper.ClickElement("ClickOnEdit");

                executionLog.Log("EditAndDeletePricingPlan", "Enter Processor name");
                CorpMasterdata_PricingPlanHelper.TypeText("PricingPlan", NewNmae);

                executionLog.Log("CreatePricingPlan", "Enter Processor Code");
                CorpMasterdata_PricingPlanHelper.SelectByText("SelectProcessor", "First Data Omaha");
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(1000);

                executionLog.Log("EditAndDeletePricingPlan", "Click Save");
                CorpMasterdata_PricingPlanHelper.ClickElement("Save");
                

                executionLog.Log("EditAndDeletePricingPlan", "Verify text present");
                CorpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully updated!!", 10);
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeletePricingPlan", "Search");
                CorpMasterdata_PricingPlanHelper.TypeText("SearchPricingPlan", NewNmae);
                CorpMasterdata_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditAndDeletePricingPlan", "Click to Delete pricing");
                CorpMasterdata_PricingPlanHelper.ClickElement("DeletePricing");
                //CorpMasterdata_PricingPlanHelper.WaitForWorkAround(3000);

                executionLog.Log("EditAndDeletePricingPlan", "Click ok to confirm");
                CorpMasterdata_PricingPlanHelper.AcceptAlert();

                executionLog.Log("EditAndDeletePricingPlan", "Verify page text");
                CorpMasterdata_PricingPlanHelper.WaitForText("The pricing plan is successfully deleted!!", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditAndDeletePricingPlan");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit And Delete Pricing Plan");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit And Delete Pricing Plan", "Bug", "Medium", "Pricing page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit And Delete Pricing Plan");
                        TakeScreenshot("EditAndDeletePricingPlan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeletePricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditAndDeletePricingPlan");
                        string id = loginHelper.getIssueID("Edit And Delete Pricing Plan");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditAndDeletePricingPlan.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit And Delete Pricing Plan"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit And Delete Pricing Plan");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditAndDeletePricingPlan");
                executionLog.WriteInExcel("Edit And Delete Pricing Plan", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

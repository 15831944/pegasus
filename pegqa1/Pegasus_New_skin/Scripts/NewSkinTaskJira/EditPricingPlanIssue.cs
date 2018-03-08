using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditPricingPlanIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editPricingPlanIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var masterData_PricingPlanHelper = new MasterData_PricingPlanHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditPricingPlanIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("EditPricingPlanIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditPricingPlanIssue", "Go to Pricing Plan page");
                VisitOffice("pricing_plans");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditPricingPlanIssue", "Verify title");
                VerifyTitle("Master Pricing Plans");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);


                executionLog.Log("EditPricingPlanIssue", "Open a Plan");
                masterData_PricingPlanHelper.ClickElement("Plan");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);


                executionLog.Log("EditPricingPlanIssue", "Verify title");
                VerifyTitle("Manage Master Pricing Plans");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);


                executionLog.Log("EditPricingPlanIssue", "Click on 'Save'button");
                masterData_PricingPlanHelper.ClickElement("Save");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("EditPricingPlanIssue", "Verify no validation message displayed");
                masterData_PricingPlanHelper.VerifyTextNotPresent("The pricing plan is already exists");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditPricingPlanIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Pricing Plan Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Pricing Plan Issue", "Bug", "Medium", "Pricing Plan page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Pricing Plan Issue");
                        TakeScreenshot("EditPricingPlanIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPricingPlanIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditPricingPlanIssue");
                        string id = loginHelper.getIssueID("Edit Pricing Plan Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditPricingPlanIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Pricing Plan Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Pricing Plan Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditPricingPlanIssue");
                executionLog.WriteInExcel("Edit Pricing Plan Issue", Status, JIRA, "Master Data");
            }
        }
    }
}
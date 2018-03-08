using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AdminMasterDataPricingPlanURLChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void adminMasterDataPricingPlanURLChange()
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


            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Goto Master Data >> pricicng Plan");
                VisitOffice("pricing_plans");

                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Click On any Pricing Plan");
                masterData_PricingPlanHelper.ClickElement("OpenPricingPlan");
                masterData_PricingPlanHelper.WaitForWorkAround(2000);

                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Change the url with the url number of another office");
                VisitOffice("manage_pricing_plans/1642");
                masterData_PricingPlanHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminMasterDataPricingPlanURLChange", "Verify Validation");
                masterData_PricingPlanHelper.WaitForText("The pricing plan is does not exists.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminMasterDataPricingPlanURLChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AdminMaster Data Pricing Plan URL Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AdminMaster Data Pricing Plan URL Change", "Bug", "Medium", "Pricing Plan", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AdminMaster Data Pricing Plan URL Change");
                        TakeScreenshot("AdminMasterDataPricingPlanURLChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataPricingPlanURLChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminMasterDataPricingPlanURLChange");
                        string id = loginHelper.getIssueID("AdminMaster Data Pricing Plan URL Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminMasterDataPricingPlanURLChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("AdminMaster Data Pricing Plan URL Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("AdminMaster Data Pricing Plan URL Change");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminMasterDataPricingPlanURLChange");
                executionLog.WriteInExcel("AdminMaster Data Pricing Plan URL Change", Status, JIRA, "Master Data");
            }
        }
    }
}

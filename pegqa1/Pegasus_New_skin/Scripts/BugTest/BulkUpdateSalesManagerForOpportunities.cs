using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BulkUpdateSalesManagerForOpportunities : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdateSalesManagerForOpportunities()
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
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());

            // Variable
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Verify Page title");
                VerifyTitle("Dashboard");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Visit opportunities page.");
                VisitOffice("opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Verify page title..");
                VerifyTitle("Opportunities");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Click On first Check Box");
                office_OpportunitiesHelper.ClickElement("CheckboxOpportunity");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Click On Bulk Update");
                office_OpportunitiesHelper.ClickElement("ClickOnBulkUpdate");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Change Sale Manager");
                office_OpportunitiesHelper.ClickElement("ChangeSaleManager");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Select Sales Manager");
                office_OpportunitiesHelper.SelectDropDownByText("//*[@id='OpportunitySalesManagerUpdateSalesManagerId']", "Howard Tang");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Click on Update button");
                office_OpportunitiesHelper.ClickDisplayed("//button[text()='Update']");
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Accept alert message.");
                office_OpportunitiesHelper.AcceptAlert();
                office_OpportunitiesHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdateSalesManagerForOpportunities", "Wait for success message.");
                office_OpportunitiesHelper.WaitForText("records updated successfully", 30);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BulkUpdateSalesManagerForOpportunities");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Bulk Update Sales Manager For Opportunities");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Bulk Update Sales Manager For Opportunities", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Bulk Update Sales Manager For Opportunities");
                        TakeScreenshot("BulkUpdateSalesManagerForOpportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSalesManagerForOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BulkUpdateSalesManagerForOpportunities");
                        string id = loginHelper.getIssueID("Bulk Update Sales Manager For Opportunities");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BulkUpdateSalesManagerForOpportunities.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Bulk Update Sales Manager For Opportunities"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Bulk Update Sales Manager For Opportunities");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BulkUpdateSalesManagerForOpportunities");
                executionLog.WriteInExcel("Bulk Update Sales Manager For Opportunities", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
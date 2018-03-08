using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BulkUpdatesChangeResponsibiltyForLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void bulkUpdatesChangeResponsibiltyForLead()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // Variable

            var FirstName = "Test" + RandomNumber(1, 99);
            var LastName = "Tester" + RandomNumber(1, 99);
            var Number = "12345678" + RandomNumber(10, 99);
            String JIRA = "";
            String Status = "Pass";

            //try
            //{

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Visit Leads page");
                VisitOffice("leads");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Verify page title.");
                VerifyTitle("Leads");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Click On first Check Box");
                office_LeadsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Click On Bulk Update");
                office_LeadsHelper.ClickElement("ClickOnBulkUpdate");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Change Sale Manager");
                office_LeadsHelper.ClickElement("ChangeSaleManager");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Select Sales MANAGER");
                office_LeadsHelper.SelectByText("SelectSalesManager", "Howard Tang");

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Click on Update button");
                office_LeadsHelper.ClickOnDisplayed("ClickUpdate");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("BulkUpdatesChangeResponsibiltyForLead", "Verify success message");
                office_LeadsHelper.WaitForText("records updated successfully", 30);

            //}
            //catch (Exception e)
            //{
            //    executionLog.Log("Error", e.StackTrace);
            //    Status = "Fail";

            //    String counter = executionLog.readLastLine("counter");
            //    String Description = executionLog.GetAllTextFile("BulkUpdatesChangeResponsibiltyForLead");
            //    String Error = executionLog.GetAllTextFile("Error");
            //    Console.WriteLine(Error);
            //    if (counter == "")
            //    {
            //        counter = "0";
            //    }
            //    bool result = loginHelper.CheckExstingIssue("Bulk Updates Change Responsibilty For Lead");
            //    if (!result)
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            loginHelper.CreateIssue("BulkUpdates Change Responsibilty For Lead", "Bug", "Medium", "Office leads", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
            //            string id = loginHelper.getIssueID("Bulk Updates Change Responsibilty For Lead");
            //            TakeScreenshot("BulkUpdatesChangeResponsibiltyForLead");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\BulkUpdatesChangeResponsibiltyForLead.png";
            //            loginHelper.AddAttachment(location, id);
            //        }
            //    }
            //    else
            //    {
            //        if (Int16.Parse(counter) < 9)
            //        {
            //            executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
            //            TakeScreenshot("Bulk UpdatesChangeResponsibiltyForLead");
            //            string id = loginHelper.getIssueID("Bulk Updates Change Responsibilty For Lead");
            //            string directoryName = loginHelper.GetnewDirectoryName(GetPath());
            //            var location = directoryName + "\\BulkUpdatesChangeResponsibiltyForLead.png";
            //            loginHelper.AddAttachment(location, id);
            //            loginHelper.AddComment(loginHelper.getIssueID("Bulk Updates Change Responsibilty For Lead"), "This issue is still occurring");
            //        }
            //    }
            //    JIRA = loginHelper.getIssueID("Bulk Updates Change Responsibilty For Lead");
            //    //    executionLog.DeleteFile("Error");
            //    throw;

            //}
            //finally
            //{
            //    executionLog.DeleteFile("BulkUpdatesChangeResponsibiltyForLead");
            //    executionLog.WriteInExcel("Bulk Updates Change Responsibilty For Lead", Status, JIRA, "Leads Management");
            //}
        }
    }
}

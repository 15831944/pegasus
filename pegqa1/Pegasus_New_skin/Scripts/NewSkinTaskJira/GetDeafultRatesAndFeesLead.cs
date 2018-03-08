using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class GetDeafultRatesAndFeesLead : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void getDeafultRatesAndFeesLead()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("GetDeafultRatesAndFeesLead", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Redirect at create leads page.");
                VisitOffice("leads/create");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Enter Company DBA");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click on Assignments");
                office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Select Responsibities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Wait for Confirmation");
                office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click on rate and fee tab");
                office_LeadsHelper.ClickElement("ClickOnRateFees");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDeafultRatesAndFeesLead", " Select Processor RF");
                office_LeadsHelper.Select("SelectProcessorRFL", "First Data Omaha");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", " Seleect Merchant RF");
                office_LeadsHelper.Select("SeleectMerchantRFL", "Test201603110126447213");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", " Select Accepting method.");
                office_LeadsHelper.Select("LeadAcceptingMethod", "Manually Swiped");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click On Get Default Rates");
                office_LeadsHelper.ClickElement("ClickOnGetDefaultRatesL");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Accept ALERT");
                office_LeadsHelper.AcceptAlert();
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Verify populated field");
                office_LeadsHelper.IsElementPresent("VerifyPopulatedFiedlL");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Redirect To leads page. ");
                VisitOffice("leads");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("GetDeafultRatesAndFeesLead", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("GetDeafultRatesAndFeesLead", "Goto leads/recyclebin ");
                VisitOffice("leads/recyclebin");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Verify title as recycled leads.");
                VerifyTitle("Recycled Leads");

                executionLog.Log("GetDeafultRatesAndFeesLead", "Click on delete icon.");
                office_LeadsHelper.ClickElement("DeleteLeadPer");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("GetDeafultRatesAndFeesLead", "Verify permanently delete confoirmation message.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("GetDeafultRatesAndFeesLead");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Get Deafult Rates And Fees Lead");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Get Deafult Rates And Fees Lead", "Bug", "Medium", "Create Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Get Deafult Rates And Fees Lead");
                        TakeScreenshot("GetDeafultRatesAndFeesLead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\GetDeafultRatesAndFeesLead.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("GetDeafultRatesAndFeesLead");
                        string id = loginHelper.getIssueID("Get Deafult Rates And Fees Lead");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\GetDeafultRatesAndFeesLead.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Get Deafult Rates And Fees Lead"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Get Deafult Rates And Fees Lead");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("GetDeafultRatesAndFeesLead");
                executionLog.WriteInExcel("Get Deafult Rates And Fees Lead", Status, JIRA, "Leads Management");
            }
        }
    }
}
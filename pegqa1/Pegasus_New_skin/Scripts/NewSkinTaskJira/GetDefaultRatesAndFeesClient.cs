using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class GetDefaultRatesAndFeesClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void getDefaultRatesAndFeesClient()
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
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // VARIABLE
            var DBA = "Client" + RandomNumber(111, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("GetDefaultRatesAndFeesClient", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("GetDefaultRatesAndFeesClient", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("GetDefaultRatesAndFeesClient", "Redirect at create client page.");
                VisitOffice("clients/create");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("GetDefaultRatesAndFeesClient", "Click on rates and fee tab.");
                office_ClientsHelper.ClickElement("RatesAndFee");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDefaultRatesAndFeesClient", " SelectProcessorRF");
                office_ClientsHelper.Select("SelectProcessorRF", "First Data Omaha");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("GetDefaultRatesAndFeesClient", " Select Merchant RF");
                office_ClientsHelper.Select("SeleectMerchantRF", "TestMerchant201603290147173188");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDefaultRatesAndFeesClient", " SelectProcessorRF");
                office_ClientsHelper.Select("MethodOfAcceptingRF", "Manually swiped");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDefaultRatesAndFeesClient", "Click On Get Default Rates");
                office_ClientsHelper.ClickElement("ClickOnGetDefaultRates");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDefaultRatesAndFeesClient", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("GetDefaultRatesAndFeesClient", "Verify");
                office_ClientsHelper.IsElementPresent("VerifyPopulatedFiedl");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("GetDefaultRatesAndFeesClient");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Get Default Rates And Fees Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Get Default Rates And Fees Client", "Bug", "Medium", "Rate and fee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Get Default Rates And Fees Client");
                        TakeScreenshot("GetDefaultRatesAndFeesClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\GetDefaultRatesAndFeesClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("GetDefaultRatesAndFeesClient");
                        string id = loginHelper.getIssueID("Get Default Rates And Fees Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\GetDefaultRatesAndFeesClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Get Default Rates And Fees Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Get Default Rates And Fees Client");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("GetDefaultRatesAndFeesClient");
                executionLog.WriteInExcel("Get Default Rates And Fees Client", Status, JIRA, "Client Management");
            }
        }
    }
}
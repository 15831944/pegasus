using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientAddresses : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void clientAddresses()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            var DBA = "Client" + RandomNumber(222, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ClientAddresses", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientAddresses", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientAddresses", "Got to Create Client");
                VisitOffice("clients/create");

                executionLog.Log("ClientAddresses", "Verify title");
                VerifyTitleMerchantClient();

                executionLog.Log("ClientAddresses", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientAddresses", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientAddresses", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientAddresses", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientAddresses", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("ClientAddresses", "Click on company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("Client", "Enter Client federal tax id.");
                office_ClientsHelper.TypeText("FederalTaxID", "111122222");

                executionLog.Log("ClientAddresses", "Enter Address Line 1 Mailing Address");
                office_ClientsHelper.TypeText("AddressLine1MailingAddress", "Test");

                executionLog.Log("ClientAddresses", "Enter Mailing Zip Code");
                office_ClientsHelper.TypeText("ZipCodeMailingAddress", "60601");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientAddresses", "Click On Client Contact Tab");
                office_ClientsHelper.ClickElement("ContactDetails");

                executionLog.Log("ClientAddresses", "Enetr Client First Name");
                office_ClientsHelper.TypeText("ContactFirstName", "My Client");

                executionLog.Log("ClientAddresses", "Enter ZipCode");
                office_ClientsHelper.TypeText("ContactZipCode", "60601");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientAddresses", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("ClientAddresses", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientAddresses", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientAddresses", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("ClientAddresses", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientAddresses", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("ClientAddresses", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("ClientAddresses", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientAddresses", "Select the all in responsibity");
                office_ClientsHelper.SelectByText("ClientResponsibityRecycle", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientAddresses", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientAddresses", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientAddresses", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientAddresses");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Addresses");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Addresses", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Addresses");
                        TakeScreenshot("ClientAddresses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientAddresses.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientAddresses");
                        string id = loginHelper.getIssueID("Client Addresses");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientAddresses.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Addresses"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Addresses");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientAddresses");
                executionLog.WriteInExcel("Client Addresses", Status, JIRA, "Client Management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientZipCode : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void clientZipCode()
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

            // Variable
            var DBA = "Client" + RandomNumber(1222, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientZipCode", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ClientZipCode", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientZipCode", "Create Client");
                VisitOffice("clients/create");

                executionLog.Log("ClientZipCode", "Verify page title");
                VerifyTitleMerchantClient();

                executionLog.Log("ClientZipCode", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientZipCode", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientZipCode", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientZipCode", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCode", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("ClientZipCode", "Wait for element to be visible.");
                office_ClientsHelper.WaitForElementPresent("CompanyDetails", 10);

                executionLog.Log("ClientZipCode", "Goto Company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("ClientZipCode", "Enter Client federal tax id.");
                office_ClientsHelper.TypeText("FederalTaxID", "111122222");

                executionLog.Log("ClientZipCode", "Enter Mail Client Address");
                office_ClientsHelper.TypeText("AddressLine1MailingAddress", "Test");

                executionLog.Log("ClientZipCode", "Enter Mailing Zip Code");
                office_ClientsHelper.TypeText("ZipCodeMailingAddress", "60601");

                executionLog.Log("ClientZipCode", "Click On Client Contact Tab");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCode", "Enetr Client First Name");
                office_ClientsHelper.TypeText("ContactFirstName", "My Client");

                executionLog.Log("ClientZipCode", "Enter Zip Code");
                office_ClientsHelper.TypeText("ContactZipCode", "60601");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientZipCode", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("ClientZipCode", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientZipCode", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCode", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("ClientZipCode", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientZipCode", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("ClientZipCode", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("ClientZipCode", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCode", "Select all in responsibity");
                office_ClientsHelper.SelectByText("ClientResponsibityRecycle", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCode", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientZipCode", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientZipCode", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientZipCode");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Zip Code");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Zip Code", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Zip Code");
                        TakeScreenshot("ClientZipCode");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientZipCode.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientZipCode");
                        string id = loginHelper.getIssueID("Client Zip Code");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientZipCode.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Zip Code"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Zip Code");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientZipCode");
                executionLog.WriteInExcel("Client Zip Code", Status, JIRA, "Client Management");
            }
        }
    }
}
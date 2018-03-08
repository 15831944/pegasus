using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class ClientZipCodeAutoPopulationIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void clientZipCodeAutoPopulationIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var DBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Visit Craete Client");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click Cancel");
                office_ClientsHelper.ClickForce("CancelOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Visit Craete Client");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", "Client DBA");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for element to be visible.");
                office_ClientsHelper.WaitForElementPresent("CompanyDetails", 10);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Goto Company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Client Company DBA Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Client federal tax id.");
                office_ClientsHelper.TypeText("FederalTaxID", "111122222");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Client Bussiness Legal Name.");
                office_ClientsHelper.TypeText("BussinessLegalName", "LegalCli");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select Status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Select Responsibilties");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter company details zipcode.");
                office_ClientsHelper.TypeText("ZipCodeMailingAddress", "60601");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on contact tab");
                office_ClientsHelper.ClickElement("ClickOnContactTab");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter the Conatct Title");
                office_ClientsHelper.TypeText("ContactTitle", "(785) 786-8768");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on owner tab.");
                office_ClientsHelper.ClickForce("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter zip code.");
                office_ClientsHelper.TypeText("OwnerZipCode", "60601");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on save button.");
                office_ClientsHelper.ClickElement("OwnerSave");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client data updated successfully. ", 10);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Visit Client page.");
                VisitOffice("clients");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Verify page title.");
                VerifyTitle();

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter created client company name.");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(6000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on searched company name.");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on comapany details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Verify all field populated automatically on entering a valid zip code.");
                office_ClientsHelper.VerifyAutoPopulation();

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on save button.");
                office_ClientsHelper.ClickElement("Save");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(5000);

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
                office_ClientsHelper.selectOwner("//*[@id='gs_first_name']");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientZipCodeAutoPopulationIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientZipCodeAutoPopulationIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("ClientZipCodeAutoPopulationIssue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("ClientZipCodeAutoPopulationIssue", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("ClientZipCodeAutoPopulationIssue");
                        TakeScreenshot("ClientZipCodeAutoPopulationIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientZipCodeAutoPopulationIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientZipCodeAutoPopulationIssue");
                        string id = loginHelper.getIssueID("ClientZipCodeAutoPopulationIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientZipCodeAutoPopulationIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("ClientZipCodeAutoPopulationIssue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("ClientZipCodeAutoPopulationIssue");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientZipCodeAutoPopulationIssue");
                executionLog.WriteInExcel("ClientZipCodeAutoPopulationIssue", Status, JIRA, "Lead Management");
            }
        }
    }
}
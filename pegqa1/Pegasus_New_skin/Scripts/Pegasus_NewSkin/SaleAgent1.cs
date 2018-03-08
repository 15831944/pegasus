using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class SaleAgent1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void saleAgent1()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects5
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agent_1099SalesAgentHelper = new Agent_1099SalesAgentHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "usernameSale");
            password = oXMLData.getData("settings/Credentials", "PasswordSale");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("SaleAgent1", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("SaleAgent1", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("SaleAgent1", "Redirect at Create Lead");
            VisitOffice("leads/create");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Click on Save");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");

            executionLog.Log("SaleAgent1", "Enter First Name");
            agent_1099SalesAgentHelper.TypeText("EnterFirstName", FName);

            executionLog.Log("SaleAgent1", "Enter Last Name");
            agent_1099SalesAgentHelper.TypeText("EnterLastName", LName);

            executionLog.Log("SaleAgent1", "Company DBA Name");
            agent_1099SalesAgentHelper.TypeText("LeadCompanyName", CDBA);

            executionLog.Log("SaleAgent1", "Select Lead Status");
            agent_1099SalesAgentHelper.SelectByText("SelectLeadStatus", "New");

            executionLog.Log("SaleAgent1", "Select Responsibilities");
            agent_1099SalesAgentHelper.SelectByText("SelectResponsibities", "Howard Tang");

            executionLog.Log("SaleAgent1", "Click Save button");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            var loc = "//h3[text()='Existing Leads']";
            if (agent_1099SalesAgentHelper.IsElementPresent(loc))
            {

                executionLog.Log("SaleAgent1", "Lead Dublicate button");
                agent_1099SalesAgentHelper.ClickOnDisplayed("CraeteLeadDub");
                agent_1099SalesAgentHelper.WaitForText("Lead saved successfully.", 10);
            }

            else {

                executionLog.Log("SaleAgent1", "Lead Saved confirmation");
                agent_1099SalesAgentHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("SaleAgent1", "Redirect at Create Lead");
                VisitOffice("leads/create");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Click on Cancel");
                agent_1099SalesAgentHelper.ClickElement("CancelOpp");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Verify page title.");
                agent_1099SalesAgentHelper.VerifyText("VerifyTextPresentLead", "Leads");

                executionLog.Log("SaleAgent1", "Redirect at Create Lead");
                VisitOffice("leads/create");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Click Save button");
                agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");
                //agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Enter First Name");
                agent_1099SalesAgentHelper.TypeText("EnterFirstName", FName);

                executionLog.Log("SaleAgent1", "Enter Last Name");
                agent_1099SalesAgentHelper.TypeText("EnterLastName", LName);

                executionLog.Log("SaleAgent1", "Lead Company DBA Name");
                agent_1099SalesAgentHelper.TypeText("LeadCompanyName", CDBA);

                executionLog.Log("SaleAgent1", "Select Lead Status");
                agent_1099SalesAgentHelper.SelectByText("SelectLeadStatus", "New");

                executionLog.Log("SaleAgent1", "Select Responsibities");
                agent_1099SalesAgentHelper.SelectByText("SelectResponsibities", "Howard Tang");

                executionLog.Log("SaleAgent1", "Click Save button");
                agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            }
            if (agent_1099SalesAgentHelper.IsElementPresent(loc))
            {

                executionLog.Log("SaleAgent1", "Click on Lead Dublicate button");
                agent_1099SalesAgentHelper.ClickOnDisplayed("CraeteLeadDub");
                agent_1099SalesAgentHelper.WaitForWorkAround(4000);

                executionLog.Log("SaleAgent1", "Goto Lead Page");
                VisitOffice("leads");
                agent_1099SalesAgentHelper.WaitForWorkAround(4000);

                executionLog.Log("SaleAgent1", "Select First Lead");
                agent_1099SalesAgentHelper.ClickElement("ClickOn1stOpp");

                executionLog.Log("SaleAgent1", "Select Second Lead");
                agent_1099SalesAgentHelper.ClickElement("ClickOn2ndOpp");

                executionLog.Log("SaleAgent1", "Click on Merge");
                agent_1099SalesAgentHelper.ClickElement("ClickOnMergeRecords");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Click on Lead Company Radio Button");
                agent_1099SalesAgentHelper.ClickElement("LeadCompyRadioBtn");
                agent_1099SalesAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("SaleAgent1", "Click Merge Button");
                agent_1099SalesAgentHelper.ClickElement("ClickOnMergeBtn");
                agent_1099SalesAgentHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Confirmation Merge Successfull");
                agent_1099SalesAgentHelper.WaitForText("Merging Lead(s) Completed Successfully.", 10);

                executionLog.Log("SaleAgent1", "Redirect To leads page. ");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                //office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgent1", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("SaleAgent1", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("SaleAgent1", "Redirect To leads recycle bin page. ");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Click on delete leads");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("SaleAgent1", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Wait for success message.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            else
            {
                executionLog.Log("SaleAgent1", "Lead Saved Successfully");
                agent_1099SalesAgentHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("SaleAgent1", "Go to lead");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Select First Lead");
                agent_1099SalesAgentHelper.ClickElement("ClickOn1stOpp");

                executionLog.Log("SaleAgent1", "Select Second Lead");
                agent_1099SalesAgentHelper.ClickElement("ClickOn2ndOpp");

                executionLog.Log("SaleAgent1", "Click Merge Record");
                agent_1099SalesAgentHelper.ClickElement("ClickOnMergeRecords");
                agent_1099SalesAgentHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Select Lead Company Radio Button");
                agent_1099SalesAgentHelper.ClickElement("LeadCompyRadioBtn");
                agent_1099SalesAgentHelper.WaitForWorkAround(1000);

                executionLog.Log("SaleAgent1", "Click Merge Button Pop up");
                agent_1099SalesAgentHelper.ClickElement("ClickOnMergeBtn");
                agent_1099SalesAgentHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Confirmation Merge Successfull");
                agent_1099SalesAgentHelper.WaitForText("Merging Lead(s) Completed Successfully.", 10);

                executionLog.Log("SaleAgent1", "Redirect To leads page. ");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                //office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaleAgent1", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("SaleAgent1", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("SaleAgent1", "Redirect To leads recycle bin page. ");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaleAgent1", "Click on delete leads");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("SaleAgent1", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("SaleAgent1", "Wait for success message.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }

            executionLog.Log("SaleAgent1", "Goto Create Client");
            VisitOffice("clients/create");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Click on Save Button");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");

            executionLog.Log("SaleAgent1", "Enter DBA name.");
            agent_1099SalesAgentHelper.TypeText("ClientCompDBA", FName);

            //executionLog.Log("SaleAgent1", "Enter Client Bussiness Legal Name.");
            //office_ClientsHelper.TypeText("BussinessLegalName", "LegalCli");

            executionLog.Log("SaleAgent1", "Client Status");
            agent_1099SalesAgentHelper.SelectByText("ClientStatus", "New");

            executionLog.Log("SaleAgent1", "Select Client Responsibility");
            agent_1099SalesAgentHelper.SelectByText("SelectClientResponsibility", "Aslam Sales");

            executionLog.Log("SaleAgent1", "Click on Save Button");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");
            agent_1099SalesAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent1", "Confirmation for the save");
            agent_1099SalesAgentHelper.WaitForText("Client saved successfully.", 10);

            executionLog.Log("SaleAgent1", "Visit Craete Client");
            VisitOffice("clients/create");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Click on Cancel");
            agent_1099SalesAgentHelper.ClickElement("CancelOpp");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            //executionLog.Log("SaleAgent1", "Verify text on page.");
            //agent_1099SalesAgentHelper.VerifyText("VerifyTextPresentClient", "Clients");

            executionLog.Log("SaleAgent1", "Visit Craete Client");
            VisitOffice("clients/create");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Click on Save");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");

            executionLog.Log("SaleAgent1", "Client Company DBA Name");
            agent_1099SalesAgentHelper.TypeText("ClientCompDBA", FName);

            //executionLog.Log("SaleAgent1", "Enter Client Bussiness Legal Name.");
            //office_ClientsHelper.TypeText("BussinessLegalName", "LegalCli");

            executionLog.Log("SaleAgent1", "Select Status");
            agent_1099SalesAgentHelper.SelectByText("ClientStatus", "New");

            executionLog.Log("SaleAgent1", "Select Responsibilties");
            agent_1099SalesAgentHelper.SelectByText("SelectClientResponsibility", "Aslam Sales");

            executionLog.Log("SaleAgent1", "Click on Save");
            agent_1099SalesAgentHelper.ClickElement("ClickSaveBtn");
            agent_1099SalesAgentHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent1", "Wait for Confirmation");
            agent_1099SalesAgentHelper.WaitForText("Client saved successfully.", 10);

            executionLog.Log("SaleAgent1", "Visit Client");
            VisitOffice("clients");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Select first client.");
            agent_1099SalesAgentHelper.ClickElement("ClickOn1stOpp");

            executionLog.Log("SaleAgent1", "Select second client.");
            agent_1099SalesAgentHelper.ClickElement("ClickOn2ndOpp");

            executionLog.Log("SaleAgent1", "Click on merge records.");
            agent_1099SalesAgentHelper.ClickElement("ClickOnMergeRecords");
            agent_1099SalesAgentHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Radio select company to merge");
            agent_1099SalesAgentHelper.ClickElement("ClientCompToMrge");
            agent_1099SalesAgentHelper.WaitForWorkAround(1000);

            executionLog.Log("SaleAgent1", "Click On Merge");
            agent_1099SalesAgentHelper.ClickElement("ClickOnMergeBtn");
            agent_1099SalesAgentHelper.AcceptAlert();

            executionLog.Log("SaleAgent1", "Wait For Confirmation");
            agent_1099SalesAgentHelper.WaitForText("Merging Client(s) Completed Successfully.", 10);

            executionLog.Log("SaleAgent1", "Redirect To clients page. ");
            VisitOffice("clients");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", FName);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent1", "Select client by check box");
            office_ClientsHelper.ClickElement("ClickOn1stOpp");
            //office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent1", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteClient");

            executionLog.Log("SaleAgent1", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("SaleAgent1", "Redirect To client recycle bin page. ");
            VisitOffice("clients/recyclebin");
            office_LeadsHelper.WaitForWorkAround(3000);

            executionLog.Log("SaleAgent1", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", FName);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("SaleAgent1", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteRbin");

            executionLog.Log("SaleAgent1", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("SaleAgent1", "Wait for success message.");
            office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

        }
    catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaleAgent1");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("SaleAgent1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("SaleAgent1", "Bug", "Medium", "Client/Laead Page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("SaleAgent1");
                        TakeScreenshot("SaleAgent1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Iframe.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaleAgent1");
                        string id = loginHelper.getIssueID("SaleAgent1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaleAgent1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("SaleAgent1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("SaleAgent1");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaleAgent1");
                executionLog.WriteInExcel("SaleAgent1", Status, JIRA, "SaleAgent Portal");
            }
        }
    }
}
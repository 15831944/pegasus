using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientMerchantIssue2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientMerchantIssue2()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            var DBA = "Client" + RandomNumber(3333, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("ClientMerchantIssue2", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("ClientMerchantIssue2", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("ClientMerchantIssue2", "Go to create a client page");
            VisitOffice("clients/create");

            executionLog.Log("ClientMerchantIssue2", "Verify title");
            VerifyTitle("Create a Client");

            executionLog.Log("ClientMerchantIssue2", "Enter Client Dba Name");
            office_ClientsHelper.TypeText("ClientDBAName", DBA);

            executionLog.Log("ClientMerchantIssue2", "Select client status");
            office_ClientsHelper.SelectByText("ClientStatus", "New");

            executionLog.Log("ClientMerchantIssue2", "Select Client Res[onsibility.");
            office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

            executionLog.Log("ClientMerchantIssue2", "Click on next button");
            office_ClientsHelper.ClickElement("Next");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientMerchantIssue2", "Wait for confirmation message.");
            office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

            executionLog.Log("ClientMerchantIssue2", "Click on 'CustomerRelation' tab");
            office_ClientsHelper.ClickElement("CustomerRelationship");

            executionLog.Log("ClientMerchantIssue2", "Verify CustomerRelation tab is present");
            office_ClientsHelper.verifyElementPresent("CustomerRelationHighlighted");

            executionLog.Log("ClientMerchantIssue2", "Verify Terminals tab not present");
            office_ClientsHelper.verifyElementNotPresent("TerminalsHighlighted");

            executionLog.Log("ClientMerchantIssue2", "Click on Terminals tab");
            office_ClientsHelper.ClickElement("TerminalsandEquipments");

            executionLog.Log("ClientMerchantIssue2", "Verify Terminals tab is present");
            office_ClientsHelper.verifyElementPresent("TerminalsHighlighted");

            executionLog.Log("ClientMerchantIssue2", "Verify Products tab not present");
            office_ClientsHelper.verifyElementNotPresent("ProductsHighlighted");

            executionLog.Log("ClientMerchantIssue2", "Redirect at clients page.");
            VisitOffice("clients");

            executionLog.Log("ClientMerchantIssue2", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", DBA);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientMerchantIssue2", "Select client by check box");
            office_ClientsHelper.ClickElement("ClickOn1stOpp");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientMerchantIssue2", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteClient");

            executionLog.Log("ClientMerchantIssue2", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("ClientMerchantIssue2", "Wait for success message.");
            office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

            executionLog.Log("ClientMerchantIssue2", "Redirect To client recycle bin page. ");
            VisitOffice("clients/recyclebin");

            executionLog.Log("ClientMerchantIssue2", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", DBA);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("ClientMerchantIssue2", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteRbin");

            executionLog.Log("ClientMerchantIssue2", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("ClientMerchantIssue2", "Wait for success message.");
            office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            executionLog.Log("ClientMerchantIssue2", "Logout from the application");
            VisitOffice("logout");

        }
      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientMerchantIssue2");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Merchant Issue 2");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Merchant Issue 2", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Merchant Issue 2");
                        TakeScreenshot("ClientMerchantIssue2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMerchantIssue2.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientMerchantIssue2");
                        string id = loginHelper.getIssueID("Client Merchant Issue 2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMerchantIssue2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Merchant Issue 2"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Merchant Issue 2");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientMerchantIssue2");
                executionLog.WriteInExcel("Client Merchant Issue 2", Status, JIRA, "Client Management");
            }
        }
    }
}
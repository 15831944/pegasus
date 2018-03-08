using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientMerchantIssue1 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientMerchantIssue1()
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

            var DBA = "Clients" + RandomNumber(333, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("ClientMerchantIssue1", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientMerchantIssue1", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientMerchantIssue1", "Go to create a client page");
                VisitOffice("clients/create");

                executionLog.Log("ClientMerchantIssue1", "Verify title");
                VerifyTitle("Create a Client");

                executionLog.Log("ClientMerchantIssue1", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientMerchantIssue1", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientMerchantIssue1", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientMerchantIssue1", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMerchantIssue1", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("ClientMerchantIssue1", "Click on 'Merchant number' tab");
                office_ClientsHelper.ClickElement("MerchantNumber");

                executionLog.Log("ClientMerchantIssue1", "Verify Merchant number tab is present");
                office_ClientsHelper.verifyElementPresent("MerchantNumberHighlighted");
                office_ClientsHelper.WaitForWorkAround(6000);

                executionLog.Log("ClientMerchantIssue1", "Verify Customer relationship is not present on page.");
                office_ClientsHelper.verifyElementNotPresent("CustomerRelationHighlighted");
                office_ClientsHelper.WaitForWorkAround(6000);

                executionLog.Log("ClientMerchantIssue1", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("ClientMerchantIssue1", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(6000);

                executionLog.Log("ClientMerchantIssue1", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMerchantIssue1", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("ClientMerchantIssue1", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientMerchantIssue1", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("ClientMerchantIssue1", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("ClientMerchantIssue1", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientMerchantIssue1", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientMerchantIssue1", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientMerchantIssue1", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);


                executionLog.Log("ClientMerchantIssue1", "Logout from the application");
                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientMerchantIssue1");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Merchant Issue 1");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Merchant Issue 1", "Bug", "Medium", "Merchant page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Merchant Issue 1");
                        TakeScreenshot("ClientMerchantIssue1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMerchantIssue1.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientMerchantIssue1");
                        string id = loginHelper.getIssueID("Client Merchant Issue 1");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientMerchantIssue1.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Merchant Issue 1"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Merchant Issue 1");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientMerchantIssue1");
                executionLog.WriteInExcel("Client Merchant Issue 1", Status, JIRA, "Client Management");
            }
        }
    }
}

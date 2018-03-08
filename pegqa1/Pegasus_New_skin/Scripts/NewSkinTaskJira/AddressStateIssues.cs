using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AddressStateIssues : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void addressStateIssues()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());

            // VARIABLE
            var CDBA = "Client" + RandomNumber(3323, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AddressStateIssues", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AddressStateIssues", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddressStateIssues", "ClickOnCreateClient");
                VisitOffice("clients/create");

                executionLog.Log("AddressStateIssues", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", CDBA);

                executionLog.Log("AddressStateIssues", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("AddressStateIssues", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("AddressStateIssues", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AddressStateIssues", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("AddressStateIssues", "ClickOnContactTab");
                office_ClientsHelper.ClickElement("ClickOnContactTab");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("AddressStateIssues", "Select Mailing Country");
                office_ClientsHelper.Select("ContactCountry", "Canada");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("AddressStateIssues", "SelectComapnyState");
                office_ClientsHelper.Select("ContactState", "AB");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("AddressStateIssues", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("AddressStateIssues", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", CDBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AddressStateIssues", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AddressStateIssues", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("AddressStateIssues", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AddressStateIssues", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("AddressStateIssues", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");

                executionLog.Log("AddressStateIssues", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", CDBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("AddressStateIssues", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("AddressStateIssues", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("AddressStateIssues", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddressStateIssues");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Address State Issues");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Address State Issues", "Bug", "Medium", "Create Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Address State Issues");
                        TakeScreenshot("AddressStateIssues");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddressStateIssues.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddressStateIssues");
                        string id = loginHelper.getIssueID("Address State Issues");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddressStateIssues.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Address State Issues"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Address State Issues");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AddressStateIssues");
                executionLog.WriteInExcel("Address State Issues", Status, JIRA, "Leads Management");
            }
        }
    }
}
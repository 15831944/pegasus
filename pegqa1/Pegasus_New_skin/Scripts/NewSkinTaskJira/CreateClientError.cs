using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateClientError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createClientError()
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

            var DBA = "Client" + RandomNumber(333, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateClientError", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateClientError", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateClientError", "navigate to the Create client page.");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateClientError", "verify title");
                VerifyTitle("Create a Client");

                executionLog.Log("CreateClientError", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("CreateClientError", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("CreateClientError", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("CreateClientError", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateClientError", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("CreateClientError", "Verify error not displayed");
                office_ClientsHelper.VerifyTextNotPresent("Already Exist");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateClientError", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientError", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientError", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientError", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("CreateClientError", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("CreateClientError", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("CreateClientError", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateClientError", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateClientError", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("CreateClientError", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("CreateClientError", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateClientError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Client Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Client Error", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Client Error");
                        TakeScreenshot("CreateClientError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateClientError");
                        string id = loginHelper.getIssueID("Create Client Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateClientError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Client Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Client Error");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateClientError");
                executionLog.WriteInExcel("Create Client Error", Status, JIRA, "Client Management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ClientVerifyCanadaCountry : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void clientVerifyCanadaCountry()
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
            var DBA = "Client" + RandomNumber(111, 99999);
            var Title = "Tester" + RandomNumber(11, 100);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("ClientVerifyCanadaCountry", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("ClientVerifyCanadaCountry", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ClientVerifyCanadaCountry", "Redirect to Create Merchant page");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("ClientVerifyCanadaCountry", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("ClientVerifyCanadaCountry", "Select Client Responsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("ClientVerifyCanadaCountry", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("ClientVerifyCanadaCountry", "Goto Company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                office_ClientsHelper.TypeText("FederalTaxID", "123432232");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Select Mailing Country");
                office_ClientsHelper.SelectByText("MailingCountry", "Canada");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Select Location counrty");
                office_ClientsHelper.SelectByText("LocationCountry", "Canada");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Click On Contact Tab");
                office_ClientsHelper.ClickElement("ContactDetails");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Enter the Contact Title");
                office_ClientsHelper.TypeText("ContactTitle", Title);

                executionLog.Log("ClientVerifyCanadaCountry", "SelectAddressCountry");
                office_ClientsHelper.SelectByText("ContactAddressCountry", "Canada");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Clkck on Owner tab");
                office_ClientsHelper.ClickForce("OwnerTab");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Select Owner Address country");
                office_ClientsHelper.SelectByText("OwnerAddressCountry", "Canada");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("ClientVerifyCanadaCountry", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientVerifyCanadaCountry", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("ClientVerifyCanadaCountry", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ClientVerifyCanadaCountry", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ClientVerifyCanadaCountry", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("ClientVerifyCanadaCountry", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("ClientVerifyCanadaCountry", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ClientVerifyCanadaCountry");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client Verify Canada Country");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Verify Canada Country", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client Verify Canada Country");
                        TakeScreenshot("ClientVerifyCanadaCountry");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientVerifyCanadaCountry.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ClientVerifyCanadaCountry");
                        string id = loginHelper.getIssueID("Client Verify Canada Country");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientVerifyCanadaCountry.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client Verify Canada Country"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client Verify Canada Country");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ClientVerifyCanadaCountry");
                executionLog.WriteInExcel("Client Verify Canada Country", Status, JIRA, "Client Management");
            }
        }
    }
}
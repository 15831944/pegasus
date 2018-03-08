using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Client : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void client()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var DBA = "Dba" + RandomNumber(99, 999);
            var DBAQA = "QADBA" + RandomNumber(100, 999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("Client", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("Client", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("Client", "Goto Create Client");
                VisitOffice("clients/create");

                executionLog.Log("Client", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("Client", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("Client", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("Client", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("Client", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("Client", "Visit Craete Client");
                VisitOffice("clients/create");

                executionLog.Log("Client", "Click Cancel");
                office_ClientsHelper.ClickElement("CancelOpp");

                executionLog.Log("Client", "Enter Comp");
                office_ClientsHelper.VerifyText("VerifyTextPresentClient", "Merchants");

                executionLog.Log("Client", "Visit Craete Client");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("Client", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBAQA);

                executionLog.Log("Client", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("Client", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("Client", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("Client", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("Client", "Wait for element to be visible.");
                office_ClientsHelper.WaitForElementPresent("CompanyDetails", 10);

                executionLog.Log("Client", "Goto Company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("Client", "Visit Client");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("Client", "Check mark the first");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("Client", "CheckMark Second");
                office_ClientsHelper.ClickElement("ClickOn2ndOpp");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("Client", "Click Merege");
                office_ClientsHelper.ClickElement("ClickOnMergeRecords");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("Client", "Radio select company to merge");
                office_ClientsHelper.ClickElement("ClientCompToMrge");
                office_ClientsHelper.WaitForWorkAround(1000);

                executionLog.Log("Client", "Click On Merge");
                office_ClientsHelper.ClickElement("ClickOnMergeBtn");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("Client", "Wait For Confirmation");
                office_ClientsHelper.WaitForText("Merging Client(s) Completed Successfully.", 10);

                executionLog.Log("Client", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("Client", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("Client", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("Client", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("Client", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("Client", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("Client", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("Client", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBAQA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("Client", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("Client", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("Client", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Client");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client", "Bug", "Medium", "Client", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Client");
                        TakeScreenshot("Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Client.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Client");
                        string id = loginHelper.getIssueID("Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Client.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Client");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Client");
                executionLog.WriteInExcel("Client", Status, JIRA, "Client Management");
            }
        }
    }
}
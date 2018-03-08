using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyClientCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyClientCreatedAndModifiedByCredits()
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
            var DBA = "Dba" + RandomNumber(99, 99999);

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Goto Create Client");
                VisitOffice("clients/create");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify created by credits");
                office_ClientsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify modified by credits.");
                office_ClientsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on company details tab.");
                office_ClientsHelper.ClickElement("CompanyDetails");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on save button.");
                office_ClientsHelper.ClickElement("OwnerSave");

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on info tab.");
                office_ClientsHelper.ClickElement("Info");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify created by credits");
                office_ClientsHelper.VerifyText("CreatedBy", "Howard Tang");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify modified by credits.");
                office_ClientsHelper.VerifyText("ModifiedBy", "Howard Tang");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Verify page title as clients");
                VerifyTitle();
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Select All responsbility");
                office_ClientsHelper.SelectByText("Responsibilityfield", "All");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Select All responsbility");
                office_ClientsHelper.SelectByText("ClientResponsibityRecycle", "All");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientCreatedAndModifiedByCredits", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyClientCreatedAndModifiedByCredits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyClientCreatedAndModifiedByCredits", "Bug", "Medium", "Clients page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyClientCreatedAndModifiedByCredits");
                        TakeScreenshot("VerifyClientCreatedAndModifiedByCredits");
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
                        TakeScreenshot("VerifyClientCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("VerifyClientCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Client.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyClientCreatedAndModifiedByCredits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyClientCreatedAndModifiedByCredits");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyClientCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("VerifyClientCreatedAndModifiedByCredits", Status, JIRA, "Client Management");
            }
        }
    }
}

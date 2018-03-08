using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyClientMerchantNumberIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyClientMerchantNumberIssue()
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
                executionLog.Log("VerifyClientMerchantNumberIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyClientMerchantNumberIssue", "Create Client");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify page title");
                VerifyTitleMerchantClient();

                executionLog.Log("VerifyClientMerchantNumberIssue", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyClientMerchantNumberIssue", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify label for merchant number text box.");
                office_ClientsHelper.IsElementPresent("LabelMerchantNumber");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify presence of merchant number text box.");
                office_ClientsHelper.IsElementVisible("//*[@id='ClientDetailMerchID']");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                //executionLog.Log("VerifyClientMerchantNumberIssue", "Wait for element to be visible.");
                //office_ClientsHelper.WaitForElementPresent("MerchantNumber", 10);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Goto Company details tab.");
                office_ClientsHelper.ClickElement("MerchantNumber");

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify label for merchant number text box.");
                office_ClientsHelper.IsElementPresent("LabelMerchantNumber");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Verify presence of merchant number text box.");
                office_ClientsHelper.IsElementVisible("//*[@id='ClientDetailMerchID']");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("VerifyClientMerchantNumberIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyClientMerchantNumberIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);
                office_ClientsHelper.selectOwner("//*[@id='gs_first_name']");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyClientMerchantNumberIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyClientMerchantNumberIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyClientMerchantNumberIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyClientMerchantNumberIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Client Merchant Number Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Client Merchant Number Issue", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Client Merchant Number Issue");
                        TakeScreenshot("VerifyClientMerchantNumberIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientMerchantNumberIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyClientMerchantNumberIssue");
                        string id = loginHelper.getIssueID("Verify Client Merchant Number Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyClientMerchantNumberIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Client Merchant Number Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Client Merchant Number Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyClientMerchantNumberIssue");
                executionLog.WriteInExcel("Verify Client Merchant Number Issue", Status, JIRA, "Client Management");
            }
        }
    }
}
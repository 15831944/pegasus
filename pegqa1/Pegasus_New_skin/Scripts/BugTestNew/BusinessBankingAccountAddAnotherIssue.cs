using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class BusinessBankingAccountAddAnotherIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void businessBankingAccountAddAnotherIssue()
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
                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", " Reditect at Create Client page.");
                VisitOffice("clients/create");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify page title");
                VerifyTitleMerchantClient();

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify label for merchant number text box.");
                office_ClientsHelper.IsElementPresent("LabelMerchantNumber");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify presence of merchant number text box.");
                office_ClientsHelper.IsElementVisible("//*[@id='ClientDetailMerchID']");
                //office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Click on business details tab.");
                office_ClientsHelper.ClickElement("BusinessDetailsTab");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify added contact name for bussiness banking not present on the page.");
                office_ClientsHelper.IsElementNotPresent("AddedContNAme");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify added contact phone for bussiness banking not present on the page.");
                office_ClientsHelper.IsElementNotPresent("AddedContPhone");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify added bank name for bussiness banking not present on the page.");
                office_ClientsHelper.IsElementNotPresent("AddedBankName");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify added routing no.  for bussiness banking not present on the page.");
                office_ClientsHelper.IsElementNotPresent("AddedRoutingNo");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verify added account number for bussiness banking not present on the page.");
                office_ClientsHelper.IsElementNotPresent("AddedAccounNo");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Wait for element to be present.");
                office_ClientsHelper.WaitForElementPresent("AddAnother", 3);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Click on Add another button to add second bankin details.");

                office_ClientsHelper.scrollToElement("AddAnother");
                office_ClientsHelper.ClickElement("AddAnother");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verifies added banking details cont. name present on the page.");
                office_ClientsHelper.VerifyElementDisplayed("AddedContNAme");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verifies added banking details cont. phone present on the page.");
                office_ClientsHelper.VerifyElementDisplayed("AddedContPhone");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verifies added banking details bank name present on the page.");
                office_ClientsHelper.VerifyElementDisplayed("AddedBankName");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verifies added banking details routing number present on the page.");
                office_ClientsHelper.VerifyElementDisplayed("AddedRoutingNo.");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Verifies added banking details Account number present on the page.");
                office_ClientsHelper.VerifyElementDisplayed("AddedAccounNo");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Redirect at clients page.");
                VisitOffice("clients");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                //office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(2000);
                office_ClientsHelper.selectOwner("//*[@id='gs_first_name']");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("BusinessBankingAccountAddAnotherIssue", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("BusinessBankingAccountAddAnotherIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Business Banking Account Add Another Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Business Banking Account Add Another Issue", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Business Banking Account Add Another Issue");
                        TakeScreenshot("BusinessBankingAccountAddAnotherIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BusinessBankingAccountAddAnotherIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("BusinessBankingAccountAddAnotherIssue");
                        string id = loginHelper.getIssueID("Business Banking Account Add Another Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\BusinessBankingAccountAddAnotherIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Business Banking Account Add Another Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Business Banking Account Add Another Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("BusinessBankingAccountAddAnotherIssue");
                executionLog.WriteInExcel("Business Banking Account Add Another Issue", Status, JIRA, "Client Management");
            }
        }
    }
}
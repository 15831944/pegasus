using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyTypoInClientRateAndFess : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTest")]
        public void verifyTypoInClientRateAndFess()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            var DBA = "Client" + RandomNumber(1111, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTypoInClientRateAndFess", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Redirect to Client page.");
                VisitOffice("clients/create");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Verify page title.");
                VerifyTitleMerchantClient();

                executionLog.Log("VerifyTypoInClientRateAndFess", "Enter Client Dba Name");
                office_ClientsHelper.TypeText("ClientDBAName", DBA);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Select client status");
                office_ClientsHelper.SelectByText("ClientStatus", "New");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Select Client Res[onsibility.");
                office_ClientsHelper.SelectByText("ClientResponsibility", "Howard Tang");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Click on next button");
                office_ClientsHelper.ClickElement("Next");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Wait for confirmation message.");
                office_ClientsHelper.WaitForText("Client saved successfully. ", 10);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Click On Client Rate And Fees");
                office_ClientsHelper.ClickElement("RatesAndFee");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Scroll to element.");
                office_ClientsHelper.scrollToElement("VerifyTextInstruction");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Verify text on page.");
                office_ClientsHelper.VerifyText("VerifyTextInstruction", "Special Instructions");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyTypoInClientRateAndFess", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Redirect To client recycle bin page. ");
                VisitOffice("clients/recyclebin");
                office_ClientsHelper.WaitForWorkAround(5000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", DBA);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyTypoInClientRateAndFess", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyTypoInClientRateAndFess", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyTypoInClientRateAndFess", "Wait for success message.");
                office_ClientsHelper.WaitForText("Client Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTypoInClientRateAndFess");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Typo In Client RateAndFess");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Typo In Client RateAndFess", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Typo In Client RateAndFess");
                        TakeScreenshot("VerifyTypoInClientRateAndFess");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTypoInClientRateAndFess.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTypoInClientRateAndFess");
                        string id = loginHelper.getIssueID("Verify Typo In Client RateAndFess");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTypoInClientRateAndFess.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Typo In Client RateAndFess"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Typo In Client RateAndFess");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTypoInClientRateAndFess");
                executionLog.WriteInExcel("Verify Typo In Client RateAndFess", Status, JIRA, "Client Management");
            }
        }
    }
}
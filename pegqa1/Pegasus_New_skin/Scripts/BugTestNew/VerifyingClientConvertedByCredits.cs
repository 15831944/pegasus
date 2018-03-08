using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyingClientConvertedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingClientConvertedByCredits()
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
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            var Company = "My Company" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyingClientConvertedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyingClientConvertedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyingClientConvertedByCredits", "Redirect To create lead page");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Enter Company Nmae");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("VerifyingClientConvertedByCredits", "Enter First Name ");
                office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

                executionLog.Log("VerifyingClientConvertedByCredits", "EnterLastName");
                office_LeadsHelper.TypeText("LastName", "Tester");

                executionLog.Log("VerifyingClientConvertedByCredits", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("VerifyingClientConvertedByCredits", "LeadResponsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyingClientConvertedByCredits", "Click on Save");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(3000);

                var LocDub = "//button[text()='Create Duplicate']";
                if (office_LeadsHelper.IsElementPresent(LocDub))
                {
                    office_LeadsHelper.Click(LocDub);
                }

                executionLog.Log("VerifyingClientConvertedByCredits", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickConvert");

                executionLog.Log("VerifyingClientConvertedByCredits", "Yes Move To Recycle Bin");
                office_LeadsHelper.ClickElement("ClickYes");

                executionLog.Log("VerifyingClientConvertedByCredits", "Click Convert Save Lead");
                office_LeadsHelper.ClickElement("ConvertSaveLead");
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Verify  messge");
                office_LeadsHelper.VerifyPageText("Lead is converted and moved to recyclebin.");
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Verify  messge");
                office_ClientsHelper.VerifyText("ConvertedBy", "Howard Tang");
                //office_ClientsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Redirect To clients page. ");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Enter Company Name");
                office_ClientsHelper.TypeText("SearchClient", Company);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Select client by check box");
                office_ClientsHelper.ClickElement("ClickOn1stOpp");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Click on delete client");
                office_ClientsHelper.ClickElement("DeleteClient");

                executionLog.Log("VerifyingClientConvertedByCredits", "Accept alert message.");
                office_ClientsHelper.AcceptAlert();

                executionLog.Log("VerifyingClientConvertedByCredits", "Wait for success message.");
                office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("VerifyingClientConvertedByCredits", "Redirect To leads recycle bin page. ");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Enter Company Name");
                office_LeadsHelper.TypeText("SearchLeadRbin", Company);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Select 'All' in responsibilty field");
                office_LeadsHelper.SelectByText("SelectResponsibiltiy", "All");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingClientConvertedByCredits", "Click on delete leads");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("VerifyingClientConvertedByCredits", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("VerifyingClientConvertedByCredits", "Wait for success message.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingClientConvertedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Client Converted By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Client Converted By Credits", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Client Converted By Credits");
                        TakeScreenshot("VerifyingClientConvertedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingClientConvertedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingClientConvertedByCredits");
                        string id = loginHelper.getIssueID("Verifying Client Converted By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingClientConvertedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Client Converted By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Client Converted By Credits");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingClientConvertedByCredits");
                executionLog.WriteInExcel("Verifying Client Converted By Credits", Status, JIRA, "Leads Management");
            }
        }
    }
}

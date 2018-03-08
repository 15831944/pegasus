using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyCreatedModifiedByForConvertedClient : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        public void verifyCreatedModifiedByForConvertedClient()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
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

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Login with valid username and password");
            Login(username[0], password[0]);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Redirect To create lead page");
            VisitOffice("leads/create");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on Assignments");
            office_LeadsHelper.ClickElement("Assignments");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Wait for element to be visible.");
            office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Select Lead Status");
            office_LeadsHelper.Select("LeadStatus", "New");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "LeadResponsibility");
            office_LeadsHelper.SelectByText("LeadResponsibility", "Howard Tang");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on companu details tab");
            office_LeadsHelper.ClickElement("CompanyDetails");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Enter First Name ");
            office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "EnterLastName");
            office_LeadsHelper.TypeText("LastName", "Tester");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Enter Company Nmae");
            office_LeadsHelper.TypeText("CompanyName", Company);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on Save");
            office_LeadsHelper.ClickElement("Save");
            office_LeadsHelper.WaitForWorkAround(7000);


            var LocDub = "//button[text()='Create Duplicate']";
            if (office_LeadsHelper.IsElementPresent(LocDub))
            {
                office_LeadsHelper.Click(LocDub);
            }

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on Convert");
            office_LeadsHelper.ClickElement("ClickConvert");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Yes Move To Recycle Bin");
            office_LeadsHelper.ClickElement("ClickYes");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click Convert Save Lead");
            office_LeadsHelper.ClickElement("ConvertSaveLead");
            office_LeadsHelper.WaitForWorkAround(4000);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Verify  messge");
            office_LeadsHelper.VerifyPageText("Lead is converted and moved to recyclebin.");
            office_LeadsHelper.WaitForWorkAround(4000);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Wait for locator to be present.");
            office_ClientsHelper.WaitForElementPresent("CreatedBy", 10);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Verify client created by name.");
            office_ClientsHelper.VerifyText("CreatedBy", "Howard Tang");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Verify  client modified b y name.");
            office_ClientsHelper.VerifyText("ModifiedBy", "Howard Tang");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Redirect To clients page. ");
            VisitOffice("clients");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Enter Company Name");
            office_ClientsHelper.TypeText("SearchClient", Company);
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Wait for locator to present.");
            office_ClientsHelper.WaitForElementPresent("ClickOn1stOpp", 10);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Select client by check box");
            office_ClientsHelper.ClickElement("ClickOn1stOpp");
            office_ClientsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on delete client");
            office_ClientsHelper.ClickElement("DeleteClient");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Accept alert message.");
            office_ClientsHelper.AcceptAlert();

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Wait for success message.");
            office_ClientsHelper.WaitForText("1 records deleted successfully", 10);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Redirect To leads recycle bin page. ");
            VisitOffice("leads/recyclebin");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Enter Company Name");
            office_LeadsHelper.TypeText("SearchLeadRbin", Company);
            office_LeadsHelper.WaitForWorkAround(2000);

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Click on delete leads");
            office_LeadsHelper.ClickElement("DeleteRbin");

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Accept alert message.");
            office_LeadsHelper.AcceptAlert();

            executionLog.Log("VerifyCreatedModifiedByForConvertedClient", "Wait for success message.");
            office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCreatedModifiedByForConvertedClient");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Created Modified By For Converted Client");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Created Modified By For Converted Client", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Created Modified By For Converted Client");
                        TakeScreenshot("VerifyCreatedModifiedByForConvertedClient");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedModifiedByForConvertedClient.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCreatedModifiedByForConvertedClient");
                        string id = loginHelper.getIssueID("Verify Created Modified By For Converted Client");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCreatedModifiedByForConvertedClient.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Created Modified By For Converted Client"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Created Modified By For Converted Client");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCreatedModifiedByForConvertedClient");
                executionLog.WriteInExcel("Verify Created Modified By For Converted Client", Status, JIRA, "Leads Management");
            }
        }
    }
}
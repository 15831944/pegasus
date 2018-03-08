using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadPhoneUpdate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void leadPhoneUpdate()
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

            // VARIABLE
            var Company = "My Company" + RandomNumber(1, 9999);
            var name = "TestEmployee" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadPhoneUpdate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadPhoneUpdate", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LeadPhoneUpdate", "Redirect To create lead page. ");
                VisitOffice("leads/create");

                executionLog.Log("LeadPhoneUpdate", "Verify page title. ");
                VerifyTitle("Create a Lead");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("LeadPhoneUpdate", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadPhoneUpdate", "Select Lead Status");
                office_LeadsHelper.Select("LeadStatus", "New");

                executionLog.Log("LeadPhoneUpdate", "Select Lead Responsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadPhoneUpdate", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadButton");
                office_LeadsHelper.WaitForWorkAround(1000);

                executionLog.Log("LeadPhoneUpdate", "Enter First Name ");
                office_LeadsHelper.TypeText("FirstNameLead", "Test Lead");

                executionLog.Log("LeadPhoneUpdate", "Enter Last Name");
                office_LeadsHelper.TypeText("LeadLastName", "Tester");

                executionLog.Log("LeadPhoneUpdate", "Enter Company Name");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("LeadPhoneUpdate", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadButton");
                office_LeadsHelper.WaitForWorkAround(4000);

                var LocDub = "//button[text()='Create Duplicate']";
                if (office_LeadsHelper.IsElementPresent(LocDub))
                {

                    office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadPhoneUpdate", "Click on duplicate btn");
                    office_LeadsHelper.Click(LocDub);
                    office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadPhoneUpdate", "Verify text.");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("LeadPhoneUpdate", "Redirect To lead page. ");
                    VisitOffice("leads");

                    executionLog.Log("LeadPhoneUpdate", "Enter Company Name");
                    office_LeadsHelper.TypeText("CompanySearch", Company);

                    executionLog.Log("LeadPhoneUpdate", "Wait for checkbox to appear.");
                    office_LeadsHelper.WaitForElementPresent("CheckDocToDel", 10);

                    executionLog.Log("LeadPhoneUpdate", "Select lead by check box");
                    office_LeadsHelper.ClickElement("CheckDocToDel");
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("LeadPhoneUpdate", "Click on delete lead");
                    office_LeadsHelper.ClickElement("DeleteLead");

                    executionLog.Log("LeadPhoneUpdate", "Accept alert message.");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("LeadPhoneUpdate", "Wait for success message.");
                    office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadPhoneUpdate");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Phone Update");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Phone Update", "Bug", "Medium", "PDF Tab page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Phone Update");
                        TakeScreenshot("LeadPhoneUpdate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadPhoneUpdate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadPhoneUpdate");
                        string id = loginHelper.getIssueID("Lead Phone Update");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadPhoneUpdate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Phone Update"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Phone Update");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadPhoneUpdate");
                executionLog.WriteInExcel("Lead Phone Update", Status, JIRA, "Leads Management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadModifiedByIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void leadModifiedByIssue()
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

            // Variable
            var FirstName = "Test" + RandomNumber(1, 99);
            var LastName = "Tester" + RandomNumber(1, 99);
            var Number = "12345678" + RandomNumber(10, 99);
            var Company = "TEST COMPANY" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadModifiedByIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("LeadModifiedByIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LeadModifiedByIssue", "Redirect at create leads page.");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("LeadModifiedByIssue", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadType", 10);

                executionLog.Log("LeadModifiedByIssue", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("LeadModifiedByIssue", "Select Responsibity");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadModifiedByIssue", "Select Salutation");
                office_LeadsHelper.Select("Salutaion", "Mr");

                executionLog.Log("LeadModifiedByIssue", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FirstName);

                executionLog.Log("LeadModifiedByIssue", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LastName);

                executionLog.Log("LeadModifiedByIssue", "Enter Company Name ");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("LeadModifiedByIssue", "Click on Save button");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(7000);

                executionLog.Log("LeadModifiedByIssue", "Verify success message");
                office_LeadsHelper.WaitForText("Lead saved successfully. .", 10);

                executionLog.Log("LeadModifiedByIssue", "Verify page title details");
                VerifyTitle("Details");

                executionLog.Log("LeadModifiedByIssue", "Verify modified by credentials.");
                office_LeadsHelper.VerifyText("ModifiedBy", "By Howard Tang");

                executionLog.Log("LeadModifiedByIssue", "Go to Lead");
                VisitOffice("leads");

                executionLog.Log("LeadModifiedByIssue", "Click on First Lead To check");
                office_LeadsHelper.ClickElement("CheckDocToDel");

                executionLog.Log("LeadModifiedByIssue", "Click on Delete button.");
                office_LeadsHelper.ClickElement("ClickDelLeadbutton");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadModifiedByIssue", "Verify Confirmation");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadModifiedByIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Lead Modified By Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Lead Modified By Issue", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Lead Modified By Issue");
                        TakeScreenshot("LeadModifiedByIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadModifiedByIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadModifiedByIssue");
                        string id = loginHelper.getIssueID("Lead Modified By Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadModifiedByIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Lead Modified By Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Lead Modified By Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadModifiedByIssue");
                executionLog.WriteInExcel("Lead Modified By Issue", Status, JIRA, "Leads Management");
            }
        }
    }
}
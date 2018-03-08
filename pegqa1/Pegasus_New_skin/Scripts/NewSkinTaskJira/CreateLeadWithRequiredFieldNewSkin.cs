using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateLeadWithRequiredFieldNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createLeadWithRequiredFieldNewSkin()
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

            var FirstName = "Test" + RandomNumber(1111, 99999);
            var Company = "Lead COmp" + RandomNumber(221212, 999999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Click On Create");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Select Salutation");
                office_LeadsHelper.Select("Salutaion", "Mr");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FirstName);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", "Last");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Enter Company Name ");
                office_LeadsHelper.TypeText("CompanyName", Company);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Select Responsibity");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Click on save button");
                office_LeadsHelper.ClickElement("Save");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Verify Save button working");
                VerifyTitle("Details");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Redirect to leads page.");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Enter Company Name in search field ");
                office_LeadsHelper.TypeText("CompanySearch", Company);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Click on first checkbox");
                office_LeadsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Click delete Button");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("CreateLeadWithRequiredFieldNewSkin", "Verify message");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateLeadWithRequiredFieldNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Lead With Required Field NewSkin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Lead With Required Field NewSkin", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Lead With Required Field NewSkin");
                        TakeScreenshot("CreateLeadWithRequiredFieldNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadWithRequiredFieldNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateLeadWithRequiredFieldNewSkin");
                        string id = loginHelper.getIssueID("Create Lead With Required Field NewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateLeadWithRequiredFieldNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Lead With Required Field NewSkin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Lead With Required Field NewSkin");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateLeadWithRequiredFieldNewSkin");
                executionLog.WriteInExcel("Create Lead With Required Field NewSkin", Status, JIRA, "Leads Management");
            }
        }
    }
}

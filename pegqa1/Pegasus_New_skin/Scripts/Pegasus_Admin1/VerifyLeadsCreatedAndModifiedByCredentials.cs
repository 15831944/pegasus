using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyLeadsCreatedAndModifiedByCredentials : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyLeadsCreatedAndModifiedByCredentials()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Visit  Lead");
                VisitOffice("leads/create");

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Enter Company DBA");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on Assignments");
                office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Select Responsibities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");

                var LocDub = "//button[text()='Create Duplicate']";
                if (office_LeadsHelper.IsElementPresent(LocDub))
                {

                    office_LeadsHelper.WaitForWorkAround(4000);

                    executionLog.Log("LeadPhoneUpdate", "Click on duplicate btn");
                    office_LeadsHelper.Click(LocDub);
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead created by credits");
                    office_LeadsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead modified by credits");
                    office_LeadsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on company details tab.");
                    office_LeadsHelper.ClickElement("CompanyDetails");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on save button.");
                    office_LeadsHelper.ClickElement("SaveLead");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on info tab.");
                    office_LeadsHelper.ClickElement("InfoTab");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead created by credits");
                    office_LeadsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead modified by credits");
                    office_LeadsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Redirect at leads page.");
                    VisitOffice("leads");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify page titles.");
                    VerifyTitle("Leads");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Select first lead");
                    office_LeadsHelper.ClickElement("CheckDocToDel");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on delete button.");
                    office_LeadsHelper.ClickElement("DeleteLead");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for confirmation message.");
                    office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Redirect at leads recycle bin page.");
                    VisitOffice("leads/recyclebin");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify page title.");
                    VerifyTitle("Recycled Leads");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on delete icon");
                    office_LeadsHelper.ClickElement("DeleteLeadPer");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for confirmation.");
                    office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

                }
                else
                {

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead created by credits");
                    office_LeadsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead modified by credits");
                    office_LeadsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on company details tab.");
                    office_LeadsHelper.ClickElement("CompanyDetails");
                    office_LeadsHelper.WaitForWorkAround(5000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on save button.");
                    office_LeadsHelper.ClickElement("SaveLeadButton");
                    office_LeadsHelper.WaitForWorkAround(5000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on info tab.");
                    office_LeadsHelper.ClickElement("InfoTab");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead created by credits");
                    office_LeadsHelper.VerifyText("CreatedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify Lead modified by credits");
                    office_LeadsHelper.VerifyText("ModifiedBy", "Howard Tang");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Redirect at leads page.");
                    VisitOffice("leads");
                    office_LeadsHelper.WaitForWorkAround(5000);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify page titles.");
                    VerifyTitle("Leads");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Select first lead");
                    office_LeadsHelper.ClickElement("CheckDocToDel");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on delete button.");
                    office_LeadsHelper.ClickElement("DeleteLead");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for confirmation message.");
                    office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Redirect at leads recycle bin page.");
                    VisitOffice("leads/recyclebin");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Verify page title.");
                    VerifyTitle("Recycled Leads");

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Click on delete icon");
                    office_LeadsHelper.ClickElement("DeleteLeadPer");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyLeadsCreatedAndModifiedByCredentials", "Wait for confirmation.");
                    office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLeadsCreatedAndModifiedByCredentials");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyLeadsCreatedAndModifiedByCredentials");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyLeadsCreatedAndModifiedByCredentials", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyLeadsCreatedAndModifiedByCredentials");
                        TakeScreenshot("VerifyLeadsCreatedAndModifiedByCredentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadsCreatedAndModifiedByCredentials.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLeadsCreatedAndModifiedByCredentials");
                        string id = loginHelper.getIssueID("VerifyLeadsCreatedAndModifiedByCredentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLeadsCreatedAndModifiedByCredentials.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyLeadsCreatedAndModifiedByCredentials"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyLeadsCreatedAndModifiedByCredentials");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLeadsCreatedAndModifiedByCredentials");
                executionLog.WriteInExcel("VerifyLeadsCreatedAndModifiedByCredentials", Status, JIRA, "Lead Management");
            }
        }
    }
}
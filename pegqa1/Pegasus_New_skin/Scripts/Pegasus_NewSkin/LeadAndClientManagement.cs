using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class LeadAndClientManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void leadAndClientManagement()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
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
                executionLog.Log("LeadAndClientManagement", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("LeadAndClientManagement", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("LeadAndClientManagement", "Visit  Lead");
                VisitOffice("leads/create");

                executionLog.Log("LeadAndClientManagement", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("LeadAndClientManagement", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("LeadAndClientManagement", "Enter Company DBA");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("LeadAndClientManagement", "Click on Assignments");
                office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("LeadAndClientManagement", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadAndClientManagement", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("LeadAndClientManagement", "Select Responsibities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadAndClientManagement", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");

                executionLog.Log("LeadAndClientManagement", "Wait for Confirmation");
                office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("LeadAndClientManagement", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickOnConvert");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click Save on Pop Up");
                office_LeadsHelper.ClickOnDisplayed("ClickonPopUpSave");

                executionLog.Log("LeadAndClientManagement", "Verify confirmation");
                office_LeadsHelper.WaitForText("Lead is converted and moved to recyclebin.", 10);

                executionLog.Log("LeadAndClientManagement", "Visit Lead");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("LeadAndClientManagement", "Click Export");
                office_LeadsHelper.ClickElement("ClickOnExport");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Click Export As CSV");
                office_LeadsHelper.ClickElement("ExportAsCSVlEAD");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Goto Lead");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Export");
                office_LeadsHelper.ClickElement("ClickOnExport");

                executionLog.Log("LeadAndClientManagement", "Click on Export as excel");
                office_LeadsHelper.ClickElement("ExportAsExcelLead");
                office_LeadsHelper.WaitForWorkAround(5000);
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadAndClientManagement", "Redirect To leads page. ");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Select the responsibility");
                office_LeadsHelper.SelectByText("SelectResponsibiltiy", "Howard Tang");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Click on delete lead");
                office_LeadsHelper.ClickJS("DeleteLead");

                executionLog.Log("LeadAndClientManagement", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadAndClientManagement", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("LeadAndClientManagement", "Goto Client");
                VisitOffice("clients");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Export");
                office_ClientsHelper.ClickForce("ClickOnExport");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Click export AS Excel");
                office_ClientsHelper.ClickElement("ExportAsExcelLead");

                executionLog.Log("LeadAndClientManagement", "Go to create a Lead");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("LeadAndClientManagement", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("LeadAndClientManagement", "Enter Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("LeadAndClientManagement", "Click on Assignments");
                office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("LeadAndClientManagement", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadAndClientManagement", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Responsibilities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click Save Button");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");

                executionLog.Log("LeadAndClientManagement", "Wait for Confirmation");
                office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("LeadAndClientManagement", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickConvert");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Convert");
                office_LeadsHelper.ClickJS("CancelConvert");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Convert");
                office_LeadsHelper.ClickElement("ClickConvert");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on No");
                office_LeadsHelper.ClickDisplayed("//*[@id='LeadConversionDelete0']");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Save");
                office_LeadsHelper.ClickOnDisplayed("ClickonPopUpSave");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Confirmation");
                office_LeadsHelper.VerifyPageText("Lead is converted successfully.");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Go to create a Lead");
                VisitOffice("leads/create");

                executionLog.Log("LeadAndClientManagement", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("LeadAndClientManagement", "Enter Last Name");
                office_LeadsHelper.TypeText("LastName", LName);

                executionLog.Log("LeadAndClientManagement", "Enter Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("LeadAndClientManagement", "Click on Assignments");
                office_LeadsHelper.ClickElement("Assignments");

                executionLog.Log("LeadAndClientManagement", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("LeadAndClientManagement", "Select Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("LeadAndClientManagement", "Select Responsibility");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("LeadAndClientManagement", "Click onSave Button");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Click on Create duplicate lead");
                office_LeadsHelper.ClickElement("CreateDuplicate");

                executionLog.Log("LeadAndClientManagement", "Verify Confirmation");
                office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                executionLog.Log("LeadAndClientManagement", "Go to Lead");
                VisitOffice("leads");

                executionLog.Log("LeadAndClientManagement", "Click on First Lead To check");
                office_LeadsHelper.ClickElement("CheckDocToDel");

                executionLog.Log("LeadAndClientManagement", "Click on Delete button.");
                office_LeadsHelper.ClickElement("ClickDelLeadbutton");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadAndClientManagement", "Verify Confirmation");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("LeadAndClientManagement", "Goto leads/recyclebin ");
                VisitOffice("leads/recyclebin");

                executionLog.Log("LeadAndClientManagement", "Click Restore");
                office_LeadsHelper.ClickElement("ClickOnRestoreLeadIcon");

                executionLog.Log("LeadAndClientManagement", "Verify Confirmation");
                office_LeadsHelper.WaitForText("Lead Restored Successfully.", 10);

                executionLog.Log("LeadAndClientManagement", "Redirect To leads page. ");
                VisitOffice("leads");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("LeadAndClientManagement", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("LeadAndClientManagement", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadAndClientManagement", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("LeadAndClientManagement", "Redirect To leads page. ");
                VisitOffice("leads");

                executionLog.Log("LeadAndClientManagement", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("LeadAndClientManagement", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("LeadAndClientManagement", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("LeadAndClientManagement", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadAndClientManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("LeadAndClientManagement");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("LeadAndClientManagement", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("LeadAndClientManagement");
                        TakeScreenshot("LeadAndClientManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadAndClientManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadAndClientManagement");
                        string id = loginHelper.getIssueID("LeadAndClientManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadAndClientManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("LeadAndClientManagement"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("LeadAndClientManagement");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadAndClientManagement");
                executionLog.WriteInExcel("LeadAndClientManagement", Status, JIRA, "Lead Management");
            }
        }
    }
}
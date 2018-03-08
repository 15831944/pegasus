using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForLeads : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForLeads()
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
            var name = "Lead" + RandomNumber(99, 99999);
            var CDBA = "DBA" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyQuickLookLabelsForLeads", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Redirect at Create Lead");
                VisitOffice("leads/create");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", "LeadFirst");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Enter Last Name");
                office_LeadsHelper.TypeText("LeadLastName", "LeadLast");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Enter Lead Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Responsibilities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(7000);


                var loc = "//h3[text()='Existing Leads']";
                if (office_LeadsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("We are in first If cond as lead is duplicate !!");
                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on Duplicate");
                    office_LeadsHelper.ClickJS("CraeteLeadDub");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);
                }
                else
                {
                    Console.WriteLine("We are in first else cond as lead is not duplicate !!");
                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for Leads type.");
                    office_LeadsHelper.VerifyText("ClientType", "Click to edit");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for status.");
                    office_LeadsHelper.VerifyText("Status", "New");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for source.");
                    office_LeadsHelper.VerifyText("Source", "Select");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for category");
                    office_LeadsHelper.VerifyText("Category", "Click to edit");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for responsibility");
                    office_LeadsHelper.VerifyText("Responsibilityl", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for account manager.");
                    office_LeadsHelper.VerifyText("AccountManager", "Select");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for partner agent.");
                    office_LeadsHelper.VerifyText("PartnerAgentl", "Select");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Save Button");
                    office_LeadsHelper.VerifyText("PartnerAssociationl", "Select");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Save Button");
                    office_LeadsHelper.VerifyText("SalesManager", "Select");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on company details tab.");
                    office_LeadsHelper.ClickElement("CompanyDetails");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on assignments.");
                    office_LeadsHelper.ClickElement("EditAssignment");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for locator to be present.");
                    office_LeadsHelper.WaitForElementPresent("Clientt", 10);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Leads type.");
                    office_LeadsHelper.Select("Clientt", "Processing");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Leads refferal source.");
                    office_LeadsHelper.Select("SelectSource", "Campaign");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Leads category.");
                    office_LeadsHelper.SelectByText("SelectCat", "test");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select account manager.");
                    office_LeadsHelper.SelectByText("SelectAcc.Mgr", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Leads user group.");
                    office_LeadsHelper.Select("Ugroup", "81");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select sales manager.");
                    office_LeadsHelper.SelectByText("SelectSaleManager", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select Leads responsibility");
                    office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select partner agent.");
                    office_LeadsHelper.SelectByText("PartnerAgent", "Mark Matthews");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select partner association.");
                    office_LeadsHelper.SelectByText("PartnerAssociation", "AslamP.Association.");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "click on save Leads");
                    office_LeadsHelper.ClickElement("SaveLead");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "click on info tab.");
                    office_LeadsHelper.ClickElement("InfoTab");

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for locator to be present.");
                    office_LeadsHelper.WaitForElementPresent("ClientsType", 10);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for Leads type.");
                    office_LeadsHelper.VerifyText("ClientsType", "Processing");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for status.");
                    office_LeadsHelper.VerifyText("Status", "New");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for source.");
                    office_LeadsHelper.VerifyText("Source", "Campaign");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for category.");
                    office_LeadsHelper.VerifyText("Category", "test");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for responsibility.");
                    office_LeadsHelper.VerifyText("Responsibilityl", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for account manager.");
                    office_LeadsHelper.VerifyText("AccountManager", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for partner agent.");
                    office_LeadsHelper.VerifyText("PartnerAgentl", "Mark Matthews");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for partner association.");
                    office_LeadsHelper.VerifyText("PartnerAssociationl", "AslamP.Association.");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Verify label for partner association.");
                    office_LeadsHelper.VerifyText("SalesManager", "Howard Tang");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Redirect To leads page. ");
                    VisitOffice("leads");

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Enter Company Name");
                    office_LeadsHelper.TypeText("CompanySearch", CDBA);
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Select lead by check box");
                    office_LeadsHelper.ClickElement("ClickOn1stOpp");
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on delete lead");
                    office_LeadsHelper.ClickElement("DeleteLead");

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Accept alert message.");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for success message.");
                    office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Redirect To leads recycle bin page. ");
                    VisitOffice("leads/recyclebin");

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Enter Company Name");
                    office_LeadsHelper.TypeText("SearchLeadRbin", CDBA);
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Click on delete leads");
                    office_LeadsHelper.ClickElement("DeleteRbin");

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Accept alert message.");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("VerifyQuickLookLabelsForLeads", "Wait for success message.");
                    office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForLeads");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForLeads");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Opportunities", "Bug", "Medium", "Leadss page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForLeads");
                        TakeScreenshot("VerifyQuickLookLabelsForLeads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForLeads.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForLeads");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForLeads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyQuickLookLabelsForLeads.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForLeads"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForLeads");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForLeads");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForLeads", Status, JIRA, "Leads management");
            }
        }
    }
}

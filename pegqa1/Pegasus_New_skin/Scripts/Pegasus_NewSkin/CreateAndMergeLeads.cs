using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateAndMergeLeads : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("Temp")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void createAndMergeLeads()
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

                executionLog.Log("CreateAndMergeLeads", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("CreateAndMergeLeads", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateAndMergeLeads", "Redirect at Create Lead");
                VisitOffice("leads/create");

                executionLog.Log("CreateAndMergeLeads", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAndMergeLeads", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("CreateAndMergeLeads", "Enter Last Name");
                office_LeadsHelper.TypeText("LeadLastName", LName);

                executionLog.Log("CreateAndMergeLeads", "Enter Lead Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("CreateAndMergeLeads", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("CreateAndMergeLeads", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("CreateAndMergeLeads", "Select Responsibilities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("CreateAndMergeLeads", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(7000);


                var loc = "//h3[text()='Existing Leads']";
                if (office_LeadsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("We are in first If cond as lead is duplicate !!");
                    executionLog.Log("CreateAndMergeLeads", "Click on Duplicate");
                    office_LeadsHelper.ClickOnDisplayed("CraeteLeadDub");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);
                }
                else
                {
                    Console.WriteLine("We are in first else cond as lead is not duplicate !!");
                    executionLog.Log("CreateAndMergeLeads", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("CreateAndMergeLeads", "Go to Create Lead");
                    VisitOffice("leads/create");

                    executionLog.Log("CreateAndMergeLeads", "Save");
                    office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("CreateAndMergeLeads", "Enter First Name");
                    office_LeadsHelper.TypeText("FirstNameLead", FName);

                    executionLog.Log("CreateAndMergeLeads", "Enter Last Name");
                    office_LeadsHelper.TypeText("LeadLastName", LName);

                    executionLog.Log("CreateAndMergeLeads", "Company DBA Name");
                    office_LeadsHelper.TypeText("CompanyName", CDBA);

                    executionLog.Log("CreateAndMergeLeads", "Wait for element to be visible.");
                    office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                    executionLog.Log("CreateAndMergeLeads", "Select Lead Status");
                    office_LeadsHelper.SelectByText("LeadStatus", "New");

                    executionLog.Log("CreateAndMergeLeads", "Select Lead Status");
                    office_LeadsHelper.SelectByText("LeadStatus", "New");

                    executionLog.Log("CreateAndMergeLeads", "Verify Responsibilties");
                    office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                    executionLog.Log("CreateAndMergeLeads", "Save");
                    office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                    office_LeadsHelper.WaitForWorkAround(3000);
                }
                if (office_LeadsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("We are in second If condition as second lead is duplicate !!");
                    executionLog.Log("CreateAndMergeLeads", "Lead Duplicate Button");

                    office_LeadsHelper.ClickElement("DuplicateRadio");

                    office_LeadsHelper.ClickOnDisplayed("CraeteLeadDub");
                    office_LeadsHelper.WaitForWorkAround(10000);

                    executionLog.Log("CreateAndMergeLeads", "Waig for success message.");
                    office_LeadsHelper.WaitForText("Lead saved successfully. .", 10);

                    executionLog.Log("CreateAndMergeLeads", "Goto Lead");
                    VisitOffice("leads");

                    executionLog.Log("CreateAndMergeLeads", "Click First lead");
                    office_LeadsHelper.ClickElement("ClickOn1stOpp");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click 2nd lead");
                    office_LeadsHelper.ClickElement("ClickOn2ndOpp");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click on Merge");
                    office_LeadsHelper.ClickElement("ClickOnMergeRecords");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreateAndMergeLeads", "Select primary lead.");
                    office_LeadsHelper.ClickElement("LeadCompyRadioBtn");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click Merge");
                    office_LeadsHelper.ClickElement("ClickOnMergeBtn");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("CreateAndMergeLeads", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Merging Lead(s) Completed Successfully.", 20);

                }
                else
                {
                    Console.WriteLine("We are in second else cond as second lead is not duplicate !!");
                    executionLog.Log("CreateAndMergeLeads", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("CreateAndMergeLeads", "Goto Lead");
                    VisitOffice("leads");

                    executionLog.Log("CreateAndMergeLeads", "Click on 1st lead");
                    office_LeadsHelper.ClickElement("ClickOn1stOpp");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click on 2nd lead");
                    office_LeadsHelper.ClickElement("ClickOn2ndOpp");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click on Merge");
                    office_LeadsHelper.ClickElement("ClickOnMergeRecords");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("CreateAndMergeLeads", "Choose Company To Merge");
                    office_LeadsHelper.ClickElement("LeadCompyRadioBtn");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("CreateAndMergeLeads", "Click on Merge");
                    office_LeadsHelper.ClickElement("ClickOnMergeBtn");
                    office_LeadsHelper.AcceptAlert();

                    executionLog.Log("CreateAndMergeLeads", "Confirmation");
                    office_LeadsHelper.WaitForText("Merging Lead(s) Completed Successfully.", 10);
                    office_LeadsHelper.WaitForWorkAround(3000);

                }

                executionLog.Log("CreateAndMergeLeads", "Redirect To leads page. ");
                VisitOffice("leads");

                executionLog.Log("CreateAndMergeLeads", "Enter Company Name");
                office_LeadsHelper.TypeText("CompanySearch", CDBA);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAndMergeLeads", "Select lead by check box");
                office_LeadsHelper.ClickElement("ClickOn1stOpp");
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateAndMergeLeads", "Click on delete lead");
                office_LeadsHelper.ClickElement("DeleteLead");

                executionLog.Log("CreateAndMergeLeads", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("CreateAndMergeLeads", "Wait for success message.");
                office_LeadsHelper.WaitForText("1 records deleted successfully", 10);

                executionLog.Log("CreateAndMergeLeads", "Redirect To leads recycle bin page. ");
                VisitOffice("leads/recyclebin");
                office_LeadsHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateAndMergeLeads", "Enter Company Name");
                office_LeadsHelper.TypeText("SearchLeadRbin", CDBA);
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateAndMergeLeads", "Select All responsibity");
                office_LeadsHelper.SelectDropDownByText("//*[@id='gs_owner']", "All");
                office_LeadsHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateAndMergeLeads", "Click on delete leads");
                office_LeadsHelper.ClickElement("DeleteRbin");

                executionLog.Log("CreateAndMergeLeads", "Accept alert message.");
                office_LeadsHelper.AcceptAlert();

                executionLog.Log("CreateAndMergeLeads", "Wait for success message.");
                office_LeadsHelper.WaitForText("Lead Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateAndMergeLeads");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("CreateAndMergeLeads");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("CreateAndMergeLeads", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("CreateAndMergeLeads");
                        TakeScreenshot("CreateAndMergeLeads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Create And Merge Leads.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateAndMergeLeads");
                        string id = loginHelper.getIssueID("CreateAndMergeLeads");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Create And Merge Leads.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("CreateAndMergeLeads"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("CreateAndMergeLeads");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateAndMergeLeads");
                executionLog.WriteInExcel("CreateAndMergeLeads", Status, JIRA, "Leads Management");
            }
        }
    }
}
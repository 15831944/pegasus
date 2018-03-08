using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyLeadPDFTabError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyLeadPDFTabError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());
            var officeActivities_CallsHelper = new OfficeActivities_CallsHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var FName = "Test" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            var doc = "Docname" + RandomNumber(99,9999);
            var file = GetPathToFile() + "Up.jpg";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyLeadPDFTabError", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyLeadPDFTabError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLeadPDFTabError", "Redirect at Create Lead");
                VisitOffice("leads/create");
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyLeadPDFTabError", "Enter First Name");
                office_LeadsHelper.TypeText("FirstNameLead", FName);

                executionLog.Log("VerifyLeadPDFTabError", "Enter Last Name");
                office_LeadsHelper.TypeText("LeadLastName", LName);

                executionLog.Log("VerifyLeadPDFTabError", "Enter Lead Company DBA Name");
                office_LeadsHelper.TypeText("CompanyName", CDBA);

                executionLog.Log("VerifyLeadPDFTabError", "Wait for element to be visible.");
                office_LeadsHelper.WaitForElementPresent("LeadStatus", 10);

                executionLog.Log("VerifyLeadPDFTabError", "Select Lead Status");
                office_LeadsHelper.SelectByText("LeadStatus", "New");

                executionLog.Log("VerifyLeadPDFTabError", "Select Responsibilities");
                office_LeadsHelper.SelectByText("Responsibility", "Howard Tang");

                executionLog.Log("VerifyLeadPDFTabError", "Click on Save");
                office_LeadsHelper.ClickElement("SaveLeadNewSkin");
                office_LeadsHelper.WaitForWorkAround(3000);

                var loc = "//h3[text()='Existing Leads']";
                if (office_LeadsHelper.IsElementPresent(loc))
                {
                    Console.WriteLine("We are in first If cond as lead is duplicate !!");
                    executionLog.Log("VerifyLeadPDFTabError", "Click on Duplicate");
                    office_LeadsHelper.ClickOnDisplayed("CraeteLeadDub");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("VerifyLeadPDFTabError", "Redirect at leads page.");
                    VisitOffice("leads");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify page title as leads.");
                    VerifyTitle("Leads");

                    executionLog.Log("VerifyLeadPDFTabError", "Dearch lead by company name.");
                    office_LeadsHelper.TypeText("CompanySearch", CDBA);
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click to view searched lead.");
                    office_LeadsHelper.ClickElement("Lead1");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Go to PDF Tab");
                    office_LeadsHelper.ClickElement("PDFTab");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Add Document");
                    office_LeadsHelper.ClickElement("AddDocument");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Enter document name");
                    officeActivities_DocumentHelper.TypeText("Name", doc);

                    executionLog.Log("VerifyLeadPDFTabError", "Upload file");
                    officeActivities_DocumentHelper.Upload("Attachment", file);
                    officeActivities_DocumentHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click Save button");
                    officeActivities_DocumentHelper.ClickElement("ClientPopupSave");
                    officeActivities_DocumentHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify 500 Error not occured");
                    office_LeadsHelper.verifyElementPresent("AddDocument");

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Log a Call");
                    office_LeadsHelper.ClickElement("LogACall");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Enter from name");
                    officeActivities_CallsHelper.TypeText("CallFromName", "test1");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter to name");
                    officeActivities_CallsHelper.TypeText("CallToName", "test2");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter from number");
                    officeActivities_CallsHelper.TypeText("FromNumber", "4584698569");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter to number");
                    officeActivities_CallsHelper.TypeText("CallTONumber", "6984589652");

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Save button");
                    officeActivities_CallsHelper.ClickElement("PopupSave");
                    officeActivities_CallsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify 500 Error not occured");
                    office_LeadsHelper.verifyElementPresent("AddDocument");


                }
                else
                {
                    Console.WriteLine("We are in first else cond as lead is not duplicate !!");
                    executionLog.Log("VerifyLeadPDFTabError", "Wait for Confirmation");
                    office_LeadsHelper.WaitForText("Lead saved successfully.", 10);

                    executionLog.Log("VerifyLeadPDFTabError", "Redirect at leads page.");
                    VisitOffice("leads");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify page title as leads.");
                    VerifyTitle("Leads");

                    executionLog.Log("VerifyLeadPDFTabError", "Dearch lead by company name.");
                    office_LeadsHelper.TypeText("CompanySearch", CDBA);
                    office_LeadsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click to view searched lead.");
                    office_LeadsHelper.ClickElement("Lead1");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Go to PDF Tab");
                    office_LeadsHelper.ClickElement("PDFTab");
                    office_LeadsHelper.WaitForWorkAround(3000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Add Document");
                    office_LeadsHelper.ClickElement("AddDocument");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Enter document name");
                    officeActivities_DocumentHelper.TypeText("Name", doc);

                    executionLog.Log("VerifyLeadPDFTabError", "Upload file");
                    officeActivities_DocumentHelper.Upload("Attachment", file);
                    officeActivities_DocumentHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Click Save button");
                    officeActivities_DocumentHelper.ClickElement("ClientPopupSave");
                    officeActivities_DocumentHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify 500 Error not occured");
                    office_LeadsHelper.verifyElementPresent("AddDocument");

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Log a Call");
                    office_LeadsHelper.ClickElement("LogACall");
                    office_LeadsHelper.WaitForWorkAround(1000);

                    executionLog.Log("VerifyLeadPDFTabError", "Enter from name");
                    officeActivities_CallsHelper.TypeText("CallFromName", "test1");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter to name");
                    officeActivities_CallsHelper.TypeText("CallToName", "test2");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter from number");
                    officeActivities_CallsHelper.TypeText("FromNumber", "4584698569");

                    executionLog.Log("VerifyLeadPDFTabError", "Enter to number");
                    officeActivities_CallsHelper.TypeText("CallTONumber", "6984589652");

                    executionLog.Log("VerifyLeadPDFTabError", "Click on Save button");
                    officeActivities_CallsHelper.ClickElement("PopupSave");
                    officeActivities_CallsHelper.WaitForWorkAround(2000);

                    executionLog.Log("VerifyLeadPDFTabError", "Verify 500 Error not occured");
                    office_LeadsHelper.verifyElementPresent("AddDocument");

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLeadPDFTabError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyLeadPDFTabError");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyLeadPDFTabError", "Bug", "Medium", "Leads page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyLeadPDFTabError");
                        TakeScreenshot("VerifyLeadPDFTabError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Verify Lead PDF Tab Error.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLeadPDFTabError");
                        string id = loginHelper.getIssueID("VerifyLeadPDFTabError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Verify Lead PDF Tab Error.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyLeadPDFTabError"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyLeadPDFTabError");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("LeadCreateAndViewIssue");
                executionLog.WriteInExcel("LeadCreateAndViewIssue", Status, JIRA, "Leads Management");
            }
        }
    }
}
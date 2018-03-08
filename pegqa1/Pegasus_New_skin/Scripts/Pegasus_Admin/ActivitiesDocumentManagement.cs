using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ActivitiesDocumentManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesDocumentManagement()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_DocumentsHelper = new OfficeActivities_DocumentHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            var office_OpportunitiesHelper = new Office_OpportunitiesHelper(GetWebDriver());
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());

            // Variable
            var name = "Doc" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String ValidFile = GetPathToFile() + "leadslist.csv";
            String InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ActivitiesDocumentManagement", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesDocumentManagement", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesDocumentManagement", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("ActivitiesDocumentManagement", "Redirect to create document page");
                VisitOffice("documents/create");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Verify validation message for name.");
                officeActivities_DocumentsHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("ActivitiesDocumentManagement", "Verify validation message for attachment.");
                officeActivities_DocumentsHelper.VerifyText("AttachmentError", "This field is required.");

                executionLog.Log("ActivitiesDocumentManagement", "Enter Document name");
                officeActivities_DocumentsHelper.TypeText("Name", name);

                executionLog.Log("ActivitiesDocumentManagement", "Click on document version");
                officeActivities_DocumentsHelper.ClickElement("DocumentVersion");

                executionLog.Log("ActivitiesDocumentManagement", "Upload an invalid File ");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", InvalidFile);

                executionLog.Log("ActivitiesDocumentManagement", "Verify alert message for invalid file.");
                officeActivities_DocumentsHelper.VerifyAlertText("please select a valid file!");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("ActivitiesDocumentManagement", "Upload a valid File.");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", ValidFile);

                executionLog.Log("ActivitiesDocumentManagement", "Select Assign owner");
                officeActivities_DocumentsHelper.SelectByText("AssignOwner", "Howard Tang");

                executionLog.Log("ActivitiesDocumentManagement", "Select status");
                officeActivities_DocumentsHelper.SelectByText("Status", "Active");

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document saved successfully.", 10);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("ActivitiesDocumentManagement", "Select document parent/related to.");
                officeActivities_DocumentsHelper.SelectByText("ReletedTo", "Client");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on any client.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement", "Redirect at clients page.");
                VisitOffice("clients");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on any client.");
                office_ClientsHelper.ClickElement("Client1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select actitivity type as documents.");
                office_ClientsHelper.SelectByText("SelectActivityType", "Documents");

                executionLog.Log("ActivitiesDocumentManagement", "Enter document name to be search.");
                office_ClientsHelper.TypeText("ActivitySubject", name);
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in created by field");
                office_ClientsHelper.SelectByText("CreatedByField", "All");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Verify created document present on client page.");
                office_ClientsHelper.IsElementPresent("OpenFirstActivity");

                executionLog.Log("ActivitiesDocumentManagement", "Redirect to document page");
                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("ActivitiesDocumentManagement", "Select document related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "14");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on any lead.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement", "Redirect at leads page.");
                VisitOffice("leads");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click On any lead.");
                office_LeadsHelper.ClickElement("ClickAnyLead");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select actitivity type as documents.");
                office_LeadsHelper.SelectByText("SelectActivityType", "Documents");

                executionLog.Log("ActivitiesDocumentManagement", "Enter document name to be search.");
                office_LeadsHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Verify created document present on leads page.");
                office_LeadsHelper.IsElementPresent("ClickNotes1");

                executionLog.Log("ActivitiesDocumentManagement", "Redirect to document page");
                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("ActivitiesDocumentManagement", "Select document parent/related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "15");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on any opportunity.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement", "Redirect at opportunities page.");
                VisitOffice("opportunities");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click On any opportunity.");
                office_OpportunitiesHelper.ClickElement("Opportunities1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select actitivity type as documents");
                office_LeadsHelper.Select("SelectActivityType", "Documents");

                executionLog.Log("ActivitiesDocumentManagement", "Enter documents name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Verify created document present on opportunity page");
                office_OpportunitiesHelper.IsElementPresent("OpenOpportunity");

                executionLog.Log("ActivitiesDocumentManagement", "Redirect to document page");
                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("ActivitiesDocumentManagement", "Select document related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "36");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on any ticket.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement", "Redirect at tickets page.");
                VisitOffice("tickets");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click On any ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Select actitivity type as documents");
                office_LeadsHelper.Select("SelectActivityType", "Documents");

                executionLog.Log("ActivitiesDocumentManagement", "Enter document name to be search.");
                office_OpportunitiesHelper.TypeText("ActivitySubject", name);
                office_LeadsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Verify created document present on ticket page.");
                ticket_CreateATicketHelper.IsElementPresent("OpenTicket");

                executionLog.Log("ActivitiesDocumentManagement", "Redirect to document page");
                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Checkbox");
                officeActivities_DocumentsHelper.ClickElement("CheckDocToDel");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Click on Delete button");
                officeActivities_DocumentsHelper.ClickElement("DeleteDoc");
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Acccept alert to delete doc.");
                officeActivities_DocumentsHelper.AcceptAlert();
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement", "Redirect at recyclebin page.");
                VisitOffice("documents/recyclebin");
                office_ClientsHelper.WaitForWorkAround(2000);

                executionLog.Log("ActivitiesDocumentManagement", "Verify page title.");
                VerifyTitle("Recycled Document");

                executionLog.Log("ActivitiesDocumentManagement", "Click on delete icon.");
                officeActivities_DocumentsHelper.ClickElement("DeleteRecycle");
                officeActivities_DocumentsHelper.AcceptAlert();
                //officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement", "Wait for success message..");
                officeActivities_DocumentsHelper.WaitForText("Document Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesDocumentManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Activities Document Management");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Activities Document Management", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Activities Document Management");
                        TakeScreenshot("ActivitiesDocumentManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesDocumentManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesDocumentManagement");
                        string id = loginHelper.getIssueID("Activities Document Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesDocumentManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Document Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Document Management");
           //     executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesDocumentManagement");
                executionLog.WriteInExcel("Activities Document Management", Status, JIRA, "Office Activities");
            }
        }
    }
}
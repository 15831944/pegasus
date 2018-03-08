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
    public class ActivitiesDocumentManagement2 : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void activitiesDocumentManagement2()
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
            String ValidFile = GetPathToFile() + "index.jpg";
            String InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("ActivitiesDocumentManagement2", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ActivitiesDocumentManagement2", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("ActivitiesDocumentManagement2", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("ActivitiesDocumentManagement2", "Redirect to create document page");
                VisitOffice("documents/create");

                executionLog.Log("ActivitiesDocumentManagement2", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement2", "Verify validation message for name.");
                officeActivities_DocumentsHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("ActivitiesDocumentManagement2", "Verify validation message for attachment.");
                officeActivities_DocumentsHelper.VerifyText("AttachmentError", "This field is required.");

                executionLog.Log("ActivitiesDocumentManagement2", "Enter Document name");
                officeActivities_DocumentsHelper.TypeText("Name", name);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on document version");
                officeActivities_DocumentsHelper.ClickElement("DocumentVersion");

                executionLog.Log("ActivitiesDocumentManagement2", "Upload an invalid File ");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", InvalidFile);

                executionLog.Log("ActivitiesDocumentManagement2", "Verify alert message for invalid file.");
                officeActivities_DocumentsHelper.VerifyAlertText("please select a valid file!");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("ActivitiesDocumentManagement2", "Upload a valid File.");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", ValidFile);

                executionLog.Log("ActivitiesDocumentManagement2", "Select Assign owner");
                officeActivities_DocumentsHelper.SelectByText("AssignOwner", "Howard Tang");

                executionLog.Log("ActivitiesDocumentManagement2", "Select status");
                officeActivities_DocumentsHelper.Select("Status", "Active");

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("ActivitiesDocumentManagement2", "Select document related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "18");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on any meeting.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Redirect at meetings page.");
                VisitOffice("meetings");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for locator to be present.");
                officeActivities_DocumentsHelper.WaitForElementPresent("ClickOnAnyMeeting", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on any meeting.");
                office_ClientsHelper.ClickElement("ClickOnAnyMeeting");
                office_ClientsHelper.WaitForWorkAround(3000);

                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(3000);

                VerifyTitle("Documents");

                executionLog.Log("ActivitiesDocumentManagement2", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Select document related to.");
                officeActivities_DocumentsHelper.SelectByText("ReletedTo", "Client");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on any task.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Redirect at tasks page.");
                VisitOffice("tasks");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for locator to be present.");
                officeActivities_DocumentsHelper.WaitForElementPresent("OpenTask", 10);

                VisitOffice("documents");
                office_ClientsHelper.WaitForWorkAround(3000);

                VerifyTitle("Documents");

                executionLog.Log("ActivitiesDocumentManagement2", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on add new version.");
                officeActivities_DocumentsHelper.ClickElement("NewVersion");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Upload a valid File.");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", ValidFile);
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Write a comment about version.");
                officeActivities_DocumentsHelper.TypeText("DocCommnet", "This is newly added version.");

                executionLog.Log("ActivitiesDocumentManagement2", "Click on save button.");
                officeActivities_DocumentsHelper.ClickElement("AddVersionSave");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("New Version File Uploaded successfully.", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Redirect to document page");
                VisitOffice("documents");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for locator to be present.");
                officeActivities_DocumentsHelper.WaitForElementPresent("SearchDocumet", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Checkbox");
                officeActivities_DocumentsHelper.ClickElement("CheckDocToDel");

                executionLog.Log("ActivitiesDocumentManagement2", "Click on Delete button");
                officeActivities_DocumentsHelper.ClickElement("DeleteDoc");

                executionLog.Log("ActivitiesDocumentManagement2", "Acccept alert to delete doc.");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.", 20);

                executionLog.Log("ActivitiesDocumentManagement2", "Redirect at recyclebin page.");
                VisitOffice("documents/recyclebin");

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for locator to be present.");
                officeActivities_DocumentsHelper.WaitForElementPresent("DeleteRecycle", 10);

                executionLog.Log("ActivitiesDocumentManagement2", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Click on delete icon.");
                officeActivities_DocumentsHelper.ClickElement("DeleteRecycle");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("ActivitiesDocumentManagement2", "Wait for success message..");
                officeActivities_DocumentsHelper.WaitForText("Document Permanently Deleted.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ActivitiesDocumentManagement2");
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
                        TakeScreenshot("ActivitiesDocumentManagement2");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesDocumentManagement2.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ActivitiesDocumentManagement2");
                        string id = loginHelper.getIssueID("Activities Document Management");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ActivitiesDocumentManagement2.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Activities Document Management"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Activities Document Management");
              //  executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("ActivitiesDocumentManagement2");
                executionLog.WriteInExcel("Activities Document Management", Status, JIRA, "Office Activities");
            }
        }
    }
}
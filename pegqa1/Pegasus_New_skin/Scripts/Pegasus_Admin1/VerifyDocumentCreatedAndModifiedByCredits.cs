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
    public class VerifyDocumentCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyDocumentCreatedAndModifiedByCredits()
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

            // Variable
            var name = "Doc" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String ValidFile = GetPathToFile() + "index.jpg";
            String InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Redirect to create document page");
                VisitOffice("documents/create");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify validation message for name.");
                officeActivities_DocumentsHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify validation message for attachment.");
                officeActivities_DocumentsHelper.VerifyText("AttachmentError", "This field is required.");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Enter Document name");
                officeActivities_DocumentsHelper.TypeText("Name", name);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on document version");
                officeActivities_DocumentsHelper.ClickElement("DocumentVersion");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Upload an invalid File ");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", InvalidFile);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify alert message for invalid file.");
                officeActivities_DocumentsHelper.VerifyAlertText("please select a valid file!");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Upload a valid File.");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", ValidFile);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select Assign owner");
                officeActivities_DocumentsHelper.SelectByText("AssignOwner", "Howard Tang");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select status");
                officeActivities_DocumentsHelper.Select("Status", "Active");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditDoc");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select document parent/related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "20");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on any client.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Redirect to document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on searched document.");
                officeActivities_DocumentsHelper.ClickElement("DocumentClick");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify Document created by credits.");
                officeActivities_DocumentsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify Document modified by credits.");
                officeActivities_DocumentsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on edit icon.");
                officeActivities_DocumentsHelper.ClickElement("EditLink");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select document related to.");
                officeActivities_DocumentsHelper.Select("ReletedTo", "14");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on find list icon.");
                officeActivities_DocumentsHelper.ClickElement("Assign");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on any lead.");
                officeActivities_DocumentsHelper.ClickElement("AssignUser");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Redirect to document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on searched document.");
                officeActivities_DocumentsHelper.ClickElement("DocumentClick");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify Document created by credits.");
                officeActivities_DocumentsHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify Document modified by credits.");
                officeActivities_DocumentsHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Redirect to document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", "Doc");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Checkbox");
                officeActivities_DocumentsHelper.ClickElement("CheckDocToDel");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on Delete button");
                officeActivities_DocumentsHelper.ClickJs("DeleteDoc");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Acccept alert to delete doc.");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Redirect at recyclebin page.");
                VisitOffice("documents/recyclebin");

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Recycled Document");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Search the Doc ");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", "Doc");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Select All in owner field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Click on delete icon.");
                officeActivities_DocumentsHelper.ClickElement("DeleteRecycle");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyDocumentCreatedAndModifiedByCredits", "Wait for success message..");
                officeActivities_DocumentsHelper.WaitForText("Document Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDocumentCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Document Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Document Created And Modified By Credits", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Document Created And Modified By Credits");
                        TakeScreenshot("VerifyDocumentCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDocumentCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Document Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Document Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Document Created And Modified By Credits");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyDocumentCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Document Created And Modified By Credits", Status, JIRA, "Office Activities");
            }
        }
    }
}

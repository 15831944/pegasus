using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class DocumentManagement : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void documentManagement()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var fileInvalid = GetPathToFile() + "Invalid.dll";
            var file = GetPathToFile() + "index.jpg";
            var Document = "New" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DocumentManagement", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("DocumentManagement", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DocumentManagement", "Visit Document");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Click on Create Document");
                officeActivities_DocumentHelper.ClickElement("ClickOnCreateDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Document Name");
                officeActivities_DocumentHelper.TypeText("Name", Document);

                executionLog.Log("DocumentManagement", "Upload Document");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", file);
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Click on Save button.");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("DocumentManagement", "Wait for Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("DocumentManagement", "Search Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Document);
                officeActivities_DocumentHelper.WaitForWorkAround(2000);
                officeActivities_DocumentHelper.selectOwner("//*[@id='gs_first_name']");
                officeActivities_DocumentHelper.WaitForWorkAround(6000);

                executionLog.Log("DocumentManagement", "Click on Edit");
                officeActivities_DocumentHelper.ClickElement("EditDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Click on Save");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("DocumentManagement", "Verify Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document updated successfully.", 10);

                executionLog.Log("DocumentManagement", "Redirect at Documents page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Click on Create Document");
                officeActivities_DocumentHelper.ClickElement("ClickOnCreateDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Click on Save button.");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("DocumentManagement", "Wait for Validation");
                officeActivities_DocumentHelper.WaitForText("This field is required.", 10);

                executionLog.Log("DocumentManagement", "Uplaod File");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", fileInvalid);
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Accept alert message.");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentManagement", "Valiadtion please select a valid file");
                officeActivities_DocumentHelper.WaitForText("please select a valid file", 10);

                executionLog.Log("DocumentManagement", "Accept alert message.");
                officeActivities_DocumentHelper.AcceptAlert();
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Click on Cancel");
                officeActivities_DocumentHelper.ClickElement("DocumentCancelButton");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                officeActivities_DocumentHelper.VerifyText("VerifyDocumentHeader", "Documents");
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("DocumentManagement", "Redirect at documents page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Click on first document");
                officeActivities_DocumentHelper.ClickElement("DocumentClick");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Add Version");
                officeActivities_DocumentHelper.ClickElement("AddVersion");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Uplaod");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", file);
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentManagement", "Add Comment");
                officeActivities_DocumentHelper.TypeText("AddComment", "Test Comment");

                executionLog.Log("DocumentManagement", "Click on Save");
                officeActivities_DocumentHelper.ClickElement("AddVersionSave");

                executionLog.Log("DocumentManagement", "Wait for Confirmation");
                officeActivities_DocumentHelper.WaitForText("New Version File Uploaded successfully.", 10);

                executionLog.Log("DocumentManagement", "Click on Delete");
                officeActivities_DocumentHelper.ClickElement("DeleteVersion");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentManagement", "Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document Version Deleted Permanently.", 10);

                executionLog.Log("DocumentManagement", "Goto Document");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Search Documet");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Document);
                officeActivities_DocumentHelper.selectOwner("//*[@id='gs_first_name']");
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentManagement", "Click on Check box for first document");
                officeActivities_DocumentHelper.ClickElement("ClickOnCheckBox");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Click Delete");
                officeActivities_DocumentHelper.ClickElement("DeleteDocument");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentManagement", "Confirmaion");
                officeActivities_DocumentHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("DocumentManagement", "Redirect at Recycle bin");
                VisitOffice("documents/recyclebin");

                executionLog.Log("DocumentManagement", "Search Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Document);
                officeActivities_DocumentHelper.selectOwner("//*[@id='gs_first_name']");
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentManagement", "Restore Document");
                officeActivities_DocumentHelper.ClickElement("RestoredDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document Restored Successfully.", 10);

                executionLog.Log("DocumentManagement", "Goto Document");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Search Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Document);
                officeActivities_DocumentHelper.selectOwner("//*[@id='gs_first_name']");
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentManagement", "Select document");
                officeActivities_DocumentHelper.ClickElement("CheckDocToDel");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentManagement", "Click on Delete");
                officeActivities_DocumentHelper.ClickElement("DeleteDocument");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentManagement", "Wait for Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("DocumentManagement", "Redirect at Recycle bin");
                VisitOffice("documents/recyclebin");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("DocumentManagement", "Search Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Document);
                officeActivities_DocumentHelper.selectOwner("//*[@id='gs_first_name']");
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("DocumentManagement", "Delete From Recycle bin");
                officeActivities_DocumentHelper.ClickElement("DeleteRecycleBin");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("DocumentManagement", "Wait for Confirmation");
                officeActivities_DocumentHelper.WaitForText("Document Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentManagement");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("DocumentManagement");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("DocumentManagement", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("DocumentManagement");
                        TakeScreenshot("DocumentManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentManagement.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentManagement");
                        string id = loginHelper.getIssueID("DocumentManagement");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentManagement.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("DocumentManagement"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("DocumentManagement");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DocumentManagement");
                executionLog.WriteInExcel("DocumentManagement", Status, JIRA, "Office Activities.");
            }
        }
    }
}

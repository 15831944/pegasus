using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyQuickLookLabelsForDocuments : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyQuickLookLabelsForDocuments()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_DocumentsHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var name = "Doc" + RandomNumber(1, 9999);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            var ValidFile = GetPathToFile() + "index.jpg";
            var InvalidFile = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyQuickLookLabelsForDocuments", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Redirect at create document page");
                VisitOffice("documents/create");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify validation message for name.");
                officeActivities_DocumentsHelper.VerifyText("NameError", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify validation message for attachment.");
                officeActivities_DocumentsHelper.VerifyText("AttachmentError", "This field is required.");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Enter Document name");
                officeActivities_DocumentsHelper.TypeText("Name", name);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on document version");
                officeActivities_DocumentsHelper.ClickElement("DocumentVersion");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Upload an invalid File ");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", InvalidFile);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify alert message for invalid file.");
                officeActivities_DocumentsHelper.VerifyAlertText("please select a valid file!");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Upload a valid File.");
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", ValidFile);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select Assign owner");
                officeActivities_DocumentsHelper.SelectByText("AssignOwner", "Howard Tang");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select status");
                officeActivities_DocumentsHelper.Select("Status", "Active");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Redirect at document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "verify page title");
                VerifyTitle("Documents");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Search created document.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on any document.");
                officeActivities_DocumentsHelper.ClickElement("OpenDoc");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document status.");
                officeActivities_DocumentsHelper.VerifyText("VerifyStatus", "Active");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document category.");
                officeActivities_DocumentsHelper.VerifyText("VerifyCategory", "click to add");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document responsibility.");
                officeActivities_DocumentsHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on edit button.");
                officeActivities_DocumentsHelper.ClickElement("EditLink");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select status for document.");
                officeActivities_DocumentsHelper.SelectByText("Status", "InActive");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select category for document.");
                officeActivities_DocumentsHelper.SelectByText("Category", "pdf");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select owner for document.");
                officeActivities_DocumentsHelper.SelectByText("AssignOwner", "Howard Tang");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on save button.");
                officeActivities_DocumentsHelper.ClickElement("Save");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document status.");
                officeActivities_DocumentsHelper.VerifyText("VerifyStatus", "InActive");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document category.");
                officeActivities_DocumentsHelper.VerifyText("Category2", "pdf");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify label for document responsibility.");
                officeActivities_DocumentsHelper.VerifyText("VerifyResponsibility", "Howard Tang");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on delete icon.");
                officeActivities_DocumentsHelper.ClickElement("DeleteVersion");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Redirect at documents recyclebin page.");
                VisitOffice("documents/recyclebin");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Verify page title.");
                VerifyTitle("Recycled Documents");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Search document by name.");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", name);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Select 'All' at Owner Field");
                officeActivities_DocumentsHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Click on delete icon.");
                officeActivities_DocumentsHelper.ClickElement("DeleteRecycle");

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Acccept alert to delete doc.");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("VerifyQuickLookLabelsForDocuments", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyQuickLookLabelsForDocuments");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyQuickLookLabelsForDocuments");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyQuickLookLabelsForDocuments", "Bug", "Medium", "Tasks page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForDocuments");
                        TakeScreenshot("VerifyQuickLookLabelsForDocuments");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyQuickLookLabelsForDocuments");
                        string id = loginHelper.getIssueID("VerifyQuickLookLabelsForDocuments");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Contact.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyQuickLookLabelsForDocuments"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyQuickLookLabelsForDocuments");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyQuickLookLabelsForDocuments");
                executionLog.WriteInExcel("VerifyQuickLookLabelsForDocuments", Status, JIRA, "Activities Management");
            }
        }
    }
}


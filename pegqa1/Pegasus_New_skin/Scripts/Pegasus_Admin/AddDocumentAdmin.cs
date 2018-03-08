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
    public class AddDocumentAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void addDocumentAdmin()
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
            var name = "Testing Subject" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AddDocumentAdmin", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AddDocumentAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AddDocumentAdmin", "Click On  Admin");
                VisitOffice("admin");

                executionLog.Log("AddDocumentAdmin", "Redirect to create document page");
                VisitOffice("documents/create");

                executionLog.Log("AddDocumentAdmin", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("AddDocumentAdmin", "Enter Document name");
                officeActivities_DocumentsHelper.TypeText("Name", "TEST DOCUMENT");

                executionLog.Log("AddDocumentAdmin", "Click on document version");
                officeActivities_DocumentsHelper.ClickElement("DocumentVersion");

                executionLog.Log("AddDocumentAdmin", "Upload File ");
                String Filename = GetPathToFile() + "index.jpg";
                officeActivities_DocumentsHelper.Upload("BrowseAttachment", Filename);

                executionLog.Log("AddDocumentAdmin", "Select Assign owner");
                officeActivities_DocumentsHelper.ClickElement("AssignOwner");

                executionLog.Log("AddDocumentAdmin", "Select status");
                officeActivities_DocumentsHelper.ClickElement("Status");

                executionLog.Log("AddDocumentAdmin", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");

                executionLog.Log("AddDocumentAdmin", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("AddDocumentAdmin", "Wait for locator to be present.");
                officeActivities_DocumentsHelper.WaitForElementPresent("CheckDocToDel", 10);

                executionLog.Log("AddDocumentAdmin", "Click on Checkbox");
                officeActivities_DocumentsHelper.ClickElement("CheckDocToDel");

                executionLog.Log("AddDocumentAdmin", "Click on Delete button");
                officeActivities_DocumentsHelper.ClickElement("DeleteDoc");

                executionLog.Log("AddDocumentAdmin", "Acccept alert to delete doc.");
                officeActivities_DocumentsHelper.AcceptAlert();

                executionLog.Log("AddDocumentAdmin", "Wait for success message.");
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AddDocumentAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Add Document Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Add Document Admin", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Add Document Admin");
                        TakeScreenshot("AddDocumentAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddDocumentAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AddDocumentAdmin");
                        string id = loginHelper.getIssueID("Add Document Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AddDocumentAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Add Document Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Add Document Admin");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("AddDocumentAdmin");
                executionLog.WriteInExcel("Add Document Admin", Status, JIRA, "Office Activities");
            }
        }
    }
}

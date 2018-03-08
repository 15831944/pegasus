using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditDocSaveIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editDocSaveIssue()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            var Name = "QADoc" + RandomNumber(100, 999);
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("EditDocSaveIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditDocSaveIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditDocSaveIssue", "Redirect to create document page");
                VisitOffice("documents/create");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDocSaveIssue", "verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("EditDocSaveIssue", "Enter Document name");
                officeActivities_DocumentHelper.TypeText("Name", Name);

                executionLog.Log("EditDocSaveIssue", "Click on document version");
                officeActivities_DocumentHelper.ClickElement("DocumentVersion");

                executionLog.Log("EditDocSaveIssue", "Upload File ");
                String Filename = GetPathToFile() + "index.jpg";
                officeActivities_DocumentHelper.Upload("BrowseAttachment", Filename);

                executionLog.Log("EditDocSaveIssue", "Select Assign owner");
                officeActivities_DocumentHelper.ClickElement("AssignOwner");

                executionLog.Log("EditDocSaveIssue", "Select status");
                officeActivities_DocumentHelper.ClickElement("Status");

                executionLog.Log("EditDocSaveIssue", "Click on Save button");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("EditDocSaveIssue", "Wait for success message.");
                officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 10);

                executionLog.Log("EditDocSaveIssue", "Search the document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Name);
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("EditDocSaveIssue", "Edit first doc");
                officeActivities_DocumentHelper.ClickElement("EditDoc");

                executionLog.Log("EditDocSaveIssue", "Verify title");
                VerifyTitle("Edit Document");

                executionLog.Log("EditDocSaveIssue", "Click on Save button");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("EditDocSaveIssue", "Verify Save button working properly");
                officeActivities_DocumentHelper.WaitForText("Document updated successfully.", 10);

                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(5000);

                executionLog.Log("EditDocSaveIssue", "Search the Document");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", Name);
                officeActivities_DocumentHelper.WaitForWorkAround(4000);

                executionLog.Log("EditDocSaveIssue", "Delete the document");
                officeActivities_DocumentHelper.ClickElement("CheckDocToDel");
                officeActivities_DocumentHelper.ClickElement("DeleteDoc");
                officeActivities_DocumentHelper.AcceptAlert();
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditDocSaveIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Doc Save Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Doc Save Issue", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Doc Save Issue");
                        TakeScreenshot("EditDocSaveIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDocSaveIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditDocSaveIssue");
                        string id = loginHelper.getIssueID("Edit Doc Save Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDocSaveIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Doc Save Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Doc Save Issue");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditDocSaveIssue");
                executionLog.WriteInExcel("Edit Doc Save Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}

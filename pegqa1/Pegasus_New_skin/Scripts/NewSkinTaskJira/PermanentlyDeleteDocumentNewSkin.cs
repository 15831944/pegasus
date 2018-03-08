using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class PermanentlyDeleteDocumentNewSkin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void permanentlyDeleteDocumentNewSkin()
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


            // Variable random
            var name = "TESTCLIENT" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Redirect To Document");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Click On Create");
                officeActivities_DocumentHelper.ClickElement("ClickOnDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "ClickOnCreate");
                officeActivities_DocumentHelper.TypeText("Name", "DELETE DOCUMENT");
                string pathtofile = GetPathToFile() + "index.jpg";

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Attach File");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", pathtofile);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Click on Save");
                officeActivities_DocumentHelper.ClickElement("Save");

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Verify message");
                officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 05);
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Search Documet ");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", "DELETE DOCUMENT");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Select All in owner field");
                officeActivities_DocumentHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Click on Checkbox");
                officeActivities_DocumentHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Click On delete");
                officeActivities_DocumentHelper.ClickElement("ClickOndelete");

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Accept alert messsage.");
                officeActivities_DocumentHelper.AcceptAlert();

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Verify Document deleted successfully.");
                officeActivities_DocumentHelper.WaitForText("Document deleted successfully.", 10);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Click on Recycle bin");
                officeActivities_DocumentHelper.ClickElement("ClickOnReycleBin");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Search Documet ");
                officeActivities_DocumentHelper.TypeText("SearchDocumet", "DELETE DOCUMENT");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Select All in owner field");
                officeActivities_DocumentHelper.SelectByText("OwnerField", "All");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "ClickOnDeletePer");
                officeActivities_DocumentHelper.ClickElement("ClickOnDeletePer");

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "Accept alert messsage.");
                officeActivities_DocumentHelper.AcceptAlert();
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("PermanentlyDeleteDocumentNewSkin", "verify Document Permanently Deleted.");
                officeActivities_DocumentHelper.WaitForText("Document Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("PermanentlyDeleteDocumentNewSkin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Permanently Delete Document New Skin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Permanently Delete Document New Skin", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Permanently Delete Document New Skin");
                        TakeScreenshot("PermanentlyDeleteDocumentNewSkin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PermanentlyDeleteDocumentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("PermanentlyDeleteDocumentNewSkin");
                        string id = loginHelper.getIssueID("Permanently Delete Document New Skin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\PermanentlyDeleteDocumentNewSkin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Permanently Delete Document New Skin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Permanently Delete Document New Skin");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("PermanentlyDeleteDocumentNewSkin");
                executionLog.WriteInExcel("Permanently Delete Document New Skin", Status, JIRA, "Office Activities");
            }
        }
    }
}
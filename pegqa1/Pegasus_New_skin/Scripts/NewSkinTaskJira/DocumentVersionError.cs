using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DocumentVersionError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void documentVersionError()
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

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("DocumentVersionError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentVersionError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("DocumentVersionError", "Redirect To Document");
                VisitOffice("documents/create");

                executionLog.Log("DocumentVersionError", "Verify title");
                VerifyTitle("Create a New Document");

                executionLog.Log("DocumentVersionError", "Verify version is set to 1");
                string value = officeActivities_DocumentHelper.getFiledText("DocumentVersion");
                Assert.IsTrue(value.Contains("1"));

                executionLog.Log("DocumentVersionError", "Redirect To Document");
                VisitOffice("documents");

                executionLog.Log("DocumentVersionError", "Verify title");
                VerifyTitle("Documents");

                executionLog.Log("DocumentVersionError", "Edit first doc");
                officeActivities_DocumentHelper.ClickElement("EditDoc");

                executionLog.Log("DocumentVersionError", "Verify title");
                VerifyTitle("Edit Document");

                executionLog.Log("DocumentVersionError", "Verify document version is not user editable");
                Console.WriteLine("CHECK = " + officeActivities_DocumentHelper.GetAtrributeByLocator("//*[@id='DocumentVersion']", "readonly"));
                Assert.IsTrue(officeActivities_DocumentHelper.GetAtrributeByLocator("//*[@id='DocumentVersion']", "readonly").Contains("true"));

                executionLog.Log("DocumentVersionError", "Click on Add new version");
                officeActivities_DocumentHelper.ClickElement("NewVersion");
                officeActivities_DocumentHelper.WaitForWorkAround(2000);

                executionLog.Log("DocumentVersionError", "Upload file");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", GetPathToFile() + "2.pdf");

                executionLog.Log("DocumentVersionError", "Enter comment");
                officeActivities_DocumentHelper.TypeText("DocCommnet", "Comment");

                executionLog.Log("DocumentVersionError", "Click on Save button");
                officeActivities_DocumentHelper.ClickElement("EditSave");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("DocumentVersionError", "Verify added version is not deletable");
                officeActivities_DocumentHelper.verifyVersionNotDeletable();
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentVersionError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Document Version Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Document Version Error", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Document Version Error");
                        TakeScreenshot("DocumentVersionError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentVersionError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentVersionError");
                        string id = loginHelper.getIssueID("Document Version Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentVersionError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Document Version Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Document Version Error");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DocumentVersionError");
                executionLog.WriteInExcel("Document Version Error", Status, JIRA, "Office Activities");
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DocumentUpdateVersion : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void documentUpdateVersion()
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
                executionLog.Log("DocumentUpdateVersion", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("DocumentUpdateVersion", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("DocumentUpdateVersion", "Redirect to URL");
                VisitOffice("documents");

                executionLog.Log("DocumentUpdateVersion", "Wait for element to present");
                officeActivities_DocumentHelper.WaitForElementPresent("EditDoc", 10);

                executionLog.Log("DocumentUpdateVersion", " Click On Edit Icon");
                officeActivities_DocumentHelper.ClickElement("EditDoc");

                executionLog.Log("DocumentUpdateVersion", "Verify page title");
                VerifyTitle("Edit Document");

                executionLog.Log("DocumentUpdateVersion", "ClickOnAddNewVesion");
                officeActivities_DocumentHelper.ClickElement("NewVersion");

                string value = officeActivities_DocumentHelper.GetAtrributeByLocator("//*[@id='DocumentVersion']", "value");
                Console.WriteLine("Value in string = " + value);

                executionLog.Log("DocumentUpdateVersion", "upload file");
                var path = GetPathToFile() + "1.pdf";
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", path);

                executionLog.Log("DocumentUpdateVersion", "Enter coment");
                officeActivities_DocumentHelper.TypeText("DocCommnet", "Test Comment");

                executionLog.Log("DocumentUpdateVersion", "Click Save");
                officeActivities_DocumentHelper.ClickDisplayed("//input[@title='Save']");
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("DocumentUpdateVersion", "New Version File Uploaded successfully.");
                officeActivities_DocumentHelper.WaitForText("New Version File Uploaded successfully.", 8);

                double intValue = double.Parse(value);
                Console.WriteLine("Value in int = " + intValue);
                officeActivities_DocumentHelper.WaitForWorkAround(5000);
                value = officeActivities_DocumentHelper.GetAtrributeByLocator("//*[@id='DocumentVersion']", "value");
                Assert.IsTrue(intValue + 1 == double.Parse(value));

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DocumentUpdateVersion");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Document Update Version");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Document Update Version", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Document Update Version");
                        TakeScreenshot("DocumentUpdateVersion");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentUpdateVersion.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DocumentUpdateVersion");
                        string id = loginHelper.getIssueID("Document Update Version");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DocumentUpdateVersion.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Document Update Version"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Document Update Version");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DocumentUpdateVersion");
                executionLog.WriteInExcel("Document Update Version", Status, JIRA, "Office Activities");
            }
        }
    }
}
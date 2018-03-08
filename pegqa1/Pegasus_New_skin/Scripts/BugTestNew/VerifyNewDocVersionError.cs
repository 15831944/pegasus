using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyNewDocVersionError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyNewDocVersionError()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeAdmin_CorporateHelper = new OfficeAdmin_CorporateHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var doc = "Docname" + RandomNumber(99,9999);
            var file = GetPathToFile() + "Up.jpg";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyNewDocVersionError", "Login with valid credentials");
                Login(username[0], password[0]);

                executionLog.Log("VerifyNewDocVersionError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyNewDocVersionError", "Redirect at Corporate Details page");
                VisitOffice("mycorp");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNewDocVersionError", "Click Add Document button");
                officeAdmin_CorporateHelper.ClickElement("AddDocument");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNewDocVersionError", "Enter document name");
                officeAdmin_CorporateHelper.TypeText("DocumentName", doc);

                executionLog.Log("VerifyNewDocVersionError", "Upload file");
                officeAdmin_CorporateHelper.Upload("BrowseFile", file);
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNewDocVersionError", "Click Save button");
                officeAdmin_CorporateHelper.ClickElement("SaveDoc");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNewDocVersionError", "Select Activity Type >> Documents");
                officeAdmin_CorporateHelper.SelectByText("ActivityType", "Documents");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNewDocVersionError", "Enter document name to search");
                officeAdmin_CorporateHelper.TypeText("SearchActivity", doc);
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNewDocVersionError", "Click edit icon of activity");
                officeAdmin_CorporateHelper.ClickElement("EditActivity1");
                officeAdmin_CorporateHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyNewDocVersionError", "Click on Add New Version");
                officeAdmin_CorporateHelper.ClickElement("AddNewDocVersion");
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNewDocVersionError", "Upload file");
                officeAdmin_CorporateHelper.Upload("Attachment", file);
                officeAdmin_CorporateHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyNewDocVersionError", "Enter Comment");
                officeAdmin_CorporateHelper.TypeText("Comment", "testing");

                executionLog.Log("VerifyNewDocVersionError", "Click Save button");
                officeAdmin_CorporateHelper.ClickElement("PopupSave");
                officeAdmin_CorporateHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyNewDocVersionError", "Verify 'Page Not Found' Error not occured");
                officeAdmin_CorporateHelper.verifyElementPresent("AddNewDocVersion");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyNewDocVersionError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("VerifyNewDocVersionError");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("VerifyNewDocVersionError", "Bug", "Medium", "Corporate Details page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("VerifyNewDocVersionError");
                        TakeScreenshot("VerifyNewDocVersionError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Verify New Doc Version Error.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyNewDocVersionError");
                        string id = loginHelper.getIssueID("VerifyNewDocVersionError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Verify New Doc Version Error.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("VerifyNewDocVersionError"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("VerifyNewDocVersionError");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyNewDocVersionError");
                executionLog.WriteInExcel("VerifyNewDocVersionError", Status, JIRA, "Corporate Details Management");
            }
        }
    }
}
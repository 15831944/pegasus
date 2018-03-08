using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class UniqueDocError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Test")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void uniqueDocError()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());

            try
            {
            executionLog.Log("UniqueDocError", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("UniqueDocError", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("UniqueDocError", "Redirect To create Document page");
            VisitOffice("documents/create");

            executionLog.Log("UniqueDocError", "Verify title");
            VerifyTitle("Create a New Document");

            executionLog.Log("UniqueDocError", "ClickOnCreate");
            officeActivities_DocumentHelper.TypeText("Name", "Doc1");

            string pathtofile = GetPathToFile() + "Upload1.pdf";
            executionLog.Log("UniqueDocError", "Attach File");
            officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", pathtofile);

            executionLog.Log("UniqueDocError", "Select releted to");
            officeActivities_DocumentHelper.SelectByText("ReletedTo", "Client");

            executionLog.Log("UniqueDocError", "Select Client");
            officeActivities_DocumentHelper.ClickElement("Assign");
            officeActivities_DocumentHelper.WaitForWorkAround(4000);

            officeActivities_DocumentHelper.ClickElement("AssignUser");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Click on Save");
            officeActivities_DocumentHelper.ClickElement("Save");

            executionLog.Log("UniqueDocError", "Wait for success message.");
            officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 10);

            executionLog.Log("UniqueDocError", "Redirect To Document");
            VisitOffice("documents/create");

            executionLog.Log("UniqueDocError", "Verify title");
            VerifyTitle("Create a New Document");

            executionLog.Log("UniqueDocError", "ClickOnCreate");
            officeActivities_DocumentHelper.TypeText("Name", "Doc2");

            pathtofile = GetPathToFile() + "Upload2.pdf";
            executionLog.Log("UniqueDocError", "Attach File");
            officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", pathtofile);

            executionLog.Log("UniqueDocError", "Select releted to");
            officeActivities_DocumentHelper.SelectByText("ReletedTo", "Client");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Select Client");
            officeActivities_DocumentHelper.ClickElement("Assign");
            officeActivities_DocumentHelper.WaitForWorkAround(4000);

            officeActivities_DocumentHelper.ClickElement("AssignUser");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Click on Save");
            officeActivities_DocumentHelper.ClickElement("Save");

            executionLog.Log("UniqueDocError", "Wait for success message.");
            officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 10);

            executionLog.Log("UniqueDocError", "Verify page title.");
            VerifyTitle("Documents");

            executionLog.Log("UniqueDocError", "Redirect To Document");
            VisitOffice("documents/create");

            executionLog.Log("UniqueDocError", "ClickOnCreate");
            officeActivities_DocumentHelper.TypeText("Name", "Doc3");

            pathtofile = GetPathToFile() + "Upload3.pdf";
            executionLog.Log("UniqueDocError", "Attach File");
            officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", pathtofile);

            executionLog.Log("UniqueDocError", "Select releted to");
            officeActivities_DocumentHelper.SelectByText("ReletedTo", "Client");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Select Client");
            officeActivities_DocumentHelper.ClickElement("Assign");
            officeActivities_DocumentHelper.WaitForWorkAround(4000);

            officeActivities_DocumentHelper.ClickElement("AssignUser");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Click on Save");
            officeActivities_DocumentHelper.ClickElement("Save");

            executionLog.Log("UniqueDocError", "Wait for success message.");
            officeActivities_DocumentHelper.WaitForText("Document saved successfully.", 10);

            executionLog.Log("UniqueDocError", "Go to client page");
            VisitOffice("clients");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Verify title");
            VerifyTitle();
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Open the client");
            office_ClientsHelper.ClickElement("Client1");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Click on info");
            office_ClientsHelper.ClickElement("Info");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

            executionLog.Log("UniqueDocError", "Verify title");
            VerifyTitle("- Details");
            officeActivities_DocumentHelper.WaitForWorkAround(2000);

        }
         catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("UniqueDocError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Unique Doc Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Unique Doc Error", "Bug", "Medium", "Documents page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Unique Doc Error");
                        TakeScreenshot("UniqueDocError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UniqueDocError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("UniqueDocError");
                        string id = loginHelper.getIssueID("Unique Doc Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\UniqueDocError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Unique Doc Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Unique Doc Error");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("UniqueDocError");
                executionLog.WriteInExcel("Unique Doc Error", Status, JIRA, "Office Activities");
            }
        }
    }
}
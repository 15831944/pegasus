using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;


namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class DeleteDraft : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void deleteDraft()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            String JIRA = "";
            String Status = "Pass";

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_EmailsHelper = new OfficeActivities_EmailsHelper(GetWebDriver());

            try
            {
            executionLog.Log("DeleteDraft", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("DeleteDraft", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("DeleteDraft", "Visit to Compose page");
            VisitOffice("mails/compose");
            officeActivities_EmailsHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteDraft", "Verify title");
            VerifyTitle("Compose");

            executionLog.Log("DeleteDraft", "Enter TO field");
            officeActivities_EmailsHelper.TypeText("To", "Test" + RandomNumber(1, 9999) + "@yopmail.com");

            executionLog.Log("DeleteDraft", "Enter subject");
            officeActivities_EmailsHelper.TypeText("Subject", "Subject" + RandomNumber(1, 9999));

            executionLog.Log("DeleteDraft", "Click on Draft button");
            officeActivities_EmailsHelper.ClickElement("Draft");

            executionLog.Log("DeleteDraft", "Wait for text");
            officeActivities_EmailsHelper.WaitForText("E-Mail Drafted Successfully.", 10);

            executionLog.Log("DeleteDraft", "Redirect at drafts page.");
            VisitOffice("mails/draft");
            officeActivities_EmailsHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteDraft", "Verify title");
            VerifyTitle("Draft");

            executionLog.Log("DeleteDraft", "Select First draft");
            officeActivities_EmailsHelper.ClickElement("Draft1");
            officeActivities_EmailsHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteDraft", "Click on Delete button");
            officeActivities_EmailsHelper.ClickElement("DeleteDraft");
            officeActivities_EmailsHelper.WaitForWorkAround(3000);

            executionLog.Log("DeleteDraft", "Verify draft deleted successfully");
            officeActivities_EmailsHelper.WaitForText("E-Mail has been moved to the Recycle Bin.", 10);

            executionLog.Log("DeleteDraft", "Log out from the application");
            VisitOffice("logout");

        }
     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("DeleteDraft");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Delete Draft");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete Draft", "Bug", "Medium", "Draft page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Delete Draft");
                        TakeScreenshot("DeleteDraft");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteDraft.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("DeleteDraft");
                        string id = loginHelper.getIssueID("Delete Draft");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\DeleteDraft.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Delete Draft"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Delete Draft");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("DeleteDraft");
                executionLog.WriteInExcel("Delete Draft", Status, JIRA, "Activity Email");
            }
        }
    }
} 
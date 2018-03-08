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
    public class VerifyRedirectedToEmailPageIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyRedirectedToEmailPageIssue()
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

            // Random Variable
            String JIRA = "";
            String Status = "Pass";
            var docname = "Testdoc" + RandomNumber(111,9999);

            try
            {
                executionLog.Log("VerifyRedirectedToEmailPageIssue", " Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Redirect to Create Document page");
                VisitOffice("documents/create");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Enter document name");
                officeActivities_DocumentsHelper.TypeText("Name",docname);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Upload document");
                officeActivities_DocumentsHelper.UploadFile("//*[@id='DocumentFile']", "C:/Users/Public/Pictures/Sample Pictures/Desert.jpg");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Select User Group");
                officeActivities_DocumentsHelper.SelectByText("UserGroup", "Secondary");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Click on Save button");
                officeActivities_DocumentsHelper.ClickElement("Save");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Enter document name in Search field");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", docname);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Open document");
                officeActivities_DocumentsHelper.ClickElement("OpenDoc");
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Click on User Group");
                officeActivities_DocumentsHelper.ClickElement("AssgnUserGroup");
                officeActivities_DocumentsHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Verify User not directed to Compose Email page");
                officeActivities_DocumentsHelper.VerifyTextNotAvailable("E-Mails");
                Console.WriteLine("User is not redirected to Compose Email page");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Redirect to All Document page");
                VisitOffice("documents");
                officeActivities_DocumentsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Enter document name in Search field");
                officeActivities_DocumentsHelper.TypeText("SearchDocumet", docname);
                officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Select document");
                officeActivities_DocumentsHelper.ClickElement("ClickOnCheckBox");

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Click on Delete button");
                officeActivities_DocumentsHelper.ClickElement("ClickOndelete");
                //officeActivities_DocumentsHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyRedirectedToEmailPageIssue", "Accept Alert");
                officeActivities_DocumentsHelper.AcceptAlert();
                officeActivities_DocumentsHelper.WaitForText("Document deleted successfully.",05);
                Console.WriteLine("Document Deleted");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyRedirectedToEmailPageIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Redirected To Email Page Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Redirected To Email Page Issue", "Bug", "Medium", "Document page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Redirected To Email Page Issue");
                        TakeScreenshot("VerifyRedirectedToEmailPageIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRedirectedToEmailPageIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyRedirectedToEmailPageIssue");
                        string id = loginHelper.getIssueID("Verify Redirected To Email Page Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRedirectedToEmailPageIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Redirected To Email Page Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Redirected To Email Page Issue");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("VerifyRedirectedToEmailPageIssue");
                executionLog.WriteInExcel("Verify Redirected To Email Page Issue", Status, JIRA, "Office Activities");
            }
        }
    }
}
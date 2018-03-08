using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyDocumentsOnEmailAttachment : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyDocumentsOnEmailAttachment()
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
            var officeActivities_DocumentHelper = new OfficeActivities_DocumentHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Redirect at documents page.");
                VisitOffice("documents");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                //officeActivities_DocumentHelper.TypeText("SearchDocumet", "Don't Delete");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Click on any document");
                officeActivities_DocumentHelper.ClickElement("OpenDoc");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyDocumentsOnEmailAttachment", "Wait for locator to present.");
                //officeActivities_DocumentHelper.WaitForElementPresent("EmailThisDocument", 10);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Click on add new version button");
                officeActivities_DocumentHelper.ClickElement("AddVersion");
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Attach File");
                officeActivities_DocumentHelper.UploadFile("//*[@id='DocumentFile']", "E:/pegqa/Pegasus_New_skin/Files/Up.jpg");
                officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Click on add new version button");
                officeActivities_DocumentHelper.TypeText("DocCommnet", "comment");
                //officeActivities_DocumentHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Click on Save button");
                officeActivities_DocumentHelper.ClickElement("AddVersionSave");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Click on email this note.");
                officeActivities_DocumentHelper.ClickElement("EmailThisDocument");
                officeActivities_DocumentHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyDocumentsOnEmailAttachment", "Verify attached file is pdf file");
                officeActivities_DocumentHelper.VerifyText("VerifyEmailAttachment", "Up.jpg");
                //officeActivities_DocumentHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDocumentsOnEmailAttachment");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Documents On Email Attachment");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Documents On Email Attachment", "Bug", "Medium", "Documents page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Documents On Email Attachment");
                        TakeScreenshot("VerifyDocumentsOnEmailAttachment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentsOnEmailAttachment.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDocumentsOnEmailAttachment");
                        string id = loginHelper.getIssueID("Verify Documents On Email Attachment");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDocumentsOnEmailAttachment.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Documents On Email Attachment"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Documents On Email Attachment");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDocumentsOnEmailAttachment");
                executionLog.WriteInExcel("Verify Documents On Email Attachment", Status, JIRA, "Activities Management");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SendEmailFromActivity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Temp")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void sendEmailFromActivity()
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
            var officeActivities_EmailHelper = new OfficeActivities_EmailsHelper(GetWebDriver());
            var SendTo = "Test" + RandomNumber(1, 99) + "@yopmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SendEmailFromActivity", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SendEmailFromActivity", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SendEmailFromActivity", "Redirect to URL");
                VisitOffice("mails/compose");
                officeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("SendEmailFromActivity", "Enter email in 'To' Field");
                officeActivities_EmailHelper.TypeText("To", SendTo);

                executionLog.Log("SendEmailFromActivity", "Enter CC");
                officeActivities_EmailHelper.TypeText("EnterCCMuliple", "Test@yopmail.com,123@yopmail.com,mytest@yopmail.com");
                //officeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("SendEmailFromActivity", "Enter Subject ");
                officeActivities_EmailHelper.TypeText("Subject", "This is Subject");
                //officeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("SendEmailFromActivity", "Click send Button");
                officeActivities_EmailHelper.ClickElement("ClickSendBtn");

                executionLog.Log("SendEmailFromActivity", "Verify Email sent successfully");
                officeActivities_EmailHelper.WaitForText("E-Mail Sent Successfully.", 15);

                executionLog.Log("SendEmailFromActivity", "Redirect to URL");
                VisitOffice("mails/sent");
                officeActivities_EmailHelper.WaitForWorkAround(3000);

                executionLog.Log("SendEmailFromActivity", "Verify page title");
                VerifyTitle("Sent");

                executionLog.Log("SendEmailFromActivity", "Enter Send to in search ");
                officeActivities_EmailHelper.TypeText("SearchMailInput", SendTo);

                executionLog.Log("SendEmailFromActivity", "Click on search btn");
                officeActivities_EmailHelper.ClickElement("SearchBtn");
                officeActivities_EmailHelper.WaitForWorkAround(2000);

                executionLog.Log("SendEmailFromActivity", "Select searched email.");
                officeActivities_EmailHelper.ClickElement("CheckBox1");

                executionLog.Log("SendEmailFromActivity", "Click on delete btn");
                officeActivities_EmailHelper.ClickElement("Delete");
                officeActivities_EmailHelper.WaitForWorkAround(1000);

                executionLog.Log("SendEmailFromActivity", "Verify Email Deleted successfully");
                officeActivities_EmailHelper.WaitForText("E-Mail has been moved to the Recycle Bin.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SendEmailFromActivity");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Send Email From Activity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Send Email From Activity", "Bug", "Medium", "Mail page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Send Email From Activity");
                        TakeScreenshot("SendEmailFromActivity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendEmailFromActivity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SendEmailFromActivity");
                        string id = loginHelper.getIssueID("Send Email From Activity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendEmailFromActivity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Send Email From Activity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Send Email From Activity");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SendEmailFromActivity");
                executionLog.WriteInExcel("Send Email From Activity", Status, JIRA, "Office Activities");
            }
        }
    }
}
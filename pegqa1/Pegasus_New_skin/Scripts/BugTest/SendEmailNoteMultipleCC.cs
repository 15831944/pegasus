using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SendEmailNoteMultipleCC : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void sendEmailNoteMultipleCC()
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
            var officeActivities_NotesHelper = new OfficeActivities_NotesHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SendEmailNoteMultipleCC", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SendEmailNoteMultipleCC", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("SendEmailNoteMultipleCC", "Redirect to URL Create Notes");
                VisitOffice("notes/create");
                officeActivities_NotesHelper.WaitForWorkAround(5000);

                executionLog.Log("SendEmailNoteMultipleCC", "Enter Subject ");
                officeActivities_NotesHelper.TypeText("Subject", "Email This Note");

                executionLog.Log("SendEmailNoteMultipleCC", "Click on Save");
                officeActivities_NotesHelper.ClickElement("Save");

                executionLog.Log("SendEmailNoteMultipleCC", "Verify Text");
                officeActivities_NotesHelper.WaitForText("Note saved successfully.", 10);

                executionLog.Log("SendEmailNoteMultipleCC", "Search Note");
                officeActivities_NotesHelper.TypeText("EnterSubject", "Email This Note");

                executionLog.Log("SendEmailNoteMultipleCC", "Click on Note");
                officeActivities_NotesHelper.ClickElement("ClickOnNote");

                executionLog.Log("SendEmailNoteMultipleCC", "Wait for element to present.");
                officeActivities_NotesHelper.WaitForElementPresent("ClickOnEmail", 10);

                executionLog.Log("SendEmailNoteMultipleCC", "Click On Email This NOte");
                officeActivities_NotesHelper.ClickElement("ClickOnEmail");

                executionLog.Log("SendEmailNoteMultipleCC", "Wait for element to present.");
                officeActivities_NotesHelper.WaitForElementPresent("EnterToFiled", 10);

                executionLog.Log("SendEmailNoteMultipleCC", "Enter To Field");
                officeActivities_NotesHelper.TypeText("EnterToFiled", "Test@yopmail.com");

                executionLog.Log("SendEmailNoteMultipleCC", "Enter CC");
                officeActivities_NotesHelper.TypeText("EnterCCMuliple", "Test@yopmail.com,123@yopmail.com,mytest@yopmail.com");

                executionLog.Log("SendEmailNoteMultipleCC", "Click send Button");
                officeActivities_NotesHelper.ClickElement("ClickSendBtn");

                executionLog.Log("SendEmailNoteMultipleCC", "Verify Email sent successfully");
                officeActivities_NotesHelper.WaitForText("E-Mail Sent Successfully.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SendEmailNoteMultipleCC");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Send Email Note Multiple CC");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Send Email Note Multiple CC", "Bug", "Medium", "Mail page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Send Email Note Multiple CC");
                        TakeScreenshot("SendEmailNoteMultipleCC");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendEmailNoteMultipleCC.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SendEmailNoteMultipleCC");
                        string id = loginHelper.getIssueID("Send Email Note Multiple CC");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SendEmailNoteMultipleCC.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Send Email Note Multiple CC"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Send Email Note Multiple CC");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SendEmailNoteMultipleCC");
                executionLog.WriteInExcel("Send Email Note Multiple CC", Status, JIRA, "Office Activities");
            }
        }
    }
}
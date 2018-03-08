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
    public class EditMeetingVerfiyValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editMeetingVerfiyValidation()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Random Variables
            var ExeFile = GetPathToFile() + "chrome.exe";
            Console.WriteLine("Path is " + ExeFile);
            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("EditMeetingVerfiyValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditMeetingVerfiyValidation", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditMeetingVerfiyValidation", "Click on Activities >> Meetings");
                VisitOffice("meetings");

                executionLog.Log("EditMeetingVerfiyValidation", "Click on Edit meeting icon");
                officeActivities_MeetingHelper.ClickElement("Edit");

                executionLog.Log("EditMeetingVerfiyValidation", "Click on Add Attachment");
                officeActivities_MeetingHelper.ClickElement("AddAttachment");
                officeActivities_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("EditMeetingVerfiyValidation", "Click on Add Attachment");
                officeActivities_MeetingHelper.TypeText("EnterDocumentName", "Test");
                officeActivities_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("EditMeetingVerfiyValidation", "Upload an invalid file.");
                officeActivities_MeetingHelper.Upload("File_Upload", ExeFile);
                officeActivities_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("EditMeetingVerfiyValidation", "Click on Add Attachment");
                officeActivities_MeetingHelper.ClickElement("SaveAttachment");

                executionLog.Log("EditMeetingVerfiyValidation", "Verify mandatory text on the page.");
                officeActivities_MeetingHelper.VerifyText("Attachmenterror", "This field is required.");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditMeetingVerfiyValidation");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Meeting Verfiy Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Meeting Verfiy Validation", "Bug", "Medium", "Meetings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Meeting Verfiy Validation");
                        TakeScreenshot("EditMeetingVerfiyValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingVerfiyValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditMeetingVerfiyValidation");
                        string id = loginHelper.getIssueID("Edit Meeting Verfiy Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingVerfiyValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Meeting Verfiy Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Meeting Verfiy Validation");
          //      executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditMeetingVerfiyValidation");
                executionLog.WriteInExcel("Edit Meeting Verfiy Validation", Status, JIRA, "Office Activities");
            }
        }
    }
}
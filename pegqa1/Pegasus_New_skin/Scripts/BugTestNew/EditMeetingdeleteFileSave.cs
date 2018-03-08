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
    public class EditMeetingdeleteFileSave : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void editMeetingdeleteFileSave()
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

            // Variable
            var Filename = GetPathToFile() + "index.jpg";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditMeetingdeleteFileSave", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditMeetingdeleteFileSave", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditMeetingdeleteFileSave", "Click On Admin");
                VisitOffice("admin");

                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("EditMeetingdeleteFileSave", "Verify title");
                VerifyTitle("Meetings");

                executionLog.Log("EditMeetingdeleteFileSave", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("EditMeetingdeleteFileSave", "Verify page title.");
                VerifyTitle("Edit Meeting");

                executionLog.Log("EditMeetingdeleteFileSave", "Click on add attachment");
                officeActivities_MeetingHelper.ClickElement("AddAttachment");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditMeetingdeleteFileSave", "Upload File ");
                officeActivities_MeetingHelper.Upload("File_Upload", Filename);

                executionLog.Log("EditMeetingdeleteFileSave", "Enter attachment name.");
                officeActivities_MeetingHelper.TypeText("AttachName", "New Subject");

                executionLog.Log("EditMeetingdeleteFileSave", "Remove the uploaded file.");
                officeActivities_MeetingHelper.ClickElement("RemoveAttach");

                executionLog.Log("EditMeetingdeleteFileSave", "Click on save button.");
                officeActivities_MeetingHelper.ClickElement("SaveAttachment");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("EditMeetingdeleteFileSave", "Verify validation text.");
                officeActivities_MeetingHelper.VerifyText("Attachmenterror", "This field is required.");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditMeetingdeleteFileSave");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Meeting delete File Save");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Meeting delete File Save", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Meeting delete File Save");
                        TakeScreenshot("EditMeetingdeleteFileSave");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingdeleteFileSave.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditMeetingdeleteFileSave");
                        string id = loginHelper.getIssueID("Edit Meeting delete File Save");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditMeetingdeleteFileSave.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Meeting delete File Save"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Meeting delete File Save");
            //    executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditMeetingdeleteFileSave");
                executionLog.WriteInExcel("Edit Meeting delete File Save", Status, JIRA, "Office Activities");
            }
        }
    }
}
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
    public class EditNewMeeting : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin")]
        [TestCategory("Temp")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void editNewMeeting()
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
            var name = "Subject" + RandomNumber(1, 9999);
            var EditName = "EditSubject" + RandomNumber(1, 500);
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditNewMeeting", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditNewMeeting", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditNewMeeting", " Go to meeting");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Verify title");
                VerifyTitle("Create a Meeting");

                executionLog.Log("EditNewMeeting", "Enter Subject");
                officeActivities_MeetingHelper.TypeText("Subject", name);
                Console.WriteLine("name is   " + name);

                executionLog.Log("EditNewMeeting", "Enter Meeting location");
                officeActivities_MeetingHelper.TypeText("Location", "Test Meeting");

                executionLog.Log("EditNewMeeting", "Select Priority");
                officeActivities_MeetingHelper.Select("Priority", "Low");

                executionLog.Log("EditNewMeeting", "select Module");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("EditNewMeeting", "Click on Assing");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Select a client");
                officeActivities_MeetingHelper.ClickElement("SelectedClient");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Enter start date");
                officeActivities_MeetingHelper.TypeText("StartDate", "07/07/2018");

                executionLog.Log("EditNewMeeting", "Enter Due date");
                officeActivities_MeetingHelper.TypeText("EndDate", "08/08/2018");

                executionLog.Log("EditNewMeeting", "cLICK on Save  ");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Wait for text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("EditNewMeeting", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Verify title");
                VerifyTitle("Meetings");

                executionLog.Log("EditNewMeeting", "Enter Subject in Search field");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Select all in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Click on Edit");
                officeActivities_MeetingHelper.ClickElement("Edit");
                VerifyTitle("Edit Meeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Enter Subject");
                officeActivities_MeetingHelper.TypeText("Subject", EditName);

                executionLog.Log("EditNewMeeting", "Edit Start Date");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2018");

                executionLog.Log("EditNewMeeting", "Click On Save Btn");
                officeActivities_MeetingHelper.ClickElement("Save");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", EditName);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Select all in owner field");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Click On Cancel meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("EditNewMeeting", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("EditNewMeeting", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", EditName);
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Select all in owner field");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("EditNewMeeting", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);

                executionLog.Log("EditNewMeeting", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");

                executionLog.Log("EditNewMeeting", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("EditNewMeeting", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditNewMeeting");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit New Meeting");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit New Meeting", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit New Meeting");
                        TakeScreenshot("EditNewMeeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNewMeeting.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditNewMeeting");
                        string id = loginHelper.getIssueID("Edit New Meeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditNewMeeting.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit New Meeting"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit New Meeting");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("EditNewMeeting");
                executionLog.WriteInExcel("Edit New Meeting", Status, JIRA, "Tasks and Meetings");
            }
        }
    }
}

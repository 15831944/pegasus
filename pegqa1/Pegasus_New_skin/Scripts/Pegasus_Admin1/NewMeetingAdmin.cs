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
    public class NewMeetingAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void newMeetingAdmin()
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
            var name = "Testing Subject" + RandomNumber(1, 50);
            var email = "Test" + RandomNumber(1, 99) + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("NewMeetingAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("NewMeetingAdmin", "Verify Page title");
                VerifyTitle("Dashboard");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "Click On  Admin");
                VisitOffice("admin");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("NewMeetingAdmin", " Click On Create");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Verify Page title");
                VerifyTitle("Create a Meeting");
                //officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "Enter Subject");
                officeActivities_MeetingHelper.TypeText("Subject", name);

                executionLog.Log("NewMeetingAdmin", "Enter Meeting location");
                officeActivities_MeetingHelper.TypeText("Location", "Test Meeting");

                executionLog.Log("NewMeetingAdmin", "Enter date");
                officeActivities_MeetingHelper.TypeText("StartDate", "08/08/2016");

                executionLog.Log("NewMeetingAdmin", "Due date");
                officeActivities_MeetingHelper.TypeText("EndDate", "09/09/2016");

                executionLog.Log("NewMeetingAdmin", "Select releted to");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("NewMeetingAdmin", "Click on assign");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "Select releted to client");
                officeActivities_MeetingHelper.ClickElement("SelectedClient");

                executionLog.Log("NewMeetingAdmin", "Select assigned owner");
                officeActivities_MeetingHelper.SelectByText("AssignedOwner", "Howard Tang");

                executionLog.Log("NewMeetingAdmin", "Select user");
                officeActivities_MeetingHelper.ClickElement("Priority");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "cLICK on Save  ");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("NewMeetingAdmin", "Verify text");
                officeActivities_MeetingHelper.WaitForText("Meeting saved successfully.", 10);

                executionLog.Log("NewMeetingAdmin", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "Search meeting by subject");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Select All in owner");
                officeActivities_MeetingHelper.SelectByText("Owner", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("NewMeetingAdmin", "Click on the meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

                executionLog.Log("NewMeetingAdmin", "Click On Cance meeting.");
                officeActivities_MeetingHelper.ClickElement("CancelMeeting");
                officeActivities_MeetingHelper.AcceptAlert();

                executionLog.Log("NewMeetingAdmin", "Redirect at recycle bin.");
                VisitOffice("meetings/recyclebin");
                officeActivities_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("NewMeetingAdmin", "Verify page title");
                VerifyTitle("Recycled Meeting");

                executionLog.Log("NewMeetingAdmin", "Search meeting by name.");
                officeActivities_MeetingHelper.TypeText("SearchSubject", name);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Select All in owner");
                officeActivities_MeetingHelper.SelectByText("OwnerField", "All");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Wait for delete icon to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("DeleteMeetingPermanently", 10);
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Click On delete icon");
                officeActivities_MeetingHelper.ClickElement("DeleteMeetingPermanently");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Accept alert message.");
                officeActivities_MeetingHelper.AcceptAlert();
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("NewMeetingAdmin", "Verify text.");
                officeActivities_MeetingHelper.WaitForText("Meeting Permanently Deleted.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("NewMeetingAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("New Meeting Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("New Meeting Admin", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("New Meeting Admin");
                        TakeScreenshot("NewMeetingAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NewMeetingAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("NewMeetingAdmin");
                        string id = loginHelper.getIssueID("New Meeting Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\NewMeetingAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("New Meeting Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("New Meeting Admin");
             //   executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("NewMeetingAdmin");
                executionLog.WriteInExcel("New Meeting Admin", Status, JIRA, "Office Activities");
            }
        }
    }
}
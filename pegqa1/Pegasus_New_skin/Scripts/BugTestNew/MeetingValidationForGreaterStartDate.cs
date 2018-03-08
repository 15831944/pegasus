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
    public class MeetingValidationForGreaterStartDate : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void meetingValidationForGreaterStartDate()
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

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("MeetingValidationForGreaterStartDate", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Click on Activities >> Meetings");
                VisitOffice("meetings/create");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.TypeText("Subject", "Test Subject");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.TypeText("StartDate", "2015-12-25");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.TypeText("EndDate", "2015-12-09");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.SelectByText("RelatedTo", "Client");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.ClickElement("FindListIcon");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.ClickElement("SelectClient");

                executionLog.Log("OpportunityGroupWithBlankName", "Wait for locator to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("Save", 10);

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.ClickElement("Save");

                executionLog.Log("MeetingValidationForGreaterStartDate", "Verify Page title");
                officeActivities_MeetingHelper.VerifyAlertText("Start Date & Time should lesser than or equal to Due Date & Time");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("MeetingValidationForGreaterStartDate");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Meeting Validation For Greater Start Date");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Meeting Validation For Greater Start Date", "Bug", "Medium", "Meetings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Meeting Validation For Greater Start Date");
                        TakeScreenshot("MeetingValidationForGreaterStartDate");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingValidationForGreaterStartDate.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("MeetingValidationForGreaterStartDate");
                        string id = loginHelper.getIssueID("Meeting Validation For Greater Start Date");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\MeetingValidationForGreaterStartDate.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Meeting Validation For Greater Start Date"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Meeting Validation For Greater Start Date");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("MeetingValidationForGreaterStartDate");
                executionLog.WriteInExcel("Meeting Validation For Greater Start Date", Status, JIRA, "Office Activities");
            }
        }
    }
}
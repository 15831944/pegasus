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
    public class ChangeURLofMeeting : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void changeURLofMeeting()
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

                executionLog.Log("ChangeURLofMeeting", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("ChangeURLofMeeting", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("ChangeURLofMeeting", "Activeities >> Meeting");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("ChangeURLofMeeting", "Click on Recycle bin");
                officeActivities_MeetingHelper.ClickElement("ClickOnRecycleBin");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("ChangeURLofMeeting", "Wait for element to be present.");
                officeActivities_MeetingHelper.WaitForElementPresent("ClickOnSubjectBin", 6);

                executionLog.Log("ChangeURLofMeeting", "Click on first meeting");
                officeActivities_MeetingHelper.ClickElement("ClickOnSubjectBin");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("ChangeURLofMeeting", "Change url of the meeting.");
                VisitOffice("meetings/view/21402");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ChangeURLofMeeting");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Change URL of Meeting");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Change URL of Meeting", "Bug", "Medium", "Office activities", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Change URL of Meeting");
                        TakeScreenshot("ChangeURLofMeeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ChangeURLofMeeting.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ChangeURLofMeeting");
                        string id = loginHelper.getIssueID("Change URL of Meeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ChangeURLofMeeting.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Change URL of Meeting"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Change URL of Meeting");
                //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ChangeURLofMeeting");
                executionLog.WriteInExcel("Change URL of Meeting", Status, JIRA, "Office Activities");
            }
        }
    }
}
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
    public class VerifyLabelSelectForMeeting : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyLabelSelectForMeeting()
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

                executionLog.Log("VerifyLabelSelectForMeeting", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyLabelSelectForMeeting", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyLabelSelectForMeeting", "Click on Activities >> meetings");
                VisitOffice("meetings");

                executionLog.Log("VerifyLabelSelectForMeeting", "Click on any Task");
                officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");

                executionLog.Log("VerifyLabelSelectForMeeting", "Verify Select for category");
                officeActivities_MeetingHelper.VerifyText("CategoryLabel", "Select");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyLabelSelectForMeeting");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Label Select For Meeting");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Label Select For Meeting", "Bug", "Medium", "Meetings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Label Select For Meeting");
                        TakeScreenshot("VerifyLabelSelectForMeeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLabelSelectForMeeting.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyLabelSelectForMeeting");
                        string id = loginHelper.getIssueID("Verify Label Select For Meeting");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyLabelSelectForMeeting.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Label Select For Meeting"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Label Select For Meeting");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyLabelSelectForMeeting");
                executionLog.WriteInExcel("Verify Label Select For Meeting", Status, JIRA, "Office Activities");
            }
        }
    }
}
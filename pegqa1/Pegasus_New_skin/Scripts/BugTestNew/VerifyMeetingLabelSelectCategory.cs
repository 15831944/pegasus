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
    public class VerifyMeetingLabelSelectCategory : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Bug")]
        public void verifyMeetingLabelSelectCategory()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Random Variables
            String JIRA = "";
            String Status = "Pass";

            //          try
            //        {
            executionLog.Log("VerifyMeetingLabelSelectCategory", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Click on Activities >> Meetings");
            VisitOffice("meetings");

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Verify Page title meetings");
            VerifyTitle("Meetings");

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Click on any meeting");
            officeActivities_MeetingHelper.ClickElement("ClickOnAnyMeeting");
            officeActivities_MeetingHelper.WaitForWorkAround(1000);

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Click on category to edit category.");
            officeActivities_MeetingHelper.DblClick("CategoryLabel");
         
            executionLog.Log("VerifyMeetingLabelSelectCategory", "Click on save button.");
            officeActivities_MeetingHelper.ClickElement("SaveCategory");      

            executionLog.Log("VerifyMeetingLabelSelectCategory", "Verify category label text select category.");
            officeActivities_MeetingHelper.VerifyText("CategoryLabel", "Select Category");

        }
    } }
      /*      catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMeetingLabelSelectCategory");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Meeting Label Select Category");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Meeting Label Select Category", "Bug", "Medium", "Meetings page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Meeting Label Select Category");
                        TakeScreenshot("VerifyMeetingLabelSelectCategory");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingLabelSelectCategory.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMeetingLabelSelectCategory");
                        string id = loginHelper.getIssueID("Verify Meeting Label Select Category");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingLabelSelectCategory.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Meeting Label Select Category"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Meeting Label Select Category");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMeetingLabelSelectCategory");
                executionLog.WriteInExcel("Verify Meeting Label Select Category", Status, JIRA, "Activities Meeting");
            }
        }
    }
}*/
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
    public class CorpMeetingDateLabelVerify : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void corpMeetingDateLabelVerify()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_Office_MeetingHelper = new Corp_Office_MeetingHelper(GetWebDriver());

            // Variable random
            var name = "Test" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CorpMeetingDateLabelVerify", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CorpMeetingDateLabelVerify", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("CorpMeetingDateLabelVerify", "Redirect To Office");
                VisitCorp("offices");
                corp_Office_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Click on Any Office");
                corp_Office_MeetingHelper.ClickElement("ClickOnAnyOffice");
                corp_Office_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Click On Meeting");
                corp_Office_MeetingHelper.ClickElement("NewMeeting");
                corp_Office_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Enter Subject");
                corp_Office_MeetingHelper.TypeText("Subject", "Meeting Test");

                executionLog.Log("CorpMeetingDateLabelVerify", "Enter Start Date");
                corp_Office_MeetingHelper.TypeText("StartDate", "2015-10-15");

                executionLog.Log("CorpMeetingDateLabelVerify", "End/Due Date");
                corp_Office_MeetingHelper.TypeText("EndDate", "2015-10-26");

                executionLog.Log("CorpMeetingDateLabelVerify", "Click SAVE");
                corp_Office_MeetingHelper.ClickDisplayed("//*[@id='CreateMeetingForm']//a[@title='Save']");
                corp_Office_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Wait for confirmation message.");
                corp_Office_MeetingHelper.WaitForText("Meeting Created Successfully.", 5);

                executionLog.Log("CorpMeetingDateLabelVerify", "Select Activities Meeting");
                corp_Office_MeetingHelper.Select("ActivityType", "Meetings");
                corp_Office_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Click Edit Meeting Icon");
                corp_Office_MeetingHelper.ClickElement("EditMeetingIcon");
                corp_Office_MeetingHelper.WaitForWorkAround(5000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Verify Label");
                corp_Office_MeetingHelper.VerifyText("LabelEndDate", "End date");
                corp_Office_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Click SAVE");
                corp_Office_MeetingHelper.ClickDisplayed("//button[@title='Save']");
                corp_Office_MeetingHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpMeetingDateLabelVerify", "Wait for confirmation message.");
                corp_Office_MeetingHelper.WaitForText("Meeting updated successfully.", 5);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpMeetingDateLabelVerify");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Meeting Date Label Verify");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Meeting Date Label Verify", "Bug", "Medium", "Activities meeting", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Meeting Date Label Verify");
                        TakeScreenshot("CorpMeetingDateLabelVerify");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMeetingDateLabelVerify.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpMeetingDateLabelVerify");
                        string id = loginHelper.getIssueID("Corp Meeting Date Label Verify");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpMeetingDateLabelVerify.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Meeting Date Label Verify"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Meeting Date Label Verify");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpMeetingDateLabelVerify");
                executionLog.WriteInExcel("Corp Meeting Date Label Verify", Status, JIRA, "Office Activities");
            }
        }
    }
}
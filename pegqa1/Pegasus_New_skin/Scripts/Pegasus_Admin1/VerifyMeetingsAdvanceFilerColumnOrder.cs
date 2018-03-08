using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyMeetingsAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyMeetingsAdvanceFilerColumnOrder()
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
            var officeActivities_MeetingHelper = new OfficeActivities_MeetingHelper(GetWebDriver());

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify page title.");
                VerifyTitle("Meetings");

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify status column is visible on the page..");
                officeActivities_MeetingHelper.IsElementPresent("HeadStatus");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify priority column is visible on the page.");
                officeActivities_MeetingHelper.IsElementPresent("HeadPriority");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify owner column is visible on the page.");
                officeActivities_MeetingHelper.IsElementPresent("HeadOwner");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify created column is visible on the page.");
                officeActivities_MeetingHelper.IsElementPresent("HeadCreated");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select status in displayed columns.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Status");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                officeActivities_MeetingHelper.ClickElement("RemoveCols");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select priority in displayed columns.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Priority");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_MeetingHelper.ClickElement("RemoveCols");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select owner in displayed columns.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Owner");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_MeetingHelper.ClickElement("RemoveCols");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select created in displayed columns.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Created");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                officeActivities_MeetingHelper.ClickElement("RemoveCols");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify status not present on page.");
                officeActivities_MeetingHelper.IsElementNotPresent("HeadStatus");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify priority not present on page.");
                officeActivities_MeetingHelper.IsElementNotPresent("HeadPriority");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify owner not present on page.");
                officeActivities_MeetingHelper.IsElementNotPresent("HeadOwner");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify created not present on page.");
                officeActivities_MeetingHelper.IsElementNotPresent("HeadCreated");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                officeActivities_MeetingHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify page title as meetings");
                VerifyTitle("Meetings");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify default status of phone column.");
                officeActivities_MeetingHelper.IsElementPresent("HeadStatus6");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify default position of prority column.");
                officeActivities_MeetingHelper.IsElementPresent("HeadPriority7");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Redirect at meetings page.");
                VisitOffice("meetings");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click on advance filter.");
                officeActivities_MeetingHelper.ClickElement("AdvanceFilter");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select status in displayed column.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Status");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                officeActivities_MeetingHelper.ClickElement("MoveUp");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                officeActivities_MeetingHelper.ClickElement("MoveUp");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Move phone 1 step up.");
                officeActivities_MeetingHelper.ClickElement("MoveUp");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Select priority in displayed column.");
                officeActivities_MeetingHelper.SelectByText("DisplayedCols", "Priority");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Move priority 1 step down.");
                officeActivities_MeetingHelper.ClickElement("MoveDown");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Click on apply button.");
                officeActivities_MeetingHelper.ClickElement("Apply");
                officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify changed position of status column.");
                officeActivities_MeetingHelper.IsElementPresent("HeadStatus4");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Verify changed position of priority column.");
                officeActivities_MeetingHelper.IsElementPresent("HeadPriority8");
                //officeActivities_MeetingHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyMeetingsAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyMeetingsAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Meetings Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Meetings Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Meetings Advance Filer Column Order");
                        TakeScreenshot("VerifyMeetingsAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyMeetingsAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Meetings Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyMeetingsAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Meetings Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Meetings Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyMeetingsAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Meetings Advance Filer Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}
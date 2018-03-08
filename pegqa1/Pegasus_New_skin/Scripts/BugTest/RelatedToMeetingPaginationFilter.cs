using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class RelatedToMeetingPaginationFilter : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void relatedToMeetingPaginationFilter()
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
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("RelatedToMeetingPaginationFilter", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("RelatedToMeetingPaginationFilter", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("RelatedToMeetingPaginationFilter", "Redirect to create meetings page.");
                VisitOffice("meetings/create");
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

                executionLog.Log("RelatedToMeetingPaginationFilter", "Select Related To");
                officeActivities_MeetingHelper.Select("RelatedTo", "20");

                executionLog.Log("RelatedToMeetingPaginationFilter", "Select client for meeting.");
                officeActivities_MeetingHelper.ClickOnPagination();
                officeActivities_MeetingHelper.WaitForWorkAround(1000);

            }    
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("RelatedToMeetingPaginationFilter");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Related To Meeting Pagination Filter");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Related To Meeting Pagination Filter", "Bug", "Medium", "Meeting page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Related To Meeting Pagination Filter");
                        TakeScreenshot("RelatedToMeetingPaginationFilter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RelatedToMeetingPaginationFilter.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("RelatedToMeetingPaginationFilter");
                        string id = loginHelper.getIssueID("Related To Meeting Pagination Filter");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\RelatedToMeetingPaginationFilter.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Related To Meeting Pagination Filter"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Related To Meeting Pagination Filter");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("RelatedToMeetingPaginationFilter");
                executionLog.WriteInExcel("Related To Meeting Pagination Filter", Status, JIRA, "Office Activities");
            }
        }
    }
}
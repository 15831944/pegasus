using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateReportClientAlertForActivity : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void createReportClientAlertForActivity()
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
            var report_Report_CreateReportHelper = new Reports_CreateReportHelper(GetWebDriver());

            // Variable
            var mail = "Test" + GetRandomNumber() + "@yopmail.com";
            var numb = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("CreateReportClientAlertForActivity", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateReportClientAlertForActivity", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateReportClientAlertForActivity", " Redirect To Craete Report");
                VisitOffice("reports/create");

                executionLog.Log("CreateReportClientAlertForActivity", " Verify page title");
                VerifyTitle("Reports - Create");

                executionLog.Log("CreateReportClientAlertForActivity", "Enter Report name");
                String report = "Test Report" + GetRandomNumber();
                report_Report_CreateReportHelper.TypeText("ReportName", report);

                executionLog.Log("CreateReportClientAlertForActivity", "Select Module");
                report_Report_CreateReportHelper.Select("ReportModule", "20");

                executionLog.Log("CreateReportClientAlertForActivity", "Enter Description ");
                report_Report_CreateReportHelper.TypeText("EnterDEscription", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("CreateReportClientAlertForActivity", "Click to save meeting.");
                report_Report_CreateReportHelper.ClickElement("SaveClientReport");

                executionLog.Log("CreateReportClientAlertForActivity", "Please select atleast one metric");
                report_Report_CreateReportHelper.WaitForText("Please select atleast one metric", 10);

            }
            catch (Exception e)
            {
                 executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateReportClientAlertForActivity");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Report Client Alert For Activity");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Report Client Alert For Activity", "Bug", "Medium", "Reports  page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Report Client Alert For Activity");
                        TakeScreenshot("CreateReportClientAlertForActivity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateReportClientAlertForActivity.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateReportClientAlertForActivity");
                        string id = loginHelper.getIssueID("Create Report Client Alert For Activity");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateReportClientAlertForActivity.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Report Client Alert For Activity"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Report Client Alert For Activity");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateReportClientAlertForActivity");
                executionLog.WriteInExcel("Create Report Client Alert For Activity", Status, JIRA, "Office Reports&DashBoards");
            }
        }
    }
}

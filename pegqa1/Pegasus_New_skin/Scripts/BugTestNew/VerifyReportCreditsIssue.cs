using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyReportCreditsIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyReportCreditsIssue()
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

                executionLog.Log("VerifyReportCreditsIssue", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyReportCreditsIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyReportCreditsIssue", " Redirect To Craete Report");
                VisitOffice("reports");
                report_Report_CreateReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyReportCreditsIssue", " Verify page title");
                VerifyTitle("Reports");
                //report_Report_CreateReportHelper.WaitForWorkAround(3000);

                //executionLog.Log("VerifyReportCreditsIssue", "Search the report");
                //report_Report_CreateReportHelper.TypeText("SearchReport", "aslam report");
                //report_Report_CreateReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyReportCreditsIssue", "Select 'All' in Owner field");
                report_Report_CreateReportHelper.SelectByText("OwnerField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreditsIssue", "Select 'All' in Created By field");
                report_Report_CreateReportHelper.SelectByText("CreatedByField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreditsIssue", "Click on created report.");
                report_Report_CreateReportHelper.ClickElement("Report1");
                report_Report_CreateReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyReportCreditsIssue", "Verify reports Createdby credits");
                report_Report_CreateReportHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreditsIssue", "Verify reports Createdby credits");
                report_Report_CreateReportHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreditsIssue", "Logout from the application.");
                VisitOffice("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyReportCreditsIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Report Credits Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Report Credits Issue", "Bug", "Medium", "Reports  page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Report Credits Issue");
                        TakeScreenshot("VerifyReportCreditsIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyReportCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyReportCreditsIssue");
                        string id = loginHelper.getIssueID("Verify Report Credits Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyReportCreditsIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Report Credits Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Report Credits Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyReportCreditsIssue");
                executionLog.WriteInExcel("Verify Report Credits Issue", Status, JIRA, "Office Reports&DashBoards");
            }
        }
    }
}
using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateReportError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("NewSkin_Task")]
        [TestCategory("All")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createReportError()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var reports_CreateReportHelper = new Reports_CreateReportHelper(GetWebDriver());

            var Report = "Report" + RandomNumber(9, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateReportError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateReportError", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateReportError", "Go to Create report page");
                VisitOffice("reports/create");

                executionLog.Log("CreateReportError", "Verify title");
                VerifyTitle("Reports - Create");

                executionLog.Log("CreateReportError", "Ener name");
                reports_CreateReportHelper.TypeText("ReportName", Report);

                executionLog.Log("CreateReportError", "Select Module");
                reports_CreateReportHelper.SelectByText("ReportModule", "Clients");

                executionLog.Log("CreateReportError", "Click on 'Save' button");
                reports_CreateReportHelper.ClickElement("ReportSave");

                executionLog.Log("CreateReportError", "Wait for 5 second");
                reports_CreateReportHelper.WaitForWorkAround(5000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateReportError");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Report Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Report Error", "Bug", "Medium", "Reports page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Report Error");
                        TakeScreenshot("CreateReportError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateReportError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateReportError");
                        string id = loginHelper.getIssueID("Create Report Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateReportError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Report Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Report Error");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateReportError");
                executionLog.WriteInExcel("Create Report Error", Status, JIRA, "Report");
            }
        }
    }
}
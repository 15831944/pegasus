using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyReportCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyReportCreatedAndModifiedByCredits()
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

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", " Redirect To Craete Report");
                VisitOffice("reports/create");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", " Verify page title");
                VerifyTitle("Reports - Create");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Enter Report name");
                String report = "Test Report" + GetRandomNumber();
                report_Report_CreateReportHelper.TypeText("ReportName", report);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select Module");
                report_Report_CreateReportHelper.Select("ReportModule", "20");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Enter Description ");
                report_Report_CreateReportHelper.TypeText("EnterDEscription", "THIS IS TESTING DESCRIPTION");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click to save meeting.");
                report_Report_CreateReportHelper.ClickElement("SaveClientReport");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Please select atleast one metric");
                report_Report_CreateReportHelper.WaitForText("Please select atleast one metric", 10);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Wait for locator to be present.");
                report_Report_CreateReportHelper.WaitForElementPresent("Activity1", 10);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select a metric for report.");
                report_Report_CreateReportHelper.ClickElement("Activity1");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click to save meeting.");
                report_Report_CreateReportHelper.ClickElement("SaveClientReport");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Wait for success text.");
                report_Report_CreateReportHelper.WaitForText("Report created successfully.", 10);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Wait for locator to present.");
                report_Report_CreateReportHelper.WaitForElementPresent("SearchReport", 10);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Search created report");
                report_Report_CreateReportHelper.TypeText("SearchReport", report);
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select All in owner field");
                report_Report_CreateReportHelper.SelectByText("OwnerField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select All in created by field");
                report_Report_CreateReportHelper.SelectByText("CreatedByField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click on created report.");
                report_Report_CreateReportHelper.ClickElement("Report1");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify reports Createdby credits");
                report_Report_CreateReportHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify reports Createdby credits");
                report_Report_CreateReportHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click on edit button.");
                report_Report_CreateReportHelper.ClickElement("EditButton");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Update report owner.");
                report_Report_CreateReportHelper.SelectByText("Owner", "Howard Tang");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click on save button..");
                report_Report_CreateReportHelper.ClickElement("EditSave");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Wait for updation success text.");
                report_Report_CreateReportHelper.WaitForText("Report modified successfully.", 10);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", " Redirect at reports page.");
                VisitOffice("reports");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify page title as reports.");
                VerifyTitle("Reports");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Search edited report..");
                report_Report_CreateReportHelper.TypeText("SearchReport", report);
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select All in owner field");
                report_Report_CreateReportHelper.SelectByText("OwnerField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Select All in created by field");
                report_Report_CreateReportHelper.SelectByText("CreatedByField", "All");
                report_Report_CreateReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click on any report.");
                report_Report_CreateReportHelper.ClickElement("Report1");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify reports Createdby credits");
                report_Report_CreateReportHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Verify reports modifiedby credits");
                report_Report_CreateReportHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Click on delete button.");
                report_Report_CreateReportHelper.ClickElement("DeleteReport");
                report_Report_CreateReportHelper.AcceptAlert();

                executionLog.Log("VerifyReportCreatedAndModifiedByCredits", "Wait for deletion successs text.");
                report_Report_CreateReportHelper.WaitForText("Report deleted successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyReportCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Report Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Report Created And Modified By Credits", "Bug", "Medium", "Reports  page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Report Created And Modified By Credits");
                        TakeScreenshot("VerifyReportCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyReportCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyReportCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Report Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyReportCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Report Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Report Created And Modified By Credits");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyReportCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Report Created And Modified By Credits", Status, JIRA, "Office Reports&DashBoards");
            }
        }
    }
}

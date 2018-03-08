using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class ExportingResidualIncomePayoutsReport : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]       
        [TestCategory("Fail")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void exportingResidualIncomePayoutsReport()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");


            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corpResidualIncome_PayoutsHelper = new CorpResidualIncome_PayoutsHelper(GetWebDriver());
            var knownFoder = new KnownFolders();
            string downloadsPath = knownFoder.GetPathToX(KnownFolder.Downloads);


            // Get newly created file name from downloads folder
            var newfilename = corpResidualIncome_PayoutsHelper.Getnewfilename(new DirectoryInfo(downloadsPath));
            var filepath = downloadsPath + "\\" + newfilename.ToString();
            String Status = "Pass";
            String JIRA = "";

            try
            {
            executionLog.Log("ExportingResidualIncomePayoutsReport", "Login with valid username and password");
           Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "navigate to the reports page.");
            VisitCorp("rir/reports");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "verify title");
            VerifyTitle("Residual Income - Reports");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Select file date");
            corpResidualIncome_PayoutsHelper.SelectByText("ReportingPeriod", "April 2016");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Select file date");
            corpResidualIncome_PayoutsHelper.SelectByText("FillDate", "All");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Select Processor");
            corpResidualIncome_PayoutsHelper.SelectByText("FillPro", "First Data North");
            corpResidualIncome_PayoutsHelper.WaitForWorkAround(3000);

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Click on search arrow.");
            corpResidualIncome_PayoutsHelper.ClickElement("FSSearch");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Click on fill file date.");
            corpResidualIncome_PayoutsHelper.ClickElement("FillFile");
            corpResidualIncome_PayoutsHelper.WaitForWorkAround(3000);

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Select file date");
            corpResidualIncome_PayoutsHelper.ClickElement("SelectedOffices");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Select file date");
            corpResidualIncome_PayoutsHelper.ClickElement("Office1");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Click on Excel report button");
            corpResidualIncome_PayoutsHelper.ClickElement("ExcelReoprt");

            executionLog.Log("ExportingResidualIncomePayoutsReport", "Check file contains data");
            corpResidualIncome_PayoutsHelper.FileIsNotBlank(filepath);

        }
        catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("ExportingResidualIncomePayoutsReport");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Exporting Residual Income Payouts Report");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Exporting Residual Income Payouts Report", "Bug", "Medium", "Residual income page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Exporting Residual Income Payouts Report");
                        TakeScreenshot("ExportingResidualIncomePayoutsReport");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportingResidualIncomePayoutsReport.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("ExportingResidualIncomePayoutsReport");
                        string id = loginHelper.getIssueID("Exporting Residual Income Payouts Report");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ExportingResidualIncomePayoutsReport.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Exporting Residual Income Payouts Report"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Exporting Residual Income Payouts Report");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("ExportingResidualIncomePayoutsReport");
                executionLog.WriteInExcel("Exporting Residual Income Payouts Report", Status, JIRA, "Residual Adjustment");
            }
        }
    }
}
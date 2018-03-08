using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyAddNewFieldBtnUnderReports : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("BugTestNew")]
        public void verifyAddNewFieldBtnUnderReports()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            //Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var report_ReportHelper = new Report_ReportHelper(GetWebDriver());

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Login with valid credentials");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Go to Create Reports page");
                VisitOffice("reports/create");
                report_ReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Verify page title.");
                VerifyTitle("Reports - Create");

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Select Module >> Opportunities");
                report_ReportHelper.SelectByText("Module", "Opportunities");
                report_ReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Remove all fields of 'Filter by Additional Fields");
                report_ReportHelper.ClickElement("CrossIcon1");
                report_ReportHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Click on Add New Field button");
                report_ReportHelper.ClickElement("AddMoreFieldBtn");
                report_ReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Verify field appeared and button working");
                report_ReportHelper.verifyElementPresent("AddtnlFltrField1");
                Console.WriteLine("Add New Field button is working for Opportunities module");

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Select Module >> Leads");
                report_ReportHelper.SelectByText("Module", "Leads");
                report_ReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Remove all fields of 'Filter by Additional Fields");
                report_ReportHelper.ClickElement("CrossIcon1");
                report_ReportHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Click on Add New Field button");
                report_ReportHelper.ClickElement("AddMoreFieldBtn");
                report_ReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Verify field appeared and button working");
                report_ReportHelper.verifyElementPresent("AddtnlFltrField1");
                Console.WriteLine("Add New Field button is working for Leads module");

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Select Module >> Clients");
                report_ReportHelper.SelectByText("Module", "Clients");
                report_ReportHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Remove all fields of 'Filter by Additional Fields");
                report_ReportHelper.ClickElement("CrossIcon1");
                report_ReportHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Click on Add New Field button");
                report_ReportHelper.ClickElement("AddMoreFieldBtn");
                report_ReportHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAddNewFieldBtnUnderReports", "Verify field appeared and button working");
                report_ReportHelper.verifyElementPresent("AddtnlFltrField1");
                Console.WriteLine("Add New Field button is working for Clients module");
    
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddNewFieldBtnUnderReports");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Add New Field Btn Under Reports");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Add New Field Btn Under Reports", "Bug", "Medium", "Reports page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Add New Field Btn Under Reports");
                        TakeScreenshot("VerifyAddNewFieldBtnUnderReports");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddNewFieldBtnUnderReports.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddNewFieldBtnUnderReports");
                        string id = loginHelper.getIssueID("Verify Add New Field Btn Under Reports");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddNewFieldBtnUnderReports.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Add New Field Btn Under Reports"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Add New Field Btn Under Reports");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddNewFieldBtnUnderReports");
                executionLog.WriteInExcel("Verify Add New Field Btn Under Reports", Status, JIRA, "Office Reports");
            }
        }
    }
} 
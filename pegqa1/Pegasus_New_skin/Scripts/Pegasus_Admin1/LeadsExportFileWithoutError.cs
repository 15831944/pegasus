using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class LeadsExportFileWithoutError : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin1")]
        public void leadsExportFileWithoutError()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_LeadsHelper = new Office_LeadsHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("LeadsExportFileWithoutError", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("LeadsExportFileWithoutError", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("LeadsExportFileWithoutError", "Redirect to leads page");
                VisitOffice("leads");

                executionLog.Log("LeadsExportFileWithoutError", "Verify Title");
                VerifyTitle("Leads");

                executionLog.Log("LeadsExportFileWithoutError", "Export as CSV");
                office_LeadsHelper.ExportAs("CSV");

                executionLog.Log("LeadsExportFileWithoutError", "Verify No error displayed");
                office_LeadsHelper.VerifyTextNotPresent("Some issues occured in this operation, Contact technical support for help");

                executionLog.Log("LeadsExportFileWithoutError", "Export as Excel");
                office_LeadsHelper.ExportAs("Excel");

                executionLog.Log("LeadsExportFileWithoutError", "Verify No error displayed");
                office_LeadsHelper.VerifyTextNotPresent("Some issues occured in this operation, Contact technical support for help");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("LeadsExportFileWithoutError");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Leads Export File Without Error");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Leads Export File Without Error", "Bug", "Medium", "Lead page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Leads Export File Without Error");
                        TakeScreenshot("LeadsExportFileWithoutError");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsExportFileWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("LeadsExportFileWithoutError");
                        string id = loginHelper.getIssueID("Leads Export File Without Error");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\LeadsExportFileWithoutError.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Leads Export File Without Error"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Leads Export File Without Error");
          //      executionLog.DeleteFile("Error");
                throw;
            }
            finally
            {
                executionLog.DeleteFile("LeadsExportFileWithoutError");
                executionLog.WriteInExcel("Leads Export File Without Error", Status, JIRA, "Leads Management");
            }
        }
    }
}
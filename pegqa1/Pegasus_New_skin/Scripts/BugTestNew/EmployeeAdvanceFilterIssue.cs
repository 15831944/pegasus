using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EmployeeAdvanceFilterIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void employeeAdvanceFilterIssue()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EmployeeAdvanceFilterIssue", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("EmployeeAdvanceFilterIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Go to Create employee page");
                VisitCorp("employees");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Click on advance filter");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("EmployeeAdvanceFilterIssue", "Select Phone in Sort By");
                corp_EmployeeHelper.Select("SortBy", "phone");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Click Ascending Radio Button");
                corp_EmployeeHelper.ClickElement("AscendingRadio");

                executionLog.Log("EmployeeAdvanceFilterIssue", "Click Apply Button");
                corp_EmployeeHelper.ClickElement("Apply");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAdvanceFilterIssue", "Verify error message not present");
                corp_EmployeeHelper.VerifyTextNotPresent("Oops.. You are trying to access a non-existent page on the website");
                corp_EmployeeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeAdvanceFilterIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Advance Filter Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Advance Filter Issue", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Advance Filter Issue");
                        TakeScreenshot("EmployeeAdvanceFilterIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAdvanceFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAdvanceFilterIssue");
                        string id = loginHelper.getIssueID("Employee Advance Filter Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAdvanceFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Advance Filter Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Advance Filter Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAdvanceFilterIssue");
                executionLog.WriteInExcel("Employee Advance Filter Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeAgentAdvancedFilterIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void employeeAgentAdvancedFilterIssue()
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
            var agent_EmployeeHelper = new Agents_EmployeesHelper(GetWebDriver());

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Redirect To Agent Employee page");
            VisitOffice("employees");

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Click on Employee Advance filter");
            agent_EmployeeHelper.ClickElement("AdvanceFilter");

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Select results per page");
            agent_EmployeeHelper.Select("ResultsPerPage", "10");

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Click on Apply button");
            agent_EmployeeHelper.ClickElement("ApplyButton");
            agent_EmployeeHelper.WaitForWorkAround(4000);

            executionLog.Log("EmployeeAgentAdvancedFilterIssue", "Verify no. of records at bottom of the page.");
            agent_EmployeeHelper.VerifyText("BottomResults", "Showing 1 - 10");

        }
     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeAgentAdvancedFilterIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Agent Advanced Filter Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Agent Advanced Filter Issue", "Bug", "Medium", "Agent Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Agent Advanced Filter Issue");
                        TakeScreenshot("EmployeeAgentAdvancedFilterIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentAdvancedFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAgentAdvancedFilterIssue");
                        string id = loginHelper.getIssueID("Employee Agent Advanced Filter Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentAdvancedFilterIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Agent Advanced Filter Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Agent Advanced Filter Issue");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAgentAdvancedFilterIssue");
                executionLog.WriteInExcel("Employee Agent Advanced Filter Issue", Status, JIRA, "Agents Portal");
            }
        }
    }
}
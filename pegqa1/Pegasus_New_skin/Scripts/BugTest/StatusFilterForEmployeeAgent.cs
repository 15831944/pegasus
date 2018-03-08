using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class StatusFilterForEmployeeAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void statusFilterForEmployeeAgent()
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

            // Variable
            var FirstName = "Test" + RandomNumber(1, 99);
            var LastName = "Tester" + RandomNumber(1, 99);
            var Number = "12345678" + RandomNumber(10, 99);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("StatusFilterForEmployeeAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("StatusFilterForEmployeeAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("StatusFilterForEmployeeAgent", "Redirect To Employee");
                VisitOffice("employees");

                executionLog.Log("StatusFilterForEmployeeAgent", "Select Status");
                agent_EmployeeHelper.Select("SelectStatus", "Active");
                agent_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("StatusFilterForEmployeeAgent", "Select Status");
                agent_EmployeeHelper.Select("SelectStatus", "Disabled");
                agent_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("StatusFilterForEmployeeAgent", "Select Status");
                agent_EmployeeHelper.Select("SelectStatus", "");
                agent_EmployeeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("StatusFilterForEmployeeAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Status Filter For Employee Agent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Status Filter For Employee Agent", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Status Filter For Employee Agent");
                        TakeScreenshot("StatusFilterForEmployeeAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\StatusFilterForEmployeeAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("StatusFilterForEmployeeAgent");
                        string id = loginHelper.getIssueID("Status Filter For Employee Agent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\StatusFilterForEmployeeAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Status Filter For Employee Agent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Status Filter For Employee Agent");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("StatusFilterForEmployeeAgent");
                executionLog.WriteInExcel("Status Filter For Employee Agent", Status, JIRA, "Agents Portal");
            }
        }
    }
}
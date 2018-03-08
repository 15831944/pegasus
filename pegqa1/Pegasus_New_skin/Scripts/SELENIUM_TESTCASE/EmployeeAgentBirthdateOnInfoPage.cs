using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EmployeeAgentBirthdateOnInfoPage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void employeeAgentBirthdateOnInfoPage()
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
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Goto Opportinuties");
            VisitOffice("employees");

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Open Employee 1");
            agents_EmployeesHelper.ClickElement("EditEmployee1");
            agents_EmployeesHelper.WaitForWorkAround(5000);

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Select Birth Date");
            agents_EmployeesHelper.TypeText("BirthDay", "11/18/1992");
            agents_EmployeesHelper.WaitForWorkAround(3000);

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Click On Save Button");
            agents_EmployeesHelper.clickJS("SaveEmployee");
            agents_EmployeesHelper.WaitForWorkAround(9000);

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Open Employee 1");
            agents_EmployeesHelper.ClickElement("EditEmployee1");
            agents_EmployeesHelper.WaitForWorkAround(5000);

            executionLog.Log("EmployeeAgentBirthdateOnInfoPage", "Verify birthdate on Employee I page.");
            agents_EmployeesHelper.IsElementPresent("//div[contains(text(),'11/18/1992')]");
            agents_EmployeesHelper.WaitForWorkAround(5000);

        }
     catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");                
                String Description = executionLog.GetAllTextFile("EmployeeAgentBirthdateOnInfoPage");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Agent Birthdate On Info Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Agent Birthdate On Info Page", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Agent Birthdate On Info Page");
                        TakeScreenshot("EmployeeAgentBirthdateOnInfoPage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentBirthdateOnInfoPage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAgentBirthdateOnInfoPage");
                        string id = loginHelper.getIssueID("Employee Agent Birthdate On Info Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentBirthdateOnInfoPage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Agent Birthdate On Info Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Agent Birthdate On Info Page");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAgentBirthdateOnInfoPage");
                executionLog.WriteInExcel("Employee Agent Birthdate On Info Page", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeUrlNumberChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void employeeUrlNumberChange()
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
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EmployeeUrlNumberChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmployeeUrlNumberChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmployeeUrlNumberChange", "Goto User Agent >> Employees");
                VisitOffice("employees");
               
                executionLog.Log("EmployeeUrlNumberChange", "Click On Any Emplyee");
                agent_EmployeeHelper.ClickElement("ClikOnEmployeeAgent");
                agent_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("EmployeeUrlNumberChange", "Change url number with the url number of another office");
                VisitOffice("employees/view/211");
                
                executionLog.Log("EmployeeUrlNumberChange", "Verify Validation");
                agent_EmployeeHelper.WaitForText("oops something went wrong" ,10);               

            }
            catch (Exception e)
            {

                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeUrlNumberChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Url Number Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Url Number Change", "Bug", "Medium", "Agent Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Url Number Change");
                        TakeScreenshot("EmployeeUrlNumberChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeUrlNumberChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeUrlNumberChange");
                        string id = loginHelper.getIssueID("Employee Url Number Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeUrlNumberChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Url Number Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Url Number Change");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeUrlNumberChange");
                executionLog.WriteInExcel("Employee Url Number Change", Status, JIRA, "Office Agents");
            }

        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AgentEmployeeAdvanceFilterResultsPP : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS6")]
        [TestCategory("Pegasus_Admin")]
        public void agentEmployeeAdvanceFilterResultsPP()
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
            var DocName = "Test Exe" + GetRandomNumber();
            var fileUpl = GetPathToFile() + "chrome.exe";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Redirect at employee page.");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on advance filter.");
                agents_EmployeesHelper.ClickElement("AdvanceFilter");
                agents_EmployeesHelper.WaitForWorkAround(2000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Select number of records to 10.");
                agents_EmployeesHelper.SelectByText("ResultsPerPage", "10");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on Apply button.");
                agents_EmployeesHelper.ClickElement("ApplyButton");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Verify number of records displayed.");
                //agents_EmployeesHelper.VerifyText("BottomResults", "Showing 1 - 10 of");
                agents_EmployeesHelper.ShowResult(10);
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on advance filter.");
                agents_EmployeesHelper.ClickElement("AdvanceFilter");
                agents_EmployeesHelper.WaitForWorkAround(2000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Select number of records to 20.");
                agents_EmployeesHelper.SelectByText("ResultsPerPage", "20");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on Apply button.");
                agents_EmployeesHelper.ClickElement("ApplyButton");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Verify number of records displayed.");
                // agents_EmployeesHelper.VerifyText("BottomResults", "Showing 1 - 20 of");
                agents_EmployeesHelper.ShowResult(20);
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on advance filter.");
                //agents_EmployeesHelper.ClickElement("AdvanceFilter");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Select number of records to 50.");
                //agents_EmployeesHelper.SelectByText("ResultsPerPage", "50");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on ApplyButton button.");
                //agents_EmployeesHelper.ClickElement("ApplyButton");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Verify number of records displayed.");
                //// agents_EmployeesHelper.VerifyText("BottomResults", "Showing 1 - 50 of");
                //agents_EmployeesHelper.ShowResult(50);
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on advance filter.");
                //agents_EmployeesHelper.ClickElement("AdvanceFilter");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Select number of records to 100.");
                //agents_EmployeesHelper.SelectByText("ResultsPerPage", "100");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Click on ApplyButton button.");
                //agents_EmployeesHelper.ClickElement("ApplyButton");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                //executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Verify number of records displayed.");
                ////agents_EmployeesHelper.VerifyText("BottomResults", "Showing 1 - 100 of");
                //agents_EmployeesHelper.ShowResult(100);
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeeAdvanceFilterResultsPP", "Logout from the application.");
                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AgentEmployeeAdvanceFilterResultsPP");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Agent Employee Advance Filter ResultsPP");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Agent Employee Advance Filter ResultsPP", "Bug", "Medium", "Opportunities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Agent Employee Advance Filter ResultsPP");
                        TakeScreenshot("AgentEmployeeAdvanceFilterResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentEmployeeAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AgentEmployeeAdvanceFilterResultsPP");
                        string id = loginHelper.getIssueID("Agent Employee Advance Filter ResultsPP");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentEmployeeAdvanceFilterResultsPP.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Agent Employee Advance Filter ResultsPP"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Agent Employee Advance Filter ResultsPP");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AgentEmployeeAdvanceFilterResultsPP");
                executionLog.WriteInExcel("Agent Employee Advance Filter ResultsPP", Status, JIRA, "Opportunities Management");
            }
        }
    }
}
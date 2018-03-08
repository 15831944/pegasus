using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyAgentEmployeeAdvanceFilerColumnOrder : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Admin1")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Admin1")]
        public void verifyAgentEmployeeAdvanceFilerColumnOrder()
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

            // Variable Random
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify status column is visible on the page..");
                agents_EmployeesHelper.IsElementPresent("HeadStatus");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify role column is visible on the page.");
                agents_EmployeesHelper.IsElementPresent("HeadRole");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify department column is visible on the page.");
                agents_EmployeesHelper.IsElementPresent("HeadDepartment");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify team column is visible on the page.");
                agents_EmployeesHelper.IsElementPresent("HeadTeam");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click on advance filter.");
                agents_EmployeesHelper.ClickElement("AdvanceFilter");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select status in displayed columns.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Status");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click arrow to move column to avail cols.");
                agents_EmployeesHelper.ClickElement("RemoveCols");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select role in displayed columns.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Role");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                agents_EmployeesHelper.ClickElement("RemoveCols");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select department in displayed columns.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Department");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                agents_EmployeesHelper.ClickElement("RemoveCols");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select team in displayed columns.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Team");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click arrow to move column to avail cols");
                agents_EmployeesHelper.ClickElement("RemoveCols");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click on Apply button.");
                agents_EmployeesHelper.ClickElement("ApplyButton");
                agents_EmployeesHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify status not present on page.");
                agents_EmployeesHelper.IsElementNotPresent("HeadStatus");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify role not present on page.");
                agents_EmployeesHelper.IsElementNotPresent("HeadRole");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify department not present on page.");
                agents_EmployeesHelper.IsElementNotPresent("HeadDepartment");
               // agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify team not present on page.");
                agents_EmployeesHelper.IsElementNotPresent("HeadTeam");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Redirect at leads page.");
                VisitOffice("leads");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify page title as leads.");
                VerifyTitle("Leads");

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Redirect To URL");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify default position of role column.");
                agents_EmployeesHelper.IsElementPresent("HeadRole5");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify default position of department column.");
                agents_EmployeesHelper.IsElementPresent("HeadDepartment6");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Redirect at sales agents page.");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click on advance filter.");
                agents_EmployeesHelper.ClickElement("AdvanceFilter");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select role in displayed column.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Role");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Move role 1 step up.");
                agents_EmployeesHelper.ClickElement("MoveUp");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Move role 1 step up.");
                agents_EmployeesHelper.ClickElement("MoveUp");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Move role 1 step up.");
                agents_EmployeesHelper.ClickElement("MoveUp");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Select department in displayed column.");
                agents_EmployeesHelper.SelectByText("DisplayedCols", "Department");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Move department 1 step down.");
                agents_EmployeesHelper.ClickElement("MoveDown");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Click on Apply button.");
                agents_EmployeesHelper.ClickElement("ApplyButton");
                agents_EmployeesHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify changed position of role column.");
                agents_EmployeesHelper.IsElementPresent("HeadRole3");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Verify changed position of department column.");
                agents_EmployeesHelper.IsElementPresent("HeadDepartment7");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAgentEmployeeAdvanceFilerColumnOrder", "Logout from the application.");
                VisitCorp("logout");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAgentEmployeeAdvanceFilerColumnOrder");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tickets Advance Filer Column Order");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tickets Advance Filer Column Order", "Bug", "Medium", "Activities page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
                        TakeScreenshot("VerifyAgentEmployeeAdvanceFilerColumnOrder");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAgentEmployeeAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAgentEmployeeAdvanceFilerColumnOrder");
                        string id = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAgentEmployeeAdvanceFilerColumnOrder.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tickets Advance Filer Column Order"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tickets Advance Filer Column Order");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAgentEmployeeAdvanceFilerColumnOrder");
                executionLog.WriteInExcel("Verify Tickets Advance Filer Column Order", Status, JIRA, "Meetings Management");
            }
        }
    }
}
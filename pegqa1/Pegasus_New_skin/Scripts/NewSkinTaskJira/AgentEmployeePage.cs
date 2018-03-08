using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AgentEmployeePage : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Temp")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void agentEmployeePage()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("AgentEmployeePage", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("AgentEmployeePage", "Verify Page title");
                VerifyTitle("Dashboard");

                //executionLog.Log("AgentEmployeePage", "Click on 'Menu' icon ");
                //agents_EmployeesHelper.ClickElement("MenuIcon");

                executionLog.Log("AgentEmployeePage", "Click on Agent tab");
                agents_EmployeesHelper.ClickElement("AgentTab");

                executionLog.Log("AgentEmployeePage", "Click On Employee");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("AgentEmployeePage", "Verify title");
                agents_EmployeesHelper.VerifyText("EmployeeHead", "Employee");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AgentEmployeePage");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Agent Employee Page");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Agent Employee Page", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Agent Employee Page");
                        TakeScreenshot("AgentEmployeePage");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentEmployeePage.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AgentEmployeePage");
                        string id = loginHelper.getIssueID("Agent Employee Page");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentEmployeePage.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Agent Employee Page"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Agent Employee Page");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AgentEmployeePage");
                executionLog.WriteInExcel("Agent Employee Page", Status, JIRA, "Agent Portal");
            }
        }
    }
}
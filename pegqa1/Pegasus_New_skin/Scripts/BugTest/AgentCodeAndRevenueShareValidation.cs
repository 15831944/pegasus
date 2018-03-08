using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class AgentCodeAndRevenueShareValidation : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void agentCodeAndRevenueShareValidation()
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
            var agent_Employee = new Agents_EmployeesHelper(GetWebDriver());

            // Variable
            var name = "TestAgent" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AgentCodeAndRevenueShareValidation", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AgentCodeAndRevenueShareValidation", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("AgentCodeAndRevenueShareValidation", "Redirect to the URL");
                VisitOffice("employees");

                executionLog.Log("AgentCodeAndRevenueShareValidation", "Click on any Agent employee.");
                agent_Employee.ClickElement("ClikOnEmployeeAgent");

                executionLog.Log("AgentCodeAndRevenueShareValidation", "Wait for edit icon to be present.");
                agent_Employee.WaitForElementPresent("ClickOnEditRSAC", 10);

                var EditLink = "//table[@id='list2']/tbody/tr[2]";

                if (agent_Employee.IsElementPresent(EditLink))
                {
                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click Edit revenue share");
                    agent_Employee.ClickElement("ClickOnEditRSAC");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enter Albhabet value in Revenue share");
                    agent_Employee.TypeText("EnterRSInAlphabet", "One");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enter Albhabet in Second Field");
                    agent_Employee.TypeText("EnterEditRSAC", "One");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click Save");
                    agent_Employee.ClickElement("ClickonSaveEditRC");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Verify Validation message for valid naumber.");
                    agent_Employee.WaitForText("Please enter a valid number", 10);

                }
                else
                {

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click New Agent Code");
                    agent_Employee.ClickElement("AddaNewAgentCodse");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enetr Sales code");
                    agent_Employee.TypeText("SalesCode", "1");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enetr Revenue Share");
                    agent_Employee.TypeText("RevenueSahreAsAgent", "2");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enetr Revenue Share as Manager");
                    agent_Employee.TypeText("RevenueShareAsManager", "2");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click Save button");
                    agent_Employee.ClickElement("ClickSaveBtnRSAC");
                    agent_Employee.WaitForElementPresent("ClickOnEditRSAC", 10);

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click Edit revenue share");
                    agent_Employee.ClickElement("ClickOnEditRSAC");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enter Alpha value in first field. ");
                    agent_Employee.TypeText("EnterRSInAlphabet", "One");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Enter Alphabet in Second Field");
                    agent_Employee.TypeText("EnterEditRSAC", "One");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Click Save");
                    agent_Employee.ClickElement("ClickonSaveEditRC");

                    executionLog.Log("AgentCodeAndRevenueShareValidation", "Verify Validation");
                    agent_Employee.WaitForText("Please enter a valid number", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AgentCodeAndRevenueShareValidation");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("AgentCode And Revenue Share Validation");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("AgentCode And Revenue Share Validation", "Bug", "Medium", "Office Agent", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("AgentCode And Revenue Share Validation");
                        TakeScreenshot("AgentCodeAndRevenueShareValidation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentCodeAndRevenueShareValidation.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AgentCodeAndRevenueShareValidation");
                        string id = loginHelper.getIssueID("AgentCode And Revenue Share Validation");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AgentCodeAndRevenueShareValidation.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("AgentCode And Revenue Share Validation"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("AgentCode And Revenue Share Validation");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AgentCodeAndRevenueShareValidation");
                executionLog.WriteInExcel("AgentCode And Revenue Share Validation", Status, JIRA, "Agents Portal");
            }
        }
    }
}
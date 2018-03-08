using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeAdjustmentValidationToSelectAgent : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void employeeAdjustmentValidationToSelectAgent()
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
                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Redirect To Employee page");
                VisitOffice("employees");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Click on first employee.");
                agent_EmployeeHelper.ClickElement("OpenFirstAgent");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Click On Create Button");
                agent_EmployeeHelper.ClickElement("ClickOnCreateButton");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Click on Sale Agent.");
                agent_EmployeeHelper.ClickElement("ClickOnSaleAgent");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Wait for element to present.");
                agent_EmployeeHelper.WaitForElementPresent("EnterNameAdjustment", 05);

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Enter Name Adjustment");
                agent_EmployeeHelper.TypeText("EnterNameAdjustment", "Test Validation");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Select Adjustment Type");
                agent_EmployeeHelper.Select("SelectAdjustmentType", "Transaction");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Select Processor");
                agent_EmployeeHelper.Select("SelectProcessor", "Any");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Click Clear Agent Revenue Adjustment");
                agent_EmployeeHelper.ClickElement("ClickClearAgentRAdjustment");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Click On Save button");
                agent_EmployeeHelper.ClickElement("ClickOnSaveAdjutment");

                executionLog.Log("EmployeeAdjustmentValidationToSelectAgent", "Verify Alert Select One Sales Agent");
                agent_EmployeeHelper.VerifyAlertText("Select One Sales Agent");
                agent_EmployeeHelper.WaitForWorkAround(2000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeAdjustmentValidationToSelectAgent");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Adjustment Validation To SelectAgent");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Adjustment Validation To SelectAgent", "Bug", "Medium", "Employee Agent", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Adjustment Validation To SelectAgent");
                        TakeScreenshot("EmployeeAdjustmentValidationToSelectAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAdjustmentValidationToSelectAgent.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAdjustmentValidationToSelectAgent");
                        string id = loginHelper.getIssueID("Employee Adjustment Validation To SelectAgent");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAdjustmentValidationToSelectAgent.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Adjustment Validation To SelectAgent"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Adjustment Validation To SelectAgent");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAdjustmentValidationToSelectAgent");
                executionLog.WriteInExcel("Employee Adjustment Validation To SelectAgent", Status, JIRA, "Agents Portal");
            }
        }
    }
}
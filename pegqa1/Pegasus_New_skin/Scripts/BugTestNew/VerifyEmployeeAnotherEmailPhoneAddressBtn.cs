using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEmployeeAnotherEmailPhoneAddressBtn : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyEmployeeAnotherEmailPhoneAddressBtn()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Redirect To Employee page");
                VisitOffice("employees");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Click On Create Button");
                agent_EmployeeHelper.ClickElement("Create");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Click On Add Email button");
                agent_EmployeeHelper.ClickElement("AddEmail");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Verify Add Email button is clickable");
                agent_EmployeeHelper.IsElementVisible("//select[@id='EmployeeElectronicAddress1EmailImWebType']");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Click On Add Phone Button");
                agent_EmployeeHelper.ClickElement("AddPhone");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Verify Add Phone button is clickable");
                agent_EmployeeHelper.IsElementVisible("//select[@id='EmployeePhone1PhoneType']");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Click On Add Address Button");
                agent_EmployeeHelper.ClickElement("AddAddress");

                executionLog.Log("VerifyEmployeeAnotherEmailPhoneAddressBtn", "Verify Add Address button is clickable");
                agent_EmployeeHelper.IsElementVisible("//select[@id='EmployeeAddress1Type']");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmployeeAnotherEmailPhoneAddressBtn");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Employee Another Email Phone Address Btn");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Employee Another Email Phone Address Btn", "Bug", "Medium", "Employees page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Employee Another Email Phone Address Btn");
                        TakeScreenshot("VerifyEmployeeAnotherEmailPhoneAddressBtn");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmployeeAnotherEmailPhoneAddressBtn.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmployeeAnotherEmailPhoneAddressBtn");
                        string id = loginHelper.getIssueID("Verify Employee Another Email Phone Address Btn");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmployeeAnotherEmailPhoneAddressBtn.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Employee Another Email Phone Address Btn"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Employee Another Email Phone Address Btn");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEmployeeAnotherEmailPhoneAddressBtn");
                executionLog.WriteInExcel("Verify Employee Another Email Phone Address Btn", Status, JIRA, "Office Employees");
            }
           
        }
    }
}
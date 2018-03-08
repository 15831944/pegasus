using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyEmployeeEAddressLabel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("BugTestNew")]
        public void verifyEmployeeEAddressLabel()
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
                executionLog.Log("VerifyEmployeeEAddressLabel", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyEmployeeEAddressLabel", "Redirect To Create Employee page");
                VisitOffice("employees/create");
                agent_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Select eAddress Type >> E-mail");
                agent_EmployeeHelper.SelectByText("eAddressType", "E-Mail");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Verify eAddress Label >> Work");
                agent_EmployeeHelper.selectedOption("eAddressLebel", "Work");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Select eAddress Type >> IM");
                agent_EmployeeHelper.SelectByText("eAddressType", "IM");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Verify eAddress Label >> Google");
                agent_EmployeeHelper.selectedOption("eAddressLebel", "Google");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Select eAddress Type >> Social Media");
                agent_EmployeeHelper.SelectByText("eAddressType", "Social Media");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Verify eAddress Label >> Facebook");
                agent_EmployeeHelper.selectedOption("eAddressLebel", "Facebook");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Select eAddress Type >> Web Links");
                agent_EmployeeHelper.SelectByText("eAddressType", "Web Links");
                agent_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyEmployeeEAddressLabel", "Verify eAddress Label >> Web Link");
                agent_EmployeeHelper.selectedOption("eAddressLebel", "Web Link");
                agent_EmployeeHelper.WaitForWorkAround(1000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmployeeEAddressLabel");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Employee EAddress Label");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Employee EAddress Label", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Employee EAddress Label");
                        TakeScreenshot("VerifyEmployeeEAddressLabel");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmployeeEAddressLabel.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmployeeEAddressLabel");
                        string id = loginHelper.getIssueID("Verify Employee EAddress Label");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmployeeEAddressLabel.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Employee EAddress Label"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Employee EAddress Label");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEmployeeEAddressLabel");
                executionLog.WriteInExcel("Verify Employee EAddress Label", Status, JIRA, "Office Employees");
            }
           
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeAgentNS : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void employeeAgentNS()
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
            var username1 = "testinguser" + RandomNumber(111,99999);
            String Status = "Pass";
            String JIRA = "";

            try
            {

                executionLog.Log("EmployeeAgentNS", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmployeeAgentNS", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmployeeAgentNS", "Redirect to Employee page");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAgentNS", "Click On Create Employee Btn");
                agents_EmployeesHelper.ClickElement("Create");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAgentNS", "Select Salutation");
                agents_EmployeesHelper.Select("SelectSalutation", "Mr");

                executionLog.Log("EmployeeAgentNS", "Enter FirstNAME");
                agents_EmployeesHelper.TypeText("FirstNAME", "Test Agent");

                executionLog.Log("EmployeeAgentNS", "Enter LastName");
                agents_EmployeesHelper.TypeText("LastName", "Tester");

                executionLog.Log("EmployeeAgentNS", "Enter Date Of Birth");
                agents_EmployeesHelper.TypeText("BirthDay", "02/03/1991");

                executionLog.Log("EmployeeAgentNS", "Select eAddressType");
                agents_EmployeesHelper.Select("eAddressType", "E-Mail");

                executionLog.Log("EmployeeAgentNS", "Select eAddressLebel");
                agents_EmployeesHelper.Select("eAddressLebel", "Work");

                executionLog.Log("EmployeeAgentNS", "Enter eAddressType");
                agents_EmployeesHelper.TypeText("eAddress", "Test@gmail.com");

                executionLog.Log("EmployeeAgentNS", "Select SelectPhoneType");
                agents_EmployeesHelper.Select("SelectPhoneType", "Work");

                executionLog.Log("EmployeeAgentNS", "Enter PhoneNumber");
                agents_EmployeesHelper.TypeText("PhoneNumber", "121212121");

                executionLog.Log("EmployeeAgentNS", "Select Address Type");
                agents_EmployeesHelper.Select("AddressType", "Office");

                executionLog.Log("EmployeeAgentNS", "Enter AddressLine1");
                agents_EmployeesHelper.TypeText("AddressLine1", "FC 89");

                executionLog.Log("EmployeeAgentNS", "Enter Postal Code");
                agents_EmployeesHelper.TypeText("PostalCode", "60601");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("EditCorpEmployee", "Enter Username");
                agents_EmployeesHelper.TypeText("UserName", username1);

                executionLog.Log("EditCorpEmployee", "Select Avatar");
                agents_EmployeesHelper.ClickElement("AdminUserAvatar");

                executionLog.Log("EditCorpEmployee", "Deselect Auto Generate Password check box");
                agents_EmployeesHelper.ClickElement("AutoGenPswdChkBox");

                executionLog.Log("EditCorpEmployee", "Enter Password");
                agents_EmployeesHelper.TypeText("Password", "123456789");

                agents_EmployeesHelper.ClickElement("SaveEmployee");
                agents_EmployeesHelper.WaitForText("The employee is successfully added", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeAgentNS");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Agent NS");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Agent NS", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Agent NS");
                        TakeScreenshot("EmployeeAgentNS");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentNS.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAgentNS");
                        string id = loginHelper.getIssueID("Employee Agent NS");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAgentNS.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Agent NS"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Agent NS");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAgentNS");
                executionLog.WriteInExcel("Employee Agent NS", Status, JIRA, "Agent Portal");
            }
        }
    }
}
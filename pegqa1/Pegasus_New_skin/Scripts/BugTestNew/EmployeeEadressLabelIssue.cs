using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EmployeeEadressLabelIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void employeeEadressLabelIssue()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());

            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());
            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password2");

            // Variable random
            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + RandomNumber(44, 999) + "@gmail.com";
            var email2 = "test1" + RandomNumber(1, 99) + "@gmail.com";
            var phone = "12345" + RandomNumber(11111, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EmployeeEadressLabelIssue", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("EmployeeEadressLabelIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmployeeEadressLabelIssue", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("EmployeeEadressLabelIssue", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeEadressLabelIssue", "Go to Create employee page");
                VisitCorp("employees/create");

                executionLog.Log("EmployeeEadressLabelIssue", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);

                executionLog.Log("EmployeeEadressLabelIssue", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("EmployeeEadressLabelIssue", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("EmployeeEadressLabelIssue", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("EmployeeEadressLabelIssue", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("EmployeeEadressLabelIssue", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email2);

                executionLog.Log("EmployeeEadressLabelIssue", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("EmployeeEadressLabelIssue", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");

                executionLog.Log("EmployeeEadressLabelIssue", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("EmployeeEadressLabelIssue", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                executionLog.Log("EmployeeEadressLabelIssue", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeEadressLabelIssue", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeEadressLabelIssue", "Wait for locator to present.");
                corp_EmployeeHelper.WaitForElementPresent("EAddressType2", 10);

                executionLog.Log("EmployeeEadressLabelIssue", "Change eaddress type for second email.");
                corp_EmployeeHelper.Select("EAddressType2", "Social Media");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeEadressLabelIssue", "Verify first email label not changed.");
                corp_EmployeeHelper.VerifyText("EAddressLabel1", "Work");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeEadressLabelIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Eadress Label Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Eadress Label Issue", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Eadress Label Issue");
                        TakeScreenshot("EmployeeEadressLabelIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeEadressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeEadressLabelIssue");
                        string id = loginHelper.getIssueID("Employee Eadress Label Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeEadressLabelIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Eadress Label Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Eadress Label Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeEadressLabelIssue");
                executionLog.WriteInExcel("Employee Eadress Label Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}
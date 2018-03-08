using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyEmailRemovedForEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void verifyEmailRemovedForEmployee()
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
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyEmailRemovedForEmployee", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Go to Create employee page");
                VisitCorp("employees/create");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("VerifyEmailRemovedForEmployee", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email2);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Click on add email button");
                corp_EmployeeHelper.ClickElement("AddEmail");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter added email.");
                corp_EmployeeHelper.TypeText("eAddress2", email2);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");

                executionLog.Log("VerifyEmailRemovedForEmployee", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Remove first email.");
                corp_EmployeeHelper.ClickDisplayed("//a[@title='Delete E-Mail']");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Remove second email.");
                corp_EmployeeHelper.ClickDisplayed("//a[@title='Delete E-Mail']");

                executionLog.Log("VerifyEmailRemovedForEmployee", "Click On Save");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyEmailRemovedForEmployee", "Verify removed email not present on the page.");
                corp_EmployeeHelper.EmailRemoved(email2);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyEmailRemovedForEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Email Removed For Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Email Removed For Employee", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Email Removed For Employee");
                        TakeScreenshot("VerifyEmailRemovedForEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailRemovedForEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyEmailRemovedForEmployee");
                        string id = loginHelper.getIssueID("Verify Email Removed For Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyEmailRemovedForEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Email Removed For Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Email Removed For Employee");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyEmailRemovedForEmployee");
                executionLog.WriteInExcel("Verify Email Removed For Employee", Status, JIRA, "Corp Employee");
            }
        }
    }
}

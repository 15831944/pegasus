using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyPhoneRemovedForEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void verifyPhoneRemovedForEmployee()
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
                executionLog.Log("VerifyPhoneRemovedForEmployee", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Go to Create employee page");
                VisitCorp("employees/create");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email2);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Click on add phone button");
                corp_EmployeeHelper.ClickElement("AddPhone");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter added phone.");
                corp_EmployeeHelper.TypeText("PhoneNumber2", phone);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Remove added phone.");
                corp_EmployeeHelper.ClickElement("RemovePhone");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Click On Save");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyPhoneRemovedForEmployee", "Verify removed phone not present on the page.");
                corp_EmployeeHelper.EmailRemoved(phone);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyPhoneRemovedForEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify phone Removed For Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify phone Removed For Employee", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify phone Removed For Employee");
                        TakeScreenshot("VerifyPhoneRemovedForEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPhoneRemovedForEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyPhoneRemovedForEmployee");
                        string id = loginHelper.getIssueID("Verify phone Removed For Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyPhoneRemovedForEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify phone Removed For Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify phone Removed For Employee");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyPhoneRemovedForEmployee");
                executionLog.WriteInExcel("Verify phone Removed For Employee", Status, JIRA, "Corp Employee");
            }
        }
    }
}


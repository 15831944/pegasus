using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EditabilityOfEmployeeEmailAndPhone : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void editabilityOfEmployeeEmailAndPhone()
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
                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Go to Create employee page");
                VisitCorp("employees/create");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email2);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Click on add phone button");
                corp_EmployeeHelper.ClickElement("AddPhone");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter added phone.");
                corp_EmployeeHelper.TypeText("PhoneNumber2", phone);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Wait for locator to be present.");
                corp_EmployeeHelper.WaitForElementPresent("RemovePhone", 10);

                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Remove added phone.");
                corp_EmployeeHelper.ClickElement("RemovePhone");
          
                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Click On Save");
                corp_EmployeeHelper.ClickElement("Save");
               
                executionLog.Log("EditabilityOfEmployeeEmailAndPhone", "Search created employee by email.");
                corp_EmployeeHelper.WaitForText("Employee Details successfully updated", 10);
 
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditabilityOfEmployeeEmailAndPhone");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Editability Of Employee Email And Phone");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Editability Of Employee Email And Phone", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Editability Of Employee Email And Phone");
                        TakeScreenshot("EditabilityOfEmployeeEmailAndPhone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditabilityOfEmployeeEmailAndPhone.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditabilityOfEmployeeEmailAndPhone");
                        string id = loginHelper.getIssueID("Editability Of Employee Email And Phone");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditabilityOfEmployeeEmailAndPhone.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Editability Of Employee Email And Phone"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Editability Of Employee Email And Phone");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditabilityOfEmployeeEmailAndPhone");
                executionLog.WriteInExcel("Editability Of Employee Email And Phone", Status, JIRA, "Corp Employee");
            }
        }
    }
}


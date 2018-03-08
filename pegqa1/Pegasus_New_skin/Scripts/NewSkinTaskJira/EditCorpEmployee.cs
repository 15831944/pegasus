using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditCorpEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void editCorpEmployee()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());
            String Status = "Pass";
            String JIRA = "";
            var username1 = "testingcorpuser" + RandomNumber(111,999999);

            try
            {
                executionLog.Log("EditCorpEmployee", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditCorpEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditCorpEmployee", "Click on employee tab.");
                corp_EmployeeHelper.ClickElement("EmployeesTab");

                executionLog.Log("EditCorpEmployee", "Search Employee");
                corp_EmployeeHelper.TypeText("SearchEmpName", "Test Tester");

                var Loc = "//table[@id='list1']/tbody/tr[2]";
                if (corp_EmployeeHelper.IsElementPresent(Loc))
                {

                    executionLog.Log("EditCorpEmployee", "Click on Edit");
                    corp_EmployeeHelper.ClickElement("ClickOnEdit");
                    corp_EmployeeHelper.WaitForWorkAround(3000);

                    executionLog.Log("EditCorpEmployee", "Enter zip code");
                    corp_EmployeeHelper.TypeText("ZipCode", "60601");
                    corp_EmployeeHelper.WaitForWorkAround(3000);

                    executionLog.Log("EditCorpEmployee", "Enter First Name");
                    corp_EmployeeHelper.TypeText("FirstName", "Test");

                    executionLog.Log("EditCorpEmployee", "Enter Last Name");
                    corp_EmployeeHelper.TypeText("LastName", "Tester");

                    executionLog.Log("EditCorpEmployee", "Enter Phone number.");
                    corp_EmployeeHelper.TypeText("PhoneNumber", "1111111111");

                    executionLog.Log("EditCorpEmployee", "Click primary radio button.");
                    corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                    executionLog.Log("EditCorpEmployee", "Enter eAddress");
                    corp_EmployeeHelper.TypeText("eAddress", "test@gmail.com");

                    executionLog.Log("EditCorpEmployee", "Click primary radio button.");
                    corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                    executionLog.Log("EditCorpEmployee", "Click On Save");
                    corp_EmployeeHelper.ClickElement("Save");

                    executionLog.Log("EditCorpEmployee", "Verify message");
                    corp_EmployeeHelper.WaitForText("Employee Details successfully updated", 10);

                }
                else
                {

                    executionLog.Log("EditCorpEmployee", "ClickOnCreate");
                    corp_EmployeeHelper.ClickElement("Create");

                    executionLog.Log("EditCorpEmployee", "Enter User Name");
                    var usernme = "EmpUser" + RandomNumber(1, 999);
                    corp_EmployeeHelper.TypeText("UserName", usernme);

                    executionLog.Log("EditCorpEmployee", "Click On Save");
                    corp_EmployeeHelper.ClickElement("Save");

                    executionLog.Log("EditCorpEmployee", "Verify This field is required.");
                    corp_EmployeeHelper.VerifyPageText("This field is required.");

                    executionLog.Log("EditCorpEmployee", "verify validation ");
                    corp_EmployeeHelper.VerifyPageText("This field is required.");

                    executionLog.Log("EditCorpEmployee", "verify validation");
                    corp_EmployeeHelper.VerifyText("VerifyValidation", "This field is required.");

                    executionLog.Log("EditCorpEmployee", "Verify validation");
                    corp_EmployeeHelper.VerifyText("VerifyAvatar", "This field is required.");

                    executionLog.Log("EditCorpEmployee", "Verify validatation");
                    corp_EmployeeHelper.VerifyText("VerifyEmail", "This field is required.");

                    executionLog.Log("EditCorpEmployee", "Verify validation");
                    corp_EmployeeHelper.VerifyText("VerifyPhoneNumber", "This field is required.");

                    executionLog.Log("EditCorpEmployee", "Verify validation");
                    corp_EmployeeHelper.VerifyText("VerifyLastName", "This field is required.");

                    executionLog.Log("EditCorpEmployee", "Enter First Name");
                    corp_EmployeeHelper.TypeText("FirstName", "Test");

                    executionLog.Log("EditCorpEmployee", "Enter Last Name");
                    corp_EmployeeHelper.TypeText("LastName", "Tester");

                    executionLog.Log("EditCorpEmployee", "Enter Primary Email");
                    var Email = "Email" + RandomNumber(1, 999) + "@yopmail.com";
                    corp_EmployeeHelper.TypeText("PrimaryEmail", Email);

                    executionLog.Log("EditCorpEmployee", "Click On Check box ");
                    corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                    executionLog.Log("EditCorpEmployee", "Enter Phone Number");
                    corp_EmployeeHelper.TypeText("PhoneNumber", "9898777332");

                    executionLog.Log("EditCorpEmployee", "Enter Eaddress");
                    var mail = "mail" + RandomNumber(1, 999) + "@yopmail.com";
                    corp_EmployeeHelper.TypeText("eAddress", mail);

                    executionLog.Log("EditCorpEmployee", "Enter Username");
                    corp_EmployeeHelper.TypeText("UserName", username1);

                    executionLog.Log("EditCorpEmployee", "Select Avatar");
                    corp_EmployeeHelper.ClickElement("AdminUserAvatar");

                    executionLog.Log("EditCorpEmployee", "Click On Save");
                    corp_EmployeeHelper.ClickElement("ClickSaveBtn");

                    executionLog.Log("EditCorpEmployee", "verify success message");
                    corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                    executionLog.Log("EditCorpEmployee", "Search Employee");
                    corp_EmployeeHelper.TypeText("SearchEmpName", "Test Tester");

                    executionLog.Log("EditCorpEmployee", "Enter Email To Search");
                    corp_EmployeeHelper.TypeText("SearchEnterEmail", Email);
                    corp_EmployeeHelper.WaitForWorkAround(3000);

                    executionLog.Log("EditCorpEmployee", "Click on Edit");
                    corp_EmployeeHelper.ClickElement("ClickOnEdit");
                    corp_EmployeeHelper.WaitForWorkAround(3000);

                    executionLog.Log("EditCorpEmployee", "Enter zip code");
                    corp_EmployeeHelper.TypeText("ZipCode", "60601");
                    corp_EmployeeHelper.WaitForWorkAround(3000);

                    executionLog.Log("EditCorpEmployee", "Enter First Name");
                    corp_EmployeeHelper.TypeText("FirstName", "Test");

                    executionLog.Log("EditCorpEmployee", "Enter Last Name");
                    corp_EmployeeHelper.TypeText("LastName", "Tester");

                    executionLog.Log("EditCorpEmployee", "Click On Save");
                    corp_EmployeeHelper.ClickElement("ClickOnSave");

                    executionLog.Log("EditCorpEmployee", "Verify success message");
                    corp_EmployeeHelper.WaitForText("Employee Details successfully updated", 10);

                }
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditCorpEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Corp Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Delete PDF Template Corp", "Bug", "Medium", "Employee", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Corp Employee");
                        TakeScreenshot("EditCorpEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditCorpEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditCorpEmployee");
                        string id = loginHelper.getIssueID("Edit Corp Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditCorpEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Corp Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Corp Employee");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditCorpEmployee");
                executionLog.WriteInExcel("Edit Corp Employee", Status, JIRA, "Corp Employee");
            }      
        }
    }
}
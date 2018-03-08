using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpEmployeePhoneIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void corpEmployeePhoneIssue()
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
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var username = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CorpEmployeePhoneIssue", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CorpEmployeePhoneIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CorpEmployeePhoneIssue", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("CorpEmployeePhoneIssue", "Verify Page title");
                VerifyTitle("Employees");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Go to Create employee page");
                VisitCorp("employees/create");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeePhoneIssue", "Verify Page title");
                VerifyTitle("Employees");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email);
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEmployee", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");
                corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEmployee", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");
                //corp_EmployeeHelper.WaitForWorkAround(3000);


                executionLog.Log("CreateEmployee", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeePhoneIssue", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeePhoneIssue", "Make secon email as primary");
                corp_EmployeeHelper.ClickElement("PrimaryEmail2");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CorpEmployeePhoneIssue", "Click On Save");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeePhoneIssue", "Click on edit icon");
                corp_EmployeeHelper.ClickElement("ClickOnEdit");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CorpEmployeePhoneIssue", "Remove primary email");
                corp_EmployeeHelper.ClickElement("RemoveEmail");
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpEmployeePhoneIssue", "Click On Save");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CorpEmployeePhoneIssue", "Verify success message");
                corp_EmployeeHelper.VerifyPageText("Employee Details successfully updated");
                //corp_EmployeeHelper.WaitForWorkAround(3000);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpEmployeePhoneIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Employee Phone Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Employee Phone Issue", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Employee Phone Issue");
                        TakeScreenshot("CorpEmployeePhoneIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeePhoneIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpEmployeePhoneIssue");
                        string id = loginHelper.getIssueID("Corp Employee Phone Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeePhoneIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Employee Phone Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Employee Phone Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpEmployeePhoneIssue");
                executionLog.WriteInExcel("Corp Employee Phone Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}


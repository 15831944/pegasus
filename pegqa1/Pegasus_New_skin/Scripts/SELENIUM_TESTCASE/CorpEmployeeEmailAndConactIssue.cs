using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CorpEmployeeEmailAndConactIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS8")]
        [TestCategory("SELENIUM_TESTCASE")]
        public void corpEmployeeEmailAndConactIssue()
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
            var email = "Test" + RandomNumber(100, 1000) + "@gmail.com";
            var Name = "Emptest" + RandomNumber(50, 500);
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CorpEmployeeEmailAndConactIssue", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CorpEmployeeEmailAndConactIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                corp_EmployeeHelper.WaitForWorkAround(6000);

                executionLog.Log("CorpEmployeeEmailAndConactIssue", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("CorpEmployeeEmailAndConactIssue", "Verify Page title");
                VerifyTitle("Employees");
                corp_EmployeeHelper.WaitForWorkAround(6000);

                executionLog.Log("CreateEmployee", "Go to Create employee page");
                VisitCorp("employees/create");

                executionLog.Log("CorpEmployeeEmailAndConactIssue", "Verify Page title");
                VerifyTitle("Employees");
                corp_EmployeeHelper.WaitForWorkAround(6000);

                executionLog.Log("CreateEmployee", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", Name);

                executionLog.Log("CreateEmployee", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("CreateEmployee", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("CreateEmployee", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("CreateEmployee", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("CreateEmployee", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("CreateEmployee", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("CreateEmployee", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("CreateEmployee", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("CreateEmployee", "Click On Add Phone Number.");
                corp_EmployeeHelper.ClickElement("AddPhone");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Enter Phone Number in Second Phone Field.");
                corp_EmployeeHelper.TypeText("PhoneNumber2", "8533327453");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email);
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Click On Add Email.");
                corp_EmployeeHelper.ClickElement("AddEmail");
                //corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEmployee", "Enter Email Address.");
                corp_EmployeeHelper.TypeText("eAddress2", "test09090@yopmail.com");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");
                //corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("CreateEmployee", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");
                corp_EmployeeHelper.WaitForWorkAround(4000);

                executionLog.Log("CreateEmployee", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(6000);

                executionLog.Log("CreateEmployee", "Verify Second added email.");
                corp_EmployeeHelper.ElementPresent("eAddress2");
                corp_EmployeeHelper.WaitForWorkAround(5000);

                executionLog.Log("CreateEmployee", "Verify Second added Phone Number.");
                executionLog.Log("CreateEmployee", "Click On Add Phone Number.");
                corp_EmployeeHelper.ElementPresent("PhoneNumber2");
                corp_EmployeeHelper.WaitForWorkAround(5000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CorpEmployeeEmailAndConactIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Corp Employee Email And Conact Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Corp Employee Email And Conact Issue", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Corp Employee Email And Conact Issue");
                        TakeScreenshot("CorpEmployeeEmailAndConactIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeEmailAndConactIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CorpEmployeeEmailAndConactIssue");
                        string id = loginHelper.getIssueID("Corp Employee Email And Conact Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CorpEmployeeEmailAndConactIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Corp Employee Email And Conact Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Corp Employee Email And Conact Issue");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CorpEmployeeEmailAndConactIssue");
                executionLog.WriteInExcel("Corp Employee Email And Conact Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}


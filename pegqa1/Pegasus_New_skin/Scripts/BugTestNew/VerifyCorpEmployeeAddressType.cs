using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyCorpEmployeeAddressType : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("Fail")]
        [TestCategory("BugTestNew")]
        public void verifyCorpEmployeeAddressType()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());

            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_corp");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable random
            var username1 = "TESTUSER" + GetRandomNumber();
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyCorpEmployeeAddressType", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Go to Create employee page");
                VisitCorp("employees/create");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username1);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "Test Name");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter Last Name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");

                executionLog.Log("VerifyCorpEmployeeAddressType", "  Click Corporate Admin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Select Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Select Address Type >> Mailing");
                corp_EmployeeHelper.SelectByText("AddressType1", "Mailing");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("VerifyCorpEmployeeAddressType", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Click On Save button");
                corp_EmployeeHelper.ClickElement("Save");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Wait for confirmation.");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Search created employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Open employee");
                corp_EmployeeHelper.ClickElement("ClickOnEmployee");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyCorpEmployeeAddressType", "Verify eAddress Type is appearing");
                corp_EmployeeHelper.VerifyText("Address1", "Mailing");
                corp_EmployeeHelper.WaitForWorkAround(1000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyCorpEmployeeAddressType");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Corp Employee Address Type");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Corp Employee Address Type", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Corp Employee Address Type");
                        TakeScreenshot("VerifyCorpEmployeeAddressType");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorpEmployeeAddressType.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyCorpEmployeeAddressType");
                        string id = loginHelper.getIssueID("Verify Corp Employee Address Type");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyCorpEmployeeAddressType.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Corp Employee Address Type"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Corp Employee Address Type");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyCorpEmployeeAddressType");
                executionLog.WriteInExcel("Verify Corp Employee Address Type", Status, JIRA, "Corp Employee");
            }
        }
    }
}


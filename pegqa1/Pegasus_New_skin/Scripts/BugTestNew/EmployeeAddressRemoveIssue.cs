using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class EmployeeAddressRemoveIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Aslam_Corp")]
        public void employeeAddressRemoveIssue()
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
            var username = "TESTUSEREmp" + RandomNumber(1, 9999);
            var email = "Test" + RandomNumber(1, 999999) + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("EmployeeAddressRemoveIssue", "Login with valid username and password");
                Login("newthemecorp", "pegasus");

                executionLog.Log("EmployeeAddressRemoveIssue", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EmployeeAddressRemoveIssue", "Go to Employee Address Remove Issue page");
                VisitCorp("employees/create");

                executionLog.Log("EmployeeAddressRemoveIssue", "Verify title");
                VerifyTitle("Employees");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter Name");
                corp_EmployeeHelper.TypeText("UserName", username);

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter PrimaryEmail");
                corp_EmployeeHelper.TypeText("PrimaryEmail", email);

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter Salutation");
                corp_EmployeeHelper.Select("Salutation", "Mr");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter First name");
                corp_EmployeeHelper.TypeText("FirstName", "test");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter Last name");
                corp_EmployeeHelper.TypeText("LastName", "Last");

                executionLog.Log("EmployeeAddressRemoveIssue", "  Click CorporateAdmin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");

                executionLog.Log("EmployeeAddressRemoveIssue", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");

                executionLog.Log("EmployeeAddressRemoveIssue", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email);

                executionLog.Log("EmployeeAddressRemoveIssue", "Click Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Click on add address.");
                corp_EmployeeHelper.ClickElement("AddAddress");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCOde2", "60601");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Enter address line 1 for added address.");
                corp_EmployeeHelper.TypeText("AddressLine2", "TestAddressRemoved");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("EmployeeAddressRemoveIssue", "Search created user by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Click on edit icon.");
                corp_EmployeeHelper.ClickElement("EditEmployee");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Remove added address.");
                corp_EmployeeHelper.ClickElement("RemoveAddress");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "CliCK On Save button.");
                corp_EmployeeHelper.ClickElement("Save");

                executionLog.Log("EmployeeAddressRemoveIssue", "Wait for success test.");
                corp_EmployeeHelper.WaitForText("Employee Details successfully updated", 10);

                executionLog.Log("EmployeeAddressRemoveIssue", "Search employee by email.");
                corp_EmployeeHelper.TypeText("searchEmail", email);
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Clcik on edit icon.");
                corp_EmployeeHelper.ClickElement("EditEmployee");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeAddressRemoveIssue", "Verify removed address not present on page.");
                corp_EmployeeHelper.VerifyTextNot("TestAddressRemoved");
                corp_EmployeeHelper.WaitForWorkAround(3000);


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeAddressRemoveIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Address Remove Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Address Remove Issue", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Address Remove Issue");
                        TakeScreenshot("EmployeeAddressRemoveIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAddressRemoveIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeAddressRemoveIssue");
                        string id = loginHelper.getIssueID("Employee Address Remove Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeAddressRemoveIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Address Remove Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Address Remove Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeAddressRemoveIssue");
                executionLog.WriteInExcel("Employee Address Remove Issue", Status, JIRA, "Corp Employee");
            }
        }
    }
}

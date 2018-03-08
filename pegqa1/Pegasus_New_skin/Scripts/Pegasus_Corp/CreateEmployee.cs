using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class CreateEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Corp")]
        [TestCategory("Fail")]
        [TestCategory("TS7")]
        [TestCategory("Pegasus_Corp")]
        public void createEmployee()
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
            var email = "Test" + RandomNumber(1, 99) + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("CreateEmployee", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("CreateEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateEmployee", "Go to Create employee page");
                VisitCorp("employees/create");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Verify title");
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
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter Last name");
                corp_EmployeeHelper.TypeText("LastName", "Test LastName");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "  Click CorporateAdmin Avatar");
                corp_EmployeeHelper.ClickElement("AvtarCorporateAdmin");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Select Phone Country");
                corp_EmployeeHelper.Select("PhoneSelectCountry", "1");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter PhoneNumber");
                corp_EmployeeHelper.TypeText("PhoneNumber", "9898398438");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Select Primary Phone Number");
                corp_EmployeeHelper.ClickElement("PrimaryPhoneRadio");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter eAddress");
                corp_EmployeeHelper.TypeText("eAddress", email);
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Click Primary Email");
                corp_EmployeeHelper.ClickElement("PrimaryEmailRadio");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter AddressLine1");
                corp_EmployeeHelper.TypeText("AddressLine1", "F-TEST");
                //corp_EmployeeHelper.WaitForWorkAround(1000);

                executionLog.Log("CreateEmployee", "Enter ZipCode");
                corp_EmployeeHelper.TypeText("ZipCode", "60601");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "CliCK On Save button");
                corp_EmployeeHelper.ClickElement("Save");
                //corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("CreateEmployee", "Wait for success message");
                corp_EmployeeHelper.WaitForText("Employee Created Successfully.", 10);

            }


            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                Console.WriteLine("Counter value is    " + counter);
                String Description = executionLog.GetAllTextFile("CreateEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Employee", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Employee");
                        TakeScreenshot("CreateEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateEmployee");
                        string id = loginHelper.getIssueID("Create Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Employee");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateEmployee");
                executionLog.WriteInExcel("Create Employee", Status, JIRA, "Corp Employee");
            }
        }
    }
}

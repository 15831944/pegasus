using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyDefaultDisplayColumnEmployee : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        public void verifyDefaultDisplayColumnEmployee()
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
            var email = "Test" + GetRandomNumber() + "@gmail.com";
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Go to Employee page");
                VisitCorp("employees");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify Page title");
                VerifyTitle("Employees");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Click on advance filter.");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify name column in displayed column");
                corp_EmployeeHelper.VerifyColumn("Name");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify email in displayed column");
                corp_EmployeeHelper.VerifyColumn("E-Mail");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify phone in displayed column");
                corp_EmployeeHelper.VerifyColumn("Phone");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify modified in displayed column");
                corp_EmployeeHelper.VerifyColumn("Modified");

                executionLog.Log("VerifyDefaultDisplayColumnEmployee", "Verify last logon in displayed column");
                corp_EmployeeHelper.VerifyColumn("Last Logon");


            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyDefaultDisplayColumnEmployee");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Default Display Column Employee");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Default Display Column Employee", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Default Display Column Employee");
                        TakeScreenshot("VerifyDefaultDisplayColumnEmployee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDefaultDisplayColumnEmployee.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyDefaultDisplayColumnEmployee");
                        string id = loginHelper.getIssueID("Verify Default Display Column Employee");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyDefaultDisplayColumnEmployee.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Default Display Column Employee"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Default Display Column Employee");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyDefaultDisplayColumnEmployee");
                executionLog.WriteInExcel("Verify Default Display Column Employee", Status, JIRA, "Corp Employee");
            }
        }
    }
}
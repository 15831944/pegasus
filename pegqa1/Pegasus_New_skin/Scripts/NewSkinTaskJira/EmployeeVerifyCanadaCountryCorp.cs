using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EmployeeVerifyCanadaCountryCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("Fail")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void employeeVerifyCanadaCountryCorp()
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

            // VARIABLE
            var name = "TestEmployee" + GetRandomNumber();
            String Status = "Pass";
            String JIRA = "";

            try
            {
                executionLog.Log("EmployeeVerifyCanadaCountryCorp", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EmployeeVerifyCanadaCountryCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeVerifyCanadaCountryCorp", "Redirect at create employee page.");
                VisitCorp("employees/create");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("EmployeeVerifyCanadaCountryCorp", "Select Mailing Country");
                corp_EmployeeHelper.Select("Country", "Canada");
                corp_EmployeeHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";
                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EmployeeVerifyCanadaCountryCorp");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Employee Verify Canada Country Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Employee Verify Canada Country Corp", "Bug", "Medium", "Corp Employee page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Employee Verify Canada Country Corp");
                        TakeScreenshot("EmployeeVerifyCanadaCountryCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeVerifyCanadaCountryCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EmployeeVerifyCanadaCountryCorp");
                        string id = loginHelper.getIssueID("Employee Verify Canada Country Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EmployeeVerifyCanadaCountryCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Employee Verify Canada Country Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Employee Verify Canada Country Corp");
                //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EmployeeVerifyCanadaCountryCorp");
                executionLog.WriteInExcel("Employee Verify Canada Country Corp", Status, JIRA, "Corp Employee");
            }
        }
    }
}
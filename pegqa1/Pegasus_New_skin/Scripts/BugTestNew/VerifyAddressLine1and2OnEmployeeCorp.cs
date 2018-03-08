using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyAddressLine1and2OnEmployeeCorp : DriverTestCase
    {
        [TestMethod]
        [TestCategory("Corp")]
        [TestCategory("All")]
        [TestCategory("TS2")]
        [TestCategory("BugTestNew")]
        public void verifyAddressLine1and2OnEmployeeCorp()
        {
            string[] username1 = null;
            string[] password1 = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username1 = oXMLData.getData("settings/Credentials", "username_corp");
            password1 = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var corp_EmployeeHelper = new Corp_EmployeeHelper(GetWebDriver());

            // Variable random

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Login with valid username and password");
                Login(username1[0], password1[0]);

                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Go to office page");
                VisitCorp("employees");
                corp_EmployeeHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Verify title as offices");
                VerifyTitle("Employees");

                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Click On Advanced Filter button");
                corp_EmployeeHelper.ClickElement("AdvanceFilter");
                corp_EmployeeHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyAddressLine1and2OnEmployeeCorp", "Verify options present");
                corp_EmployeeHelper.VerifyText("AvailableCols", "Address Line 1");
                corp_EmployeeHelper.VerifyText("AvailableCols", "Address Line 2");
            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyAddressLine1and2OnEmployeeCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Address Line1 and 2 On Employee Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Address Line1 and 2 On Employee Corp", "Bug", "Medium", "Employee page", "QA", "Log in as: " + username1[0] + " / " + password1[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Address Line1 and 2 On Employee Corp");
                        TakeScreenshot("VerifyAddressLine1and2OnEmployeeCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddressLine1and2OnEmployeeCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyAddressLine1and2OnEmployeeCorp");
                        string id = loginHelper.getIssueID("Verify Address Line1 and 2 On Employee Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyAddressLine1and2OnEmployeeCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Address Line1 and 2 On Employee Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Address Line1 and 2 On Employee Corp");
                //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyAddressLine1and2OnEmployeeCorp");
                executionLog.WriteInExcel("Verify Address Line1 and 2 On Employee Corp", Status, JIRA, "Corp Employees");
            }
        }
    }
}

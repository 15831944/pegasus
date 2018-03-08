using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class VerifyEmployeeEAddressLabel : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("SELENIUM_TESTCASE")]
        [TestCategory("TS8")]
        public void verifyEmployeeEAddressLabel()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var agents_EmployeesHelper = new Agents_EmployeesHelper(GetWebDriver());

            // Variable

            String JIRA = "";
            String Status = "Pass";
            var username1 = "Testuser" + GetRandomNumber();

            try
            {

                executionLog.Log("PartnerAgentCodeInQuickLook", "Login with valid credential  Username");
                Login(username[0], password[0]);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("PartnerAgentCodeInQuickLook", "Goto Opportinuties");
                VisitOffice("employees");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Open Employee 1");
                agents_EmployeesHelper.ClickElement("EditEmployee1");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Select eAddress Label");
                agents_EmployeesHelper.SelectByText("eAddressLebel", "Work");
                //agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Check if User account is not created");
                if (agents_EmployeesHelper.IsElementPresent("//h5[contains(text(),'Edit User Account for Employee')]"))
                {
                    agents_EmployeesHelper.Click("//h5[contains(text(),'Edit User Account for Employee')]");
                    agents_EmployeesHelper.WaitForWorkAround(1000);

                    executionLog.Log("PartnerAgentCodeInQuickLook", "Enter username");
                    agents_EmployeesHelper.TypeText("UserName", username1);

                    executionLog.Log("PartnerAgentCodeInQuickLook", "Select avatar");
                    agents_EmployeesHelper.ClickElement("AdminUserAvatar");

                }

                executionLog.Log("PartnerAgentCodeInQuickLook", "Click On Save Button");
                agents_EmployeesHelper.clickJS("SaveEmployee");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Open Employee 1");
                agents_EmployeesHelper.ClickElement("EditEmployee1");
                agents_EmployeesHelper.WaitForWorkAround(3000);

                executionLog.Log("PartnerAgentCodeInQuickLook", "Verify select eAddress Label Work.");
                agents_EmployeesHelper.selectedOption("eAddressLebel", "Work"); ;
                //agents_EmployeesHelper.WaitForWorkAround(2000);

            }

            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AmexRateCorp");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Amex Rate Corp");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Amex Rate Corp", "Bug", "Medium", "Amex page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        TakeScreenshot("AmexRateCorp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AmexRateCorp");
                        string id = loginHelper.getIssueID("Amex Rate Corp");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AmexRateCorp.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Amex Rate Corp"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Amex Rate Corp");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AmexRateCorp");
                executionLog.WriteInExcel("Amex Rate Corp", Status, JIRA, "Corp Master Data");
            }
        }
    }
}

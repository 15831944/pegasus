using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditOfficeDetail : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editOfficeDetail()
        {
            string[] username = null;
            string[] password = null;

            XMLParse oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_Admin_DashboardHelper = new DashBoard_CreateDashboardHelper(GetWebDriver());

            String JIRA = "";
            String Status = "Pass";
            try
            {

                executionLog.Log("EditOfficeDetail", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditOfficeDetail", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditOfficeDetail", "Visit admin page");
                VisitOffice("admin");
                office_Admin_DashboardHelper.WaitForWorkAround(1000);

                executionLog.Log("EditOfficeDetail", "Click on Edit Office details button");
                office_Admin_DashboardHelper.ClickElement("ClickOnEditOfficeCode");
                office_Admin_DashboardHelper.WaitForWorkAround(1000);

                executionLog.Log("EditOfficeDetail", "Click on Save button");
                office_Admin_DashboardHelper.ClickElement("SaveDashboard");

                executionLog.Log("EditOfficeDetail", "Verify Confirmation");
                office_Admin_DashboardHelper.WaitForText("Office codes updated successfully.", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditOfficeDetail");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Office Detail");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Client Addresses", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Office Detail");
                        TakeScreenshot("EditOfficeDetail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditOfficeDetail.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditOfficeDetail");
                        string id = loginHelper.getIssueID("Edit Office Detail");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\ClientAddresses.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Office Detail"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Office Detail");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditOfficeDetail");
                executionLog.WriteInExcel("Edit Office Detail", Status, JIRA, "Client Management");
            }
        }
    }
}

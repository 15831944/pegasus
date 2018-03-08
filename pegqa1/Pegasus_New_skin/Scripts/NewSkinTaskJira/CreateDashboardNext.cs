using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateDashboardNext : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS4")]
        [TestCategory("NewSkinTaskJira")]
        public void createDashboardNext()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var dashBoard_CreateDashboardHelper = new DashBoard_CreateDashboardHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("CreateDashboardNext", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateDashboardNext", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateDashboardNext", "Go to Create Dashboard page");
                VisitOffice("dashboards/create");

                executionLog.Log("CreateDashboardNext", "Verify title");
                VerifyTitle("Dashboards");

                executionLog.Log("CreateDashboardNext", "Click on Next button");
                dashBoard_CreateDashboardHelper.ClickElement("Next");

                executionLog.Log("CreateDashboardNext", "Accept alert message.");
                dashBoard_CreateDashboardHelper.AcceptAlert();

                executionLog.Log("CreateDashboardNext", "Wait for alert message.");
                dashBoard_CreateDashboardHelper.WaitForText("This field is required.", 10);

                executionLog.Log("CreateDashboardNext", "Log out from the application");
                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateDashboardNext");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create Dashboard Next");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create Dashboard Next", "Bug", "Medium", "User page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create Dashboard Next");
                        TakeScreenshot("CreateDashboardNext");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDashboardNext.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateDashboardNext");
                        string id = loginHelper.getIssueID("Create Dashboard Next");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDashboardNext.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create Dashboard Next"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create Dashboard Next");
           //     executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateDashboardNext");
                executionLog.WriteInExcel("Create Dashboard Next", Status, JIRA, "Dashboards");
            }
        }
    }
}
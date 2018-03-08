using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class SaveDashboard : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void saveDashboard()
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
            var reports_DashBoardsHelper = new Reports_DashBoardsHelper(GetWebDriver());


            // Variable
            var DashBoard = "Dashboard" + RandomNumber(1, 9999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("SaveDashboard", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("SaveDashboard", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("SaveDashboard", "Go to Create Dashboard page");
                VisitOffice("dashboards/create");

                executionLog.Log("SaveDashboard", "Verify title");
                VerifyTitle("Dashboards");

                executionLog.Log("SaveDashboard", "Enter Name");
                reports_DashBoardsHelper.TypeText("DashboardName", DashBoard);
                reports_DashBoardsHelper.WaitForWorkAround(1000);

                executionLog.Log("SaveDashboard", "Click on other dashlet");
                reports_DashBoardsHelper.CheckAndClick("DashboardLet");
                reports_DashBoardsHelper.WaitForWorkAround(1000);

                executionLog.Log("SaveDashboard", "Click on Next button");
                reports_DashBoardsHelper.ClickElement("SelectDashlet");
                reports_DashBoardsHelper.WaitForWorkAround(1000);

                executionLog.Log("SaveDashboard", "Click on Next button");
                reports_DashBoardsHelper.ClickElement("DashbboardNext");
                reports_DashBoardsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveDashboard", "Click on Save button");
                reports_DashBoardsHelper.ClickElement("ClickSaveNskin");
                reports_DashBoardsHelper.WaitForWorkAround(3000);

                executionLog.Log("SaveDashboard", "Verify title");
                VisitOffice("dashboards");
                reports_DashBoardsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveDashboard", "Search dashboard");
                reports_DashBoardsHelper.TypeText("SearchDashboard", DashBoard);
                reports_DashBoardsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveDashboard", "Select All in owner field");
                reports_DashBoardsHelper.SelectByText("OwnerField", "All");
                reports_DashBoardsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveDashboard", "Click on delete dashboard");
                reports_DashBoardsHelper.ClickElement("DeleteDashboard");
                reports_DashBoardsHelper.WaitForWorkAround(2000);

                executionLog.Log("SaveDashboard", "Accept alert message.");
                reports_DashBoardsHelper.AcceptAlert();
                reports_DashBoardsHelper.WaitForWorkAround(1000);

                executionLog.Log("SaveDashboard", "Click On dashlet");
                reports_DashBoardsHelper.WaitForText("Dashboard deleted successfully.", 30);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("SaveDashboard");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Save Dashboard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Save Dashboard", "Bug", "Medium", "Dashboard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Save Dashboard");
                        TakeScreenshot("SaveDashboard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveDashboard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("SaveDashboard");
                        string id = loginHelper.getIssueID("Save Dashboard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\SaveDashboard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Save Dashboard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Save Dashboard");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("SaveDashboard");
                executionLog.WriteInExcel("Save Dashboard", Status, JIRA, "Customizable Dashboards");
            }
        }
    }
}
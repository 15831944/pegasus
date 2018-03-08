using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.Corp
{
    [TestClass]
    public class Dashboard : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin")]
        [TestCategory("TS8")]
        [TestCategory("Pegasus_NewSkin")]
        public void dashboard()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var dashBoard_CreateDashboardHelper = new DashBoard_CreateDashboardHelper(GetWebDriver());
            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Variable
            var DashBoard = "Dash" + RandomNumber(99, 99999);
            var LName = "Test" + RandomNumber(99, 99999);
            var CDBA = "New" + RandomNumber(99, 99999);
            String JIRA = "";
            String Status = "Pass";

            try
            {

            executionLog.Log("Dashboard", "Login with valid credential  Username");
            Login(username[0], password[0]);

            executionLog.Log("Dashboard", "Verify Page title");
            VerifyTitle("Dashboard");
            Console.WriteLine("Redirected at Dashboard screen.");

            executionLog.Log("Dashboard", "Goto Create Dashboard");
            VisitOffice("dashboards/create");

            executionLog.Log("Dashboard", "Enter Dashboard Name");
            dashBoard_CreateDashboardHelper.TypeText("EnterDashboardName", DashBoard);

            executionLog.Log("Dashboard", "Click on Report Dashlest");
            dashBoard_CreateDashboardHelper.ClickElement("ClientReportDashlest");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Click Next Button");
            dashBoard_CreateDashboardHelper.ClickElement("ClickNextButton");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Save Dashboard");
            dashBoard_CreateDashboardHelper.ClickElement("ClickDashboardSaveBtn");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Goto Dashboard");
            VisitOffice("dashboards");

            executionLog.Log("Dashboard", "Search Name");
            dashBoard_CreateDashboardHelper.TypeText("SearchDeshboard", DashBoard);
            dashBoard_CreateDashboardHelper.selectOwner("//*[@name='owner']");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Click on Edit Dashboard");
            dashBoard_CreateDashboardHelper.ClickElement("ClickOnEditDashboard");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Click on Cancel button.");
            dashBoard_CreateDashboardHelper.ClickElement("ClickCancelDasEdit");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Dashboard Verify");
            dashBoard_CreateDashboardHelper.VerifyText("VerifyTextDeshboard", "Dashboards");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Goto Dashbord");
            VisitOffice("dashboards");

            executionLog.Log("Dashboard", "Search Dashboard");
            dashBoard_CreateDashboardHelper.TypeText("SearchDeshboard", DashBoard);
            dashBoard_CreateDashboardHelper.selectOwner("//*[@name='owner']");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Click on Edit");
            dashBoard_CreateDashboardHelper.ClickElement("ClickOnEditDashboard");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Click on Next");
            dashBoard_CreateDashboardHelper.ClickElement("ClickNextButton");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Click  on Save Dashboard");
            dashBoard_CreateDashboardHelper.ClickElement("ClickDashboardSaveBtn");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Goto Dashboard");
            VisitOffice("dashboards");

            executionLog.Log("Dashboard", "Search Dashboard");
            dashBoard_CreateDashboardHelper.TypeText("SearchDeshboard", DashBoard);
            dashBoard_CreateDashboardHelper.selectOwner("//*[@name='owner']");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Delete Dashboard");
            dashBoard_CreateDashboardHelper.ClickElement("ClickDeleteDashboard");
            dashBoard_CreateDashboardHelper.AcceptAlert();

            executionLog.Log("Dashboard", "Wait for confirmation.");
            dashBoard_CreateDashboardHelper.WaitForText("Dashboard deleted successfully.", 10);

            executionLog.Log("Dashboard", "Goto Dashbord");
            VisitOffice("dashboards");

            executionLog.Log("Dashboard", "Click on dashboard.");
            dashBoard_CreateDashboardHelper.ClickElement("ClickOnDashBoard");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Click on Edit Icon");
            dashBoard_CreateDashboardHelper.ClickElement("ClickEditDashboardButton");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(1000);

            executionLog.Log("Dashboard", "Click on Next");
            dashBoard_CreateDashboardHelper.ClickElement("ClickNextButton");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

            executionLog.Log("Dashboard", "Save Dashboard");
            dashBoard_CreateDashboardHelper.ClickElement("ClickDashboardSaveBtn");
            dashBoard_CreateDashboardHelper.WaitForWorkAround(3000);

        }
    


            catch
                        (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("Dashboard");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Dashboard");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Dashboard", "Bug", "Medium", "Dashboard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Dashboard");
                        TakeScreenshot("Dashboard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Dashboard.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("Dashboard");
                        string id = loginHelper.getIssueID("Dashboard");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\Dashboard.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Dashboard"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Dashboard");
               // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("Dashboard");
                executionLog.WriteInExcel("Dashboard", Status, JIRA, "Office Reports&DashBoards");
            }
        }
    }
} 
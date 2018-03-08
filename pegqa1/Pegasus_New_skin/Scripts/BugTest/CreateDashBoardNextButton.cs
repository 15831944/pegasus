using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class CreateDashBoardNextButton : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void createDashBoardNextButton()
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
            var report_DashboardHelper = new Reports_DashBoardsHelper(GetWebDriver());

            // Variable

            var mail = "Test" +GetRandomNumber() + "@yopmail.com";
            var numb = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";


            try
            {
                executionLog.Log("CreateDashBoardNextButton", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("CreateDashBoardNextButton", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("CreateDashBoardNextButton", " Redirect To Create dashboards");
                VisitOffice("dashboards/create");

                executionLog.Log("CreateDashBoardNextButton", " Verify page title.");
                VerifyTitle("Dashboards");

                executionLog.Log("CreateDashBoardNextButton", "Click on next button.");
                report_DashboardHelper.ClickElement("ClickNextButtonOfDashboard");
                
                executionLog.Log("CreateDashBoardNextButton", "Please select atleast one metric");
                report_DashboardHelper.VerifyAlertText("Select atleast one dashlet");
                report_DashboardHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("CreateDashBoardNextButton");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Create DashBoard Next Button");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Create DashBoard Next Button", "Bug", "Medium", "Dashboard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Create DashBoard Next Button");
                        TakeScreenshot("CreateDashBoardNextButton");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDashBoardNextButton.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("CreateDashBoardNextButton");
                        string id = loginHelper.getIssueID("Create DashBoard Next Button");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\CreateDashBoardNextButton.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Create DashBoard Next Button"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Create DashBoard Next Button");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("CreateDashBoardNextButton");
                executionLog.WriteInExcel("Create DashBoard Next Button", Status, JIRA, "Dashboards");
            }
        }
    }
}

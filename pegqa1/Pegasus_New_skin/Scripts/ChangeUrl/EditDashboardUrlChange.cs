using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class EditDashboardUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Url")]
        [TestCategory("TS3")]
        [TestCategory("ChangeUrl")]
        public void  editDashboardUrlChange()
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
            var FirstName = "Test" + GetRandomNumber();
            var LastName = "Tester" + GetRandomNumber();
            var Number = "12345678" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditDashboardUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditDashboardUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("EditDashboardUrlChange", "Redirect at dashboards page.");
                VisitOffice("dashboards");
               
                executionLog.Log("EditDashboardUrlChange", "Click On Edit Deshboard");
                report_DashboardHelper.ClickElement("ClickOnEditDeshboard");
                report_DashboardHelper.WaitForWorkAround(2000);

                executionLog.Log("EditDashboardUrlChange", "Change the url with the url number of another office");
                VisitOffice("dashboards/edit/363");
               
                executionLog.Log("EditDashboardUrlChange", "Verify Validation");
                report_DashboardHelper.WaitForText("You don't have privileges to edit this dashboard." ,10);
               
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditDashboardUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0"; 
                }
                bool result = loginHelper.CheckExstingIssue("Edit Dashboard Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Dashboard Url Change", "Bug", "Medium", "Dashboard page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Dashboard Url Change");
                        TakeScreenshot("EditDashboardUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDashboardUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditDashboardUrlChange");
                        string id = loginHelper.getIssueID("Edit Dashboard Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditDashboardUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Dashboard Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Dashboard Url Change");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditDashboardUrlChange");
                executionLog.WriteInExcel("Edit Dashboard Url Change", Status, JIRA, "Dashboard");
            }
        }
    }
}

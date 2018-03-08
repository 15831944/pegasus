using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyBulkUpdateForClients : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void verifyBulkUpdateForClients()
        {
            string[] username = null;
            string[] password = null;

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            String JIRA = "";
            String Status = "Pass";

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            try
            {
                executionLog.Log("VerifyBulkUpdateForClients", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyBulkUpdateForClients", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyBulkUpdateForClients", "Click on Menu Icon");
                office_ClientsHelper.ClickElement("MenuIcon");

                executionLog.Log("VerifyBulkUpdateForClients", "Click on Client tab.");
                office_ClientsHelper.MouseOverAndWait("ClickOnClientTab", 3);

                executionLog.Log("VerifyBulkUpdateForClients", "Click on Client button");
                office_ClientsHelper.ClickForce("AllClientBtn");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyBulkUpdateForClients", "Click on Bulk update.");
                office_ClientsHelper.ClickElement("ClickOnBulkUpdateClient");

                executionLog.Log("VerifyBulkUpdateForClients", "Verify bulk update");
                office_ClientsHelper.WaitForText("Bulk Update", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyBulkUpdateForClients");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Bulk Update For Clients");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Bulk Update For Clients", "Bug", "Medium", "Client page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Bulk Update For Clients");
                        TakeScreenshot("VerifyBulkUpdateForClients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBulkUpdateForClients.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyBulkUpdateForClients");
                        string id = loginHelper.getIssueID("Verify Bulk Update For Clients");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyBulkUpdateForClients.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Bulk Update For Clients"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Bulk Update For Clients");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyBulkUpdateForClients");
                executionLog.WriteInExcel("Verify Bulk Update For Clients", Status, JIRA, "Client Management");
            }
        }
    }
}
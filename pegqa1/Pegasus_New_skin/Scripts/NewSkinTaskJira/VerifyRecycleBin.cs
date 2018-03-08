using System;
using System.IO;
using LinqToExcel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class VerifyRecycleBin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void verifyRecycleBin()
        {
            string[] username = null;
            string[] password = null;
            string[] log = null;
            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");
            String JIRA = "";
            String Status = "Pass";

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");
            log = oXMLData.getData("settings/URL", "logout");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var office_ClientsHelper = new Office_ClientsHelper(GetWebDriver());

            try
            {
                executionLog.Log("VerifyRecycleBin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyRecycleBin", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("VerifyRecycleBin", "Go to Client export page");
                VisitOffice("clients/export");

                executionLog.Log("VerifyRecycleBin", "Verify Title");
                VerifyTitle("Client");

                executionLog.Log("VerifyRecycleBin", "Mouse hover on the tickets.");
                office_ClientsHelper.MouseOverAndWait("TicHover", 5);

                executionLog.Log("VerifyRecycleBin", "Click on All ticket button");
                office_ClientsHelper.ClickForce("AllTicketBtn");
                office_ClientsHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyRecycleBin", "Verify 'RecycleBin' not available");
                office_ClientsHelper.VerifyTextNotPresent("RecycleBin");

                executionLog.Log("VerifyRecycleBin", "Verify Recycle Bin available");
                office_ClientsHelper.VerifyPageText("Recycle Bin");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyRecycleBin");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Recycle Bin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Recycle Bin", "Bug", "Medium", "Clients page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Recycle Bin");
                        TakeScreenshot("VerifyRecycleBin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyRecycleBin");
                        string id = loginHelper.getIssueID("Verify Recycle Bin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyRecycleBin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Recycle Bin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Recycle Bin");
             //   executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyRecycleBin");
                executionLog.WriteInExcel("Verify Recycle Bin", Status, JIRA, "Client Management");
            }
        }
    }
}
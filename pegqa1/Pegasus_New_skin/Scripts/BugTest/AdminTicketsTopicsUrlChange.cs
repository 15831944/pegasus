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
    public class AdminTicketsTopicsUrlChange : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void adminTicketsTopicsUrlChange()
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
            var tickets_MasterDataHelper = new Tickets_MasterDataHelper(GetWebDriver());
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("AdminTicketsTopicsUrlChange", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("AdminTicketsTopicsUrlChange", "Verify Page title");
                VerifyTitle("Dashboard");
                Console.WriteLine("Redirected at Dashboard screen.");

                executionLog.Log("AdminTicketsTopicsUrlChange", "Redirect To Admin");
                VisitOffice("admin");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketsTopicsUrlChange", "Goto Resolution");
                VisitOffice("tickets/masterdata/resolution");

                executionLog.Log("AdminTicketsTopicsUrlChange", "Click On any Resolution");
                tickets_MasterDataHelper.ClickElement("ClicKOnIssueResolved");
                tickets_MasterDataHelper.WaitForWorkAround(1000);

                executionLog.Log("AdminTicketsTopicsUrlChange", "Change Url of the page.");
                VisitOffice("tickets/masterdata/edit/resolution/121");

                executionLog.Log("AdminTicketsTopicsUrlChange", "Valiadtaion You don't have privilege.");
                tickets_MasterDataHelper.WaitForText("You don't have privilege.", 20);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("AdminTicketsTopicsUrlChange");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Admin Tickets Topics Url Change");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Admin Tickets Topics Url Change", "Bug", "Medium", "Office tickets", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Admin Tickets Topics Url Change");
                        TakeScreenshot("AdminTicketsTopicsUrlChange");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketsTopicsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("AdminTicketsTopicsUrlChange");
                        string id = loginHelper.getIssueID("Admin Tickets Topics Url Change");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\AdminTicketsTopicsUrlChange.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Admin Tickets Topics Url Change"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Admin Tickets Topics Url Change");
                // executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("AdminTicketsTopicsUrlChange");
                executionLog.WriteInExcel("Admin Tickets Topics Url Change", Status, JIRA, "Admin tickets");
            }
        }
    }
}
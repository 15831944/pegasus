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
    public class TicketSettingAdmin : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void ticketSettingAdmin()
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
            var tickets_SettingHelper = new Ticket_SettingsHelper(GetWebDriver());

            // Variable random
            String JIRA = "";
            String Status = "Pass";

            try
            {

                executionLog.Log("TicketSettingAdmin", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("TicketSettingAdmin", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketSettingAdmin", "Redirect To Admin");
                VisitOffice("admin");

                executionLog.Log("TicketSettingAdmin", "Redirect to Ticket settings page.");
                VisitOffice("tickets/settings");
                tickets_SettingHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketSettingAdmin", "Select Ticket Created Status");
                tickets_SettingHelper.SelectByText("SelectTicketCreatedStatus", "New");
                tickets_SettingHelper.WaitForWorkAround(2000);

                executionLog.Log("TicketSettingAdmin", "Select Ticket Status");
                tickets_SettingHelper.SelectByText("ResolvedTicketStatus", "Resolved");

                executionLog.Log("TicketSettingAdmin", "Wait for element to present.");
                tickets_SettingHelper.WaitForElementPresent("ClosedTicketStatus", 10);

                executionLog.Log("TicketSettingAdmin", "Select Ticket Status");
                tickets_SettingHelper.SelectByText("ClosedTicketStatus", "Closed");

                executionLog.Log("TicketSettingAdmin", "Click Save");
                tickets_SettingHelper.ClickElement("ClickOnAddBtn");
                tickets_SettingHelper.WaitForWorkAround(3000);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketSettingAdmin");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Setting Admin");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Setting Admin", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Setting Admin");
                        TakeScreenshot("TicketSettingAdmin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketSettingAdmin.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketSettingAdmin");
                        string id = loginHelper.getIssueID("Ticket Setting Admin");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketSettingAdmin.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Setting Admin"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Setting Admin");
                //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketSettingAdmin");
                executionLog.WriteInExcel("Ticket Setting Admin", Status, JIRA, "Admin Tickets");
            }
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PegasusTests.PageHelper;
using PegasusTests.PageHelper.Comm;

namespace PegasusTests.Scripts.ClientsTests
{
    [TestClass]
    public class TicketIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("NewSkin_Task")]
        [TestCategory("TS5")]
        [TestCategory("NewSkinTaskJira")]
        public void ticketIssue()
        {
            string[] username = null;
            string[] password = null;
            String JIRA = "";
            String Status = "Pass";

            var oXMLData = new XMLParse();
            oXMLData.LoadXML("../../Config/ApplicationSettings.xml");

            username = oXMLData.getData("settings/Credentials", "username_office");
            password = oXMLData.getData("settings/Credentials", "password");

            // Initializing the objects
            var executionLog = new ExecutionLog();
            var loginHelper = new LoginHelper(GetWebDriver());
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            try
            {
                executionLog.Log("TicketIssue", "Login with valid username and password");
                Login(username[0], password[0]);

                executionLog.Log("TicketIssue", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("TicketIssue", "Go to open Ticket page");
                VisitOffice("ticket-search/open");

                executionLog.Log("TicketIssue", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketIssue", "Get first ticket id");
                string id = officeTickets_AllTicketsHelper.getText("TickID");

                executionLog.Log("TicketIssue", "Go to closed Ticket page");
                VisitOffice("ticket-search/closed");

                executionLog.Log("TicketIssue", "Verify title");
                VerifyTitle("Tickets");

                executionLog.Log("TicketIssue", "Verify ticket not available");
                officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

                executionLog.Log("TicketIssue", "Go to resolved Ticket page");
                VisitOffice("ticket-search/resolved");

                executionLog.Log("TicketIssue", "Verify title");
                VerifyTitle("Tickets");

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketIssue");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Issue", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Issue");
                        TakeScreenshot("TicketIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketIssue");
                        string id = loginHelper.getIssueID("Ticket Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Issue");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketIssue");
                executionLog.WriteInExcel("Ticket Issue", Status, JIRA, "Ticketing System");
            }
        }
    }
}
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
    public class TicketResultsPerPageIssue : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void ticketResultsPerPageIssue()
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
            var officeTickets_AllTicketsHelper = new OfficeTickets_AllTicketsHelper(GetWebDriver());

            // Variable
            var TickName = "Test Ticket" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            //        try
            //      {
            executionLog.Log("TicketResultsPerPageIssue", "Login with valid username and password");
            Login(username[0], password[0]);
            Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

            executionLog.Log("TicketResultsPerPageIssue", "Verify Page title");
            VerifyTitle("Dashboard");

            executionLog.Log("TicketResultsPerPageIssue", "Redirect To Tickets");
            VisitOffice("tickets");

            executionLog.Log("TicketResultsPerPageIssue", "Verify page title.");
            VerifyTitle("Tickets");

            executionLog.Log("TicketResultsPerPageIssue", "Click on advance filter");
            officeTickets_AllTicketsHelper.ClickElement("Advance");

            executionLog.Log("TicketResultsPerPageIssue", "Wait for locator to be present.");
            officeTickets_AllTicketsHelper.WaitForElementPresent("ResultsPerp", 10);
             
            executionLog.Log("TicketResultsPerPageIssue", "Select results per 20.");
            officeTickets_AllTicketsHelper.Select("ResultsPerp", "20");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketResultsPerPageIssue", "Click on apply button.");
            officeTickets_AllTicketsHelper.ClickElement("Apply");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

            executionLog.Log("TicketResultsPerPageIssue", "Verify that first twenty resords are displayed.");
            officeTickets_AllTicketsHelper.VerifyText("BootomResults", "Showing 1 - 20 ");
            officeTickets_AllTicketsHelper.WaitForWorkAround(3000);

        }
    } }
     /*       catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("TicketResultsPerPageIssue");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Ticket Results Per Page Issue");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Ticket Results Per Page Issue", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Ticket Results Per Page Issue");
                        TakeScreenshot("TicketResultsPerPageIssue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketResultsPerPageIssue.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("TicketResultsPerPageIssue");
                        string id = loginHelper.getIssueID("Ticket Results Per Page Issue");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\TicketResultsPerPageIssue.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Ticket Results Per Page Issue"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Ticket Results Per Page Issue");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("TicketResultsPerPageIssue");
                executionLog.WriteInExcel("Ticket Results Per Page Issue", Status, JIRA, "Office Tickets");
            }
        }
    }
}*/
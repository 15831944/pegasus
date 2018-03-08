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
    public class VerifyingTicketDefaultSettings : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void verifyingTicketDefaultSettings()
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
            var ticket_CreateATicketHelper = new OfficeTickets_CreateTicketsHelper(GetWebDriver());
            var ticket_SettingsHelper = new Ticket_SettingsHelper(GetWebDriver());

            // Variable
            var TickName = "Test Ticket" + RandomNumber(1, 999);
            var TicketName = "Test" + RandomNumber(1, 999);
            string Loc = "//a[text()=" + "[']" + TicketName + "[']" + "]";
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyingTicketDefaultSettings", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify page title.");
                VerifyTitle("Create a Ticket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation text for name");
                ticket_CreateATicketHelper.VerifyText("TickNameErr", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation text for assignee.");
                ticket_CreateATicketHelper.VerifyText("TickParentErr", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click On Client Display Icon");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Search Dba Client");
                ticket_CreateATicketHelper.TypeText("SearchDBAClient", "Chy Company");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on SearchButton");
                ticket_CreateATicketHelper.ClickElement("SearchButton");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "ClickOnClient");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait text Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status as open");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Open");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on resolve button.");
                ticket_CreateATicketHelper.ClickElement("ResolvedBtn");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on save button.");
                ticket_CreateATicketHelper.ClickElement("ClickResolvedSaveBtn");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation for resolution.");
                ticket_CreateATicketHelper.VerifyText("ResolutionError", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Select resolution for ticket.");
                ticket_CreateATicketHelper.Select("SelectTicketResolution", "Issue Resolved");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on save button.");
                ticket_CreateATicketHelper.ClickElement("ClickResolvedSaveBtn");

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait for success text.");
                ticket_CreateATicketHelper.WaitForText("Ticket Resolved Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status as resolved.");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Resolved");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on close button.");
                ticket_CreateATicketHelper.ClickElement("ClosedTicket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait for success test for closed ticket.");
                ticket_CreateATicketHelper.WaitForText("Ticket Closed Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status as closed.");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Closed");

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Select first ticket");
                ticket_CreateATicketHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on delete link");
                ticket_CreateATicketHelper.ClickElement("DeleteBulk");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Accept alert message.");
                ticket_CreateATicketHelper.AcceptAlert();

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect at ticket settings page.");
                VisitOffice("tickets/settings");

                executionLog.Log("VerifyingTicketDefaultSettings", "Select status when ticket created.");
                ticket_SettingsHelper.Select("SelectTicketCreatedStatus", "Resolved");

                executionLog.Log("VerifyingTicketDefaultSettings", "Select ticket status when resolved.");
                ticket_SettingsHelper.Select("ResolvedTicketStatus", "Closed");

                executionLog.Log("VerifyingTicketDefaultSettings", "Select ticket status when ticket closed.");
                ticket_SettingsHelper.Select("ClosedTicketStatus", "Open");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on Save button.");
                ticket_SettingsHelper.ClickElement("ClickOnAddBtn");

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To create tickets page.");
                VisitOffice("tickets/create");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify page title.");
                VerifyTitle("Create a Ticket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation text for name");
                ticket_CreateATicketHelper.VerifyText("TickNameErr", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation text for assignee.");
                ticket_CreateATicketHelper.VerifyText("TickParentErr", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TicketName);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click On Client Display Icon");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Search Dba Client");
                ticket_CreateATicketHelper.TypeText("SearchDBAClient", "Chy Company");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on SearchButton");
                ticket_CreateATicketHelper.ClickElement("SearchButton");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "ClickOnClient");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait text Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TicketName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Resolved");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on resolve button.");
                ticket_CreateATicketHelper.ClickElement("ResolvedBtn");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on save button.");
                ticket_CreateATicketHelper.ClickElement("ClickResolvedSaveBtn");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify validation text for resolution.");
                ticket_CreateATicketHelper.VerifyText("ResolutionError", "This field is required.");

                executionLog.Log("VerifyingTicketDefaultSettings", "Select ticket resolution.");
                ticket_CreateATicketHelper.Select("SelectTicketResolution", "Issue Resolved");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on save button.");
                ticket_CreateATicketHelper.ClickElement("ClickResolvedSaveBtn");

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait for success text.");
                ticket_CreateATicketHelper.WaitForText("Ticket Resolved Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TicketName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status as closed.");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Closed");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on close button.");
                ticket_CreateATicketHelper.ClickElement("ClosedTicket");

                executionLog.Log("VerifyingTicketDefaultSettings", "Wait for success text.");
                ticket_CreateATicketHelper.WaitForText("Ticket Closed Successfully.", 10);

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TicketName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Verify ticket status as open.");
                ticket_CreateATicketHelper.VerifyText("VerifyStatus", "Open");

                executionLog.Log("VerifyingTicketDefaultSettings", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketDefaultSettings", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TicketName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketDefaultSettings", "Select first ticket");
                ticket_CreateATicketHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyingTicketDefaultSettings", "Click on delete link");
                ticket_CreateATicketHelper.ClickElement("DeleteBulk");

                executionLog.Log("VerifyingTicketDefaultSettings", "Accept alert message.");
                ticket_CreateATicketHelper.AcceptAlert();

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingTicketDefaultSettings");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Ticket Default Settings");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Ticket Default Settings", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Ticket Default Settings");
                        TakeScreenshot("VerifyingTicketDefaultSettings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingTicketDefaultSettings.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingTicketDefaultSettings");
                        string id = loginHelper.getIssueID("Verifying Ticket Default Settings");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingTicketDefaultSettings.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Ticket Default Settings"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Ticket Default Settings");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingTicketDefaultSettings");
                executionLog.WriteInExcel("Verifying Ticket Default Settings", Status, JIRA, "Office tickets");
            }
        }
    }
}

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
    public class VerifyTicketsCreatedAndModifiedByCredits : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        public void verifyTicketsCreatedAndModifiedByCredits()
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

            // Variable
            var TickName = "Test Ticket" + RandomNumber(1, 999);
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Redirect To Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Create a Ticket");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify validation text for name");
                ticket_CreateATicketHelper.VerifyText("TickNameErr", "This field is required.");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify validation text for assignee.");
                ticket_CreateATicketHelper.VerifyText("TickParentErr", "This field is required.");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click On Client Display Icon");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Search Dba Client");
                ticket_CreateATicketHelper.TypeText("SearchDBAClient", "Chy Company");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on SearchButton");
                ticket_CreateATicketHelper.ClickElement("SearchButton");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "ClickOnClient");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Wait text Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify ticket created by.");
                ticket_CreateATicketHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify ticket Modified by.");
                ticket_CreateATicketHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on edit icon.");
                ticket_CreateATicketHelper.ClickElement("ClickEditTicketBtn");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Updated ticket name.");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on save button.");
                ticket_CreateATicketHelper.ClickElement("Save");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Wait for success text..");
                ticket_CreateATicketHelper.WaitForText("Ticket Edited Successfully", 10);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on first ticket.");
                ticket_CreateATicketHelper.ClickElement("Ticket1");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify ticket created by.");
                ticket_CreateATicketHelper.VerifyText("CreatedBy", "Howard Tang");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify ticket Modified by.");
                ticket_CreateATicketHelper.VerifyText("ModifiedBy", "Howard Tang");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Select first ticket");
                ticket_CreateATicketHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Click on delete link");
                ticket_CreateATicketHelper.ClickElement("DeleteBulk");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Accept alert message.");
                ticket_CreateATicketHelper.AcceptAlert();

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Redirect at tickets recyclebin page.");
                VisitOffice("tickets/recyclebin");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Verify page title.");
                VerifyTitle("Recycle Bin");

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Accept alert message.");
                ticket_CreateATicketHelper.ClickElement("DeletePermanently");
                ticket_CreateATicketHelper.AcceptAlert();

                executionLog.Log("VerifyTicketsCreatedAndModifiedByCredits", "Wait for success text.");
                ticket_CreateATicketHelper.WaitForText("Ticket Permanently Deleted.", 10);
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyTicketsCreatedAndModifiedByCredits");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verify Tickets Created And Modified By Credits");
                if (!result)
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verify Tickets Created And Modified By Credits", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
                        TakeScreenshot("VerifyTicketsCreatedAndModifiedByCredits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 5)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyTicketsCreatedAndModifiedByCredits");
                        string id = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyTicketsCreatedAndModifiedByCredits.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verify Tickets Created And Modified By Credits"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verify Tickets Created And Modified By Credits");
                executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyTicketsCreatedAndModifiedByCredits");
                executionLog.WriteInExcel("Verify Tickets Created And Modified By Credits", Status, JIRA, "Office tickets");
            }
        }
    }
}
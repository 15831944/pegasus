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
    public class VerifyingTicketModifiedByCredentials : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS3")]
        [TestCategory("BugTestNew")]
        public void verifyingTicketModifiedByCredentials()
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
            var TicketName = "Test" + GetRandomNumber();
            string Loc = "//a[text()=" + "[']" + TicketName + "[']" + "]";
            var TickName = "Test Ticket" + GetRandomNumber();

            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("VerifyingTicketModifiedByCredentials", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Redirect To Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Verify Page title");
                VerifyTitle("Create a Ticket");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Select the status");
                ticket_CreateATicketHelper.SelectByText("TickStatus", "New");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Click On Client Display Icon");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Click On Client");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Assigned To ");
                ticket_CreateATicketHelper.SelectDropDownByText("//*[@id='TicketAssignedUserId']", "Howard Tang");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Assigned Manager");
                ticket_CreateATicketHelper.SelectDropDownByText("//*[@id='TicketAssignedManagerId']", "Howard Tang");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Verify Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "SearchTicket");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Select All in Assign field");
                ticket_CreateATicketHelper.SelectByText("AssignedField", "All");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Click on ticket to view details");
                ticket_CreateATicketHelper.ClickElement("ClickOnTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(4000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Verify Modified By credentials");
                ticket_CreateATicketHelper.VerifyText("ModifiedBy", " By Howard Tang");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Redirect at tickets page");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Select All in Assign field");
                ticket_CreateATicketHelper.SelectByText("AssignedField", "All");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Select first ticket");
                ticket_CreateATicketHelper.ClickElement("Checkbox1");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Click on delete link");
                ticket_CreateATicketHelper.ClickElement("DeleteBulk");

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Accept alert message.");
                ticket_CreateATicketHelper.AcceptAlert();

                executionLog.Log("VerifyingTicketModifiedByCredentials", "Wait for success message.");
                ticket_CreateATicketHelper.WaitForText("1 Records deleted successfully", 10);

                VisitOffice("logout");
            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("VerifyingTicketModifiedByCredentials");
                String Error = executionLog.GetAllTextFile("Error");
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Verifying Ticket Modified By Credentials");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Verifying Ticket Modified By Credentials", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Verifying Ticket Modified By Credentials");
                        TakeScreenshot("VerifyingTicketModifiedByCredentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingTicketModifiedByCredentials.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("VerifyingTicketModifiedByCredentials");
                        string id = loginHelper.getIssueID("Verifying Ticket Modified By Credentials");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\VerifyingTicketModifiedByCredentials.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Verifying Ticket Modified By Credentials"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Verifying Ticket Modified By Credentials");
              //  executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("VerifyingTicketModifiedByCredentials");
                executionLog.WriteInExcel("Verifying Ticket Modified By Credentials", Status, JIRA, "Office Tickets");
            }
        }
    }
}

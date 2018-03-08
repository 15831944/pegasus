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
    public class EditTicketVerifyResponsibility : DriverTestCase
    {
        [TestMethod]
        [TestCategory("All")]
        [TestCategory("Bug")]
        [TestCategory("TS1")]
        [TestCategory("BugTest")]
        public void editTicketVerifyResponsibility()
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
            var TickName = "Test Ticket" + GetRandomNumber();
            String JIRA = "";
            String Status = "Pass";

            try
            {
                executionLog.Log("EditTicketVerifyResponsibility", "Login with valid username and password");
                Login(username[0], password[0]);
                Console.WriteLine("Logged in as: " + username[0] + " / " + password[0]);

                executionLog.Log("EditTicketVerifyResponsibility", "Verify Page title");
                VerifyTitle("Dashboard");

                executionLog.Log("EditTicketVerifyResponsibility", "Redirect To Tickets");
                VisitOffice("tickets/create");

                executionLog.Log("EditTicketVerifyResponsibility", "Verify page title.");
                VerifyTitle("Create a Ticket");

                executionLog.Log("EditTicketVerifyResponsibility", "Enter Ticket Name");
                ticket_CreateATicketHelper.TypeText("TicketName", TickName);

                executionLog.Log("EditTicketVerifyResponsibility", "Select the status");
                ticket_CreateATicketHelper.SelectByText("TickStatus", "New");

                executionLog.Log("EditTicketVerifyResponsibility", "ClickOnClientDisplayIcon");
                ticket_CreateATicketHelper.ClickElement("ClientDisplayIcon");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTicketVerifyResponsibility", "ClickOnClient");
                ticket_CreateATicketHelper.ClickElement("ClickOnClient");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTicketVerifyResponsibility", "Select Assigned To ");
                ticket_CreateATicketHelper.SelectDropDownByText("//*[@id='TicketAssignedUserId']", "Howard Tang");

                executionLog.Log("EditTicketVerifyResponsibility", "Select Assigned Manager");
                ticket_CreateATicketHelper.SelectDropDownByText("//*[@id='TicketAssignedManagerId']", "Howard Tang");

                executionLog.Log("EditTicketVerifyResponsibility", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTicketVerifyResponsibility", "Wait text Ticket Created Successfully.");
                ticket_CreateATicketHelper.WaitForText("Ticket Created Successfully.", 10);

                executionLog.Log("EditTicketVerifyResponsibility", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("EditTicketVerifyResponsibility", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTicketVerifyResponsibility", "Select all in assign field");
                ticket_CreateATicketHelper.SelectByText("AssignedField", "All");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTicketVerifyResponsibility", "Click on edit icon");
                ticket_CreateATicketHelper.ClickElement("ClickOnEdit");

                executionLog.Log("EditTicketVerifyResponsibility", "Verify page title.");
                VerifyTitle("Edit Ticket");

                executionLog.Log("EditTicketVerifyResponsibility", "Click on Save");
                ticket_CreateATicketHelper.ClickElement("ClickOnSaveTicket");
                ticket_CreateATicketHelper.WaitForWorkAround(3000);

                executionLog.Log("EditTicketVerifyResponsibility", "Verify Success message. ");
                ticket_CreateATicketHelper.WaitForText("Ticket Edited Successfully", 10);

                executionLog.Log("EditTicketVerifyResponsibility", "Redirect To Tickets");
                VisitOffice("tickets");
                ticket_CreateATicketHelper.WaitForWorkAround(1000);

                executionLog.Log("EditTicketVerifyResponsibility", "SearchTicket by Name");
                ticket_CreateATicketHelper.TypeText("SearchTicket", TickName);
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTicketVerifyResponsibility", "Select all in assign field");
                ticket_CreateATicketHelper.SelectByText("AssignedField", "All");
                ticket_CreateATicketHelper.WaitForWorkAround(2000);

                executionLog.Log("EditTicketVerifyResponsibility", "Select first ticket");
                ticket_CreateATicketHelper.ClickElement("Checkbox1");

                executionLog.Log("EditTicketVerifyResponsibility", "Click on delete link");
                ticket_CreateATicketHelper.ClickElement("DeleteBulk");

                executionLog.Log("EditTicketVerifyResponsibility", "Accept alert message.");
                ticket_CreateATicketHelper.AcceptAlert();

                executionLog.Log("EditTicketVerifyResponsibility", "Wait for success message.");
                ticket_CreateATicketHelper.WaitForText("1 Records deleted successfully", 10);

            }
            catch (Exception e)
            {
                executionLog.Log("Error", e.StackTrace);
                Status = "Fail";

                String counter = executionLog.readLastLine("counter");
                String Description = executionLog.GetAllTextFile("EditTicketVerifyResponsibility");
                String Error = executionLog.GetAllTextFile("Error");
                Console.WriteLine(Error);
                if (counter == "")
                {
                    counter = "0";
                }
                bool result = loginHelper.CheckExstingIssue("Edit Ticket Verify Responsibility");
                if (!result)
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        loginHelper.CreateIssue("Edit Ticket Verify Responsibility", "Bug", "Medium", "Ticket page", "QA", "Log in as: " + username[0] + " / " + password[0] + "\n\nSteps:\n" + Description + "\n\n\nError Description:\n" + Error);
                        string id = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
                        TakeScreenshot("EditTicketVerifyResponsibility");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTicketVerifyResponsibility.png";
                        loginHelper.AddAttachment(location, id);
                    }
                }
                else
                {
                    if (Int16.Parse(counter) < 9)
                    {
                        executionLog.Count("counter", (Int16.Parse(counter) + 1).ToString());
                        TakeScreenshot("EditTicketVerifyResponsibility");
                        string id = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
                        string directoryName = loginHelper.GetnewDirectoryName(GetPath());
                        var location = directoryName + "\\EditTicketVerifyResponsibility.png";
                        loginHelper.AddAttachment(location, id);
                        loginHelper.AddComment(loginHelper.getIssueID("Edit Ticket Verify Responsibility"), "This issue is still occurring");
                    }
                }
                JIRA = loginHelper.getIssueID("Edit Ticket Verify Responsibility");
            //    executionLog.DeleteFile("Error");
                throw;

            }
            finally
            {
                executionLog.DeleteFile("EditTicketVerifyResponsibility");
                executionLog.WriteInExcel("Edit Ticket Verify Responsibility", Status, JIRA, "Office Tickets");
            }
        }
    }
}